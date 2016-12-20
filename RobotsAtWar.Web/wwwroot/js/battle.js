var actionInpactTextElement;

function onPageLoad() {
    enableButtonPopovers(".action_buttons .attack", ".attack_buttons");
    enableButtonPopovers(".action_buttons .defence", ".defence_buttons");
    enableButtonPopovers(".action_buttons .rest", ".rest_buttons");

    actionInpactTextElement = $(".action_impact_text")[0];
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
        actionBar.css("transition", "unset ");

        actionBar.css("width", "0");
    }, timeToWait * 1000 + 100);
}

function setActionImpactText(impact, color, timeToShow) {
    timeToShow = typeof timeToShow !== 'undefined' ? timeToShow : 500;

    actionInpactTextElement.innerHTML = impact;
    actionInpactTextElement.style.color = color;

    setTimeout(function () {
        actionInpactTextElement.innerHTML = "";
    }, timeToShow);
}

function setDefenceText(defenceState) {
    actionInpactTextElement.style.color = "black";

    switch (defenceState) {
        case "start":
            actionInpactTextElement.innerHTML = "Defending";
            break;
        case "finish":
            actionInpactTextElement.innerHTML = "";
            break;
        default:
            actionInpactTextElement.innerHTML = "";
    }
}