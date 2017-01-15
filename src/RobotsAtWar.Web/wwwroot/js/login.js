function onPageLoad() {
    $(".login-form").submit(function () {
        login();
        return false;
    });
}

function login() {

    var inputValue = document.getElementById("loginInput").value;

    $.get('http://PCTOMBASL1:1235/api/auth/login?robotId='.concat(inputValue), function (isValidId) {
        if (isValidId) {
            sessionStorage.setItem("robotId", inputValue);
            window.location.replace("./index.html");
        }
    });
    return true;
}