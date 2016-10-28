function redirectOnActiveSession() {

    var robotId = sessionStorage.getItem("robotId");

    if (robotId !== null) {
        $.get('http://localhost:1235/api/auth/login?robotId='.concat(robotId), function (isValidId) {
            if (isValidId) {
                window.location.replace("./index.html");
            }
        });
    }
}

function onPageLoad() {
    $(".login-form").submit(function () {
        login();
        return false;
    });
}

function login() {

    var inputValue = document.getElementById("loginInput").value;

    $.get('http://localhost:1235/api/auth/login?robotId='.concat(inputValue), function (isValidId) {
        if (isValidId) {
            sessionStorage.setItem("robotId", inputValue);
            window.location.replace("./index.html");
        }
    });
    return true;
}