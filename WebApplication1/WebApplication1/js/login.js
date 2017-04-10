$(document).ready(init);

function init() {
    firebase.auth().onAuthStateChanged(function (user) {
        if (user) {
            $(location).attr("href", "carga-de-archivos.html");
        } else {
            document.getElementsByTagName("html")[0].style.visibility = "visible";
        }
    });
    $("#iniciar").click(iniciar_sesion);
}


function iniciar_sesion(evt) {
    evt.preventDefault();

    //if (validarFormulario()) {

    var email = $("#usuario").val();
    var password = $("#password").val();

    if (email == '' || password == '') {
        alert("Por favor llenar todos los campos");

    } else {
        firebase.auth().signInWithEmailAndPassword(email, password).then(function () {
            goToCargaDeArchivos();
        }).catch(function (error) {
            // Handle Errors here.
            var errorCode = error.code;
            var errorMessage = error.message;
            alert("Datos inváidos");
            // ...
        });
    }
    //}
}

function validarFormulario() {

    var email = $("#usuario").val();
    var password = $("#password").val();
    var resultado = true;

    if (email == '') {
        $('#usuario').addClass('error');
        $('#usuario').nextAll().remove();
        $('#usuario').after('<label>El usuario no puede estar vacío.</label>');
        resultado = false;
    } else {
        $('#usuario').removeClass('error');
        $('#usuario').nextAll().remove();
    }

    if (password == '') {
        $('#password').addClass('error');
        $('#password').nextAll().remove();
        $('#password').after('<label>La contraseña no puede estar vacía.</label>');
        resultado = false;
    } else {
        $('#password').removeClass('error');
        $('#password').nextAll().remove();
    }
    return resultado;
}

function goToCargaDeArchivos() {
    $(location).attr("href", "carga-de-archivos.html");
}

function crear_usuario(evt) {
    evt.preventDefault();

    var email = $("#usuario").val();
    var password = $("#password").val();

    firebase.auth().createUserWithEmailAndPassword(email, password).catch(function (error) {
        // Handle Errors here.
        var errorCode = error.code;
        var errorMessage = error.message;

        alert(errorCode);
        alert(errorMessage);
        // ...
    });
}


function recuperar_contrasena(emailAddress) {
    var auth = firebase.auth();
    auth.sendPasswordResetEmail(emailAddress).then(function () {
        // Email sent.
    }, function (error) {
        console.log(error);
    });
}