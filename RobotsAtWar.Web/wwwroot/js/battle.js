var actionInpactTextElement;
var robotHealthBarElement;

var maxRobotLife = 500;
var robotLife = 500;

function onPageLoad() {
    enableButtonPopovers(".action_buttons .attack", ".attack_buttons");
    enableButtonPopovers(".action_buttons .defence", ".defence_buttons");
    enableButtonPopovers(".action_buttons .rest", ".rest_buttons");

    actionInpactTextElement = $(".action_impact_text")[0];
    robotHealthBarElement = $(".robot_healt_bar .progress-bar")[0];

    startPreparationCountdown(3);

    robotStatusPolling();
}

function enableButtonPopovers(popoverSelector, contentSelector) {
    $(popoverSelector).popover({ trigger: "manual", html: true, content: $(contentSelector).html() })
    .on("mouseenter", function () {
        var _this = this;
        $(this).popover("show");
        $(".popover").on("mouseleave", function () {
            $(_this).popover('hide');
        });
    }).on("mouseleave", function () {
        var _this = this;
        setTimeout(function () {
            if (!$(".popover:hover").length) {
                $(_this).popover("hide");
            }
        });
    });
}

function attack(strength) {

    var battleFieldId = sessionStorage.getItem("battleFieldId");
    var robotId = sessionStorage.getItem("robotId");

    fillActionBar(strength);
    clearActionBar(strength);

    $.get('http://localhost:1235/api/actions/attack?' +
        'battleFieldId=' + battleFieldId +
        '&robotId=' + robotId +
        '&attackStrength=' + strength,
        function (response) {
            switch (response) {
                case -1:
                    setActionImpactText("interupted", "red");
                    break;
                case -99:
                    setActionImpactText("dead", "red");
                    break;
                default:
                    setActionImpactText(response, "red");
            }
        });
}


function defence(strength) {

    var battleFieldId = sessionStorage.getItem("battleFieldId");
    var robotId = sessionStorage.getItem("robotId");

    fillActionBar(strength);
    clearActionBar(strength);

    setDefenceText("start");

    $.get('http://localhost:1235/api/actions/defence?' +
        'battleFieldId=' + battleFieldId +
        '&robotId=' + robotId +
        '&defenceStrength=' + strength,
        function () {
            setDefenceText("finish");
        });
}

function rest(strength) {

    var battleFieldId = sessionStorage.getItem("battleFieldId");
    var robotId = sessionStorage.getItem("robotId");

    fillActionBar(strength);
    clearActionBar(strength);

    $.get('http://localhost:1235/api/actions/rest?' +
        'battleFieldId=' + battleFieldId +
        '&robotId=' + robotId +
        '&restStrength=' + strength,
        function (response) {
            setActionImpactText(response, "green");
        });
}

function fillActionBar(length) {
    var actionBar = $('.action_progress_bar .progress-bar');

    actionBar.css("transition", "width " + length + "s ease-in-out");
    actionBar.css("width", "100%");
}

function clearActionBar(timeToWait) {
    var actionBar = $('.action_progress_bar .progress-bar');

    setTimeout(function () {
        actionBar.css("transition", "unset");

        actionBar.css("width", "0");
    }, timeToWait * 1000 + 100);
}

function setActionImpactText(impact, color) {
    actionInpactTextElement.innerHTML = impact;
    actionInpactTextElement.style.color = color;

    setTimeout(function () {
        actionInpactTextElement.innerHTML = "";
    }, 500);
}

function setDefenceText(defenceState) {
    actionInpactTextElement.style.color = "black";

    switch (defenceState) {
        case "start":
            actionInpactTextElement.innerHTML = "&#9960";
            break;
        case "finish":
            actionInpactTextElement.innerHTML = "";
            break;
        default:
            actionInpactTextElement.innerHTML = "";
    }
}

function startPreparationCountdown(time) {
    var timerElement = $(".timer h1")[0];

    var countdownId = setInterval(function () {
        timerElement.innerHTML = --time;
        if (time === 0) {
            timerElement.innerHTML = "START!";
        }
    }, 1000);

    setTimeout(function () {
        clearInterval(countdownId);
        timerElement.innerHTML = "";
    }, (time+1) * 1000);
}

function robotStatusPolling()
{
    var battleFieldId = sessionStorage.getItem("battleFieldId");
    var robotId = sessionStorage.getItem("robotId");

    setInterval(function () {
        $.get('http://localhost:1235/api/battlefield/robotstatus?' +
            'battleFieldId=' + battleFieldId +
            '&robotId=' + robotId,
            function (response) {
                if (response !== "" && Number(response) == response) {
                    controlRobotHealthBar(Number(response));
                    setActionImpactText(response, "black");
                }
                else if (response === "Dead") {
                    setActionImpactText(response, "black");
                }
            });
    }, 100);
}

function controlRobotHealthBar(damage) {
    robotLife -= damage;

    var healthBarPercentage = robotLife / maxRobotLife * 100;

    robotHealthBarElement.style.width = healthBarPercentage + "%";
}