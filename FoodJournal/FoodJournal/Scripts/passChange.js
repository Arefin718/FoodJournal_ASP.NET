function passCheck() {

    if ($("#pass").val().trim().length != 0 && $("#conpass").val().trim().length != 0) {

        if ($("#pass").val() == $("#conpass").val()) {
            $("#passwordConfirmForm").submit();
        }
        else {
            $("#errorMessage").html("Password doesn't match");
        }
    }
    else {
        $("#errorMessage").html("Password Empty");
    }

}