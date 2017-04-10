    firebase.auth().onAuthStateChanged(function (user) {
    if (user) {
        console.log(user);
        $("#nombre_usuario").text(user.email);
    } else {
        $(location).attr("href", "login.html");
    }
});

$(document).ready(init);
function init() {

    $("#btn_cerrar_sesion").click(cerrar_sesion);
}

function cerrar_sesion(evt) {
    evt.preventDefault();
    firebase.auth().signOut().then(function () {
        // Sign-out successful.
    }, function (error) {
        // An error happened.
    });
}