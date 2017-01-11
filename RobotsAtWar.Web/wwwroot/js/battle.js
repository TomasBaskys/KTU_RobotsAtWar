var robotActionInpactTextElement;
var enemyActionInpactTextElement;

var battleHeaderElement;

var robotHealthBarElement;
var enemyHealthBarElement;

var maxRobotLife = 500;
var robotLife = 500;
var enemyLife = 500;

function onPageLoad() {
    enableButtonPopovers(".action_buttons .attack", ".attack_buttons");
    enableButtonPopovers(".action_buttons .defence", ".defence_buttons");
    enableButtonPopovers(".action_buttons .rest", ".rest_buttons");

    robotActionInpactTextElement = $(".robot_action_impact_text")[0];
    enemyActionInpactTextElement = $(".enemy_action_impact_text")[0];

    battleHeaderElement = $(".timer h1")[0];
    robotHealthBarElement = $(".robot_healt_bar .progress-bar")[0];
    enemyHealthBarElement = $(".enemy_healt_bar .progress-bar")[0];

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

    $.get('http://PCTOMBASL1:1235/api/actions/attack?' +
        'battleFieldId=' + battleFieldId +
        '&robotId=' + robotId +
        '&attackStrength=' + strength);
}


function defence(strength) {

    var battleFieldId = sessionStorage.getItem("battleFieldId");
    var robotId = sessionStorage.getItem("robotId");

    fillActionBar(strength);
    clearActionBar(strength);

    setDefenceText(robotActionInpactTextElement, "start");

    $.get('http://PCTOMBASL1:1235/api/actions/defence?' +
        'battleFieldId=' + battleFieldId +
        '&robotId=' + robotId +
        '&defenceStrength=' + strength,
        function () {
            setDefenceText(robotActionInpactTextElement, "finish");
        });
}

function rest(strength) {

    var battleFieldId = sessionStorage.getItem("battleFieldId");
    var robotId = sessionStorage.getItem("robotId");

    fillActionBar(strength);
    clearActionBar(strength);

    $.get('http://PCTOMBASL1:1235/api/actions/rest?' +
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

function setActionImpactText(element, impact, color) {
    element.innerHTML = impact;
    element.style.color = color;

    setTimeout(function () {
        element.innerHTML = "";
    }, 500);
}

function setDefenceText(element, defenceState) {
    element.style.color = "black";

    switch (defenceState) {
        case "start":
            element.innerHTML = "&#9960";
            break;
        case "finish":
            element.innerHTML = "";
            break;
        default:
            element.innerHTML = "";
    }
}

function startPreparationCountdown(time) {
    var countdownId = setInterval(function () {
        battleHeaderElement.innerHTML = --time;
        if (time === 0) {
            battleHeaderElement.innerHTML = "START!";
        }
    }, 1000);

    setTimeout(function () {
        clearInterval(countdownId);
        battleHeaderElement.innerHTML = "";
    }, (time+1) * 1000);
}

function robotStatusPolling()
{
    var battleFieldId = sessionStorage.getItem("battleFieldId");
    var robotId = sessionStorage.getItem("robotId");

    setInterval(function () {
        $.get('http://PCTOMBASL1:1235/api/battlefield/robotstatus?' +
            'battleFieldId=' + battleFieldId +
            '&robotId=' + robotId,
            function (response) {

                var robotImpact = Number(robotHealthBarElement.innerHTML) - response.Robot;
                var enemyImpact = Number(enemyHealthBarElement.innerHTML) - response.Enemy;

                if (robotImpact > 0) {
                    setActionImpactText(robotActionInpactTextElement, robotImpact, "red");
                }
                else if (robotImpact < 0) {
                    setActionImpactText(robotActionInpactTextElement, Math.abs(robotImpact), "green");
                }
                else if (enemyImpact > 0) {
                    setActionImpactText(enemyActionInpactTextElement, enemyImpact, "red");
                }
                else if (enemyImpact < 0) {
                    setActionImpactText(enemyActionInpactTextElement, Math.abs(enemyImpact), "green");
                }

                if (response.Robot <= 0) {
                    setActionImpactText(battleHeaderElement, "You Lose!", "red");
                }

                if (response.Enemy <= 0) {
                    setActionImpactText(battleHeaderElement, "You Win!", "green");
                }

                controlHealthBar(response.Robot, robotHealthBarElement);
                controlHealthBar(response.Enemy, enemyHealthBarElement);

            });
    }, 100);
}

function controlHealthBar(life, healthBarElement) {
    var healthBarPercentage = life / maxRobotLife * 100;

    healthBarElement.style.width = healthBarPercentage + "%";
    healthBarElement.innerHTML = life;
}

function battleStatus(text, color) {
    battleHeaderElement.innerHTML = text;
    battleHeaderElement.style.color = color;
}