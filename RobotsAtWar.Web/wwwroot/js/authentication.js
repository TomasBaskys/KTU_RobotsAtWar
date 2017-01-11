window.fbAsyncInit = function () {
    FB.init({
        appId: '188040698337782',
        cookie: true,  // enable cookies to allow the server to access
        // the session
        xfbml: true,  // parse social plugins on this page
        version: 'v2.8' // use graph api version 2.8
    });
};

// Load the SDK asynchronously
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.8&appId=188040698337782";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

function setRobotId() {
    if (sessionStorage.getItem("robotId") === null) {
        setTimeout(function () {
            FB.getLoginStatus(function (response) {
                if (response.status === "connected" && sessionStorage.getItem("robotId") === null) {
                    sessionStorage.setItem("robotId", response.authResponse.userID);
                    location.reload();
                }
            });
        }, 500);
    }
}

function redirectOnUnauthenticated() {
    if (!isAuthenticated()) {
        window.location.replace("./index.html");
    }
}

function isAuthenticated() {
    return sessionStorage.getItem("robotId") !== null;
}

function login() {
    FB.login(function (response) {
        sessionStorage.setItem("robotId", response.authResponse.userID);
        window.location.reload();
    });
}

function fbLogout() {
    FB.getLoginStatus(function () {
        FB.logout(function () {
            sessionStorage.removeItem("robotId");
            window.location.reload();
        });
    });
}