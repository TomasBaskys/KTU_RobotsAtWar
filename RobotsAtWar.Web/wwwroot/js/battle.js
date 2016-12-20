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
        function (damage) {
            setActionImpactText(damage, "red");
        });
}

function defence(strength) {

    var battleFieldId = sessionStorage.getItem("battleFieldId");
    var robotId = sessionStorage.getItem("robotId");

    fillActionBar(strength);
    clearActionBar(strength);

    $.get('http://localhost:1235/api/actions/attack?' +
        'battleFieldId=' + battleFieldId +
        '&robotId=' + robotId +
        '&attackStrength=' + strength,
        function (damage) {
            switch (damage) {
                case -1:
                    setActionImpactText("interupted", "red");
                    break;
                case -99:
                    setActionImpactText("dead", "red");
                    break;
                default:
                    setActionImpactText(damage, "red");
            }
        });
}

function rest(strength) {

    var battleFieldId = sessionStorage.getItem("battleFieldId");
    var robotId = sessionStorage.getItem("robotId");

    fillActionBar(strength);
    clearActionBar(strength);

    /* $.get('http://localhost:1235/api/actions/attack?battleFieldId=' + battleFieldId + '&robotId=' + robotId + '&attackStrength=' + attackStrength, function (damage) {
         console.log(damage);
         
     });*/
    return true;
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

function setActionImpactText(impact, color) {
    actionInpactTextElement.innerHTML = impact;
    actionInpactTextElement.style.color = color;

    setTimeout(function () {
        actionInpactTextElement.innerHTML = "";
    }, 500);
}