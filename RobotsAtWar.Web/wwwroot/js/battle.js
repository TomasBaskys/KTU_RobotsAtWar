function onPageLoad() {
    $(".action_buttons .attack").on("click", attack);
}

function attack() {

    var actionBar = $('.action_progress_bar .progress-bar');

    actionBar.css("transition", "none");
    actionBar.css("width", "0");

    var battleFieldId = sessionStorage.getItem("battleFieldId");
    var robotId = sessionStorage.getItem("robotId");
    var attackStrength = 1;

    fillActionBar(attackStrength);

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