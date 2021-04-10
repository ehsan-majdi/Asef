$(document).ready(function () {
    $("#frmUser").loadForm();

    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();
        $("#frmUser").makeRequest();
    });

    $("#txtMobileNumber").on("blur", function () {
        $("#mobileValid, #mobileInvalid").hide();

        if (!$(this).val()) return;

        $("#loaderMobileNumber").show();
        var params = { mobileNumber: $(this).val(), userId: $("#hiddenId").val() };
        backgroundGet('/admin/user/checkMobileNumber', params, function (response) {
            $("#loaderMobileNumber").hide();
            if (response.status !== 200) {
                dangerNotif(response.message);
                $("#mobileInvalid").show();
            }
            else {
                $("#mobileValid").show();
            }
        });
    });

    $("#txtEmail").on("blur", function () {
        $("#emailValid, #emailInvalid").hide();
        if (!$(this).val()) return;
        $("#loaderEmail").show();
        var params = { email: $(this).val(), userId: $("#hiddenId").val() };
        backgroundGet('/admin/user/checkEmail', params, function (response) {
            $("#loaderEmail").hide();
            if (response.status !== 200) {
                dangerNotif(response.message);
                $("#emailInvalid").show();
            }
            else {
                $("#emailValid").show();
            }
        });
    });
});

function saveComplete(response) {
    callbackAlert(response.message, function () {
        document.location.href = "/admin/user/list";
    })
}
