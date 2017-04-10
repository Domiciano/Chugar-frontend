$(document).ready(init);

function init() {
    document.getElementsByTagName("html")[0].style.visibility = "visible";
    $("#recuperar-password").click(enviarCorreo);
}

function enviarCorreo() {
    var auth = firebase.auth();
    var emailAddress = $("#usuario_recuperar").val();
    alert(emailAddress);

    auth.sendPasswordResetEmail(emailAddress).then(function () {
        $(location).attr("href", "login.html");
    }, function (error) {
        //ERROR
    });

}