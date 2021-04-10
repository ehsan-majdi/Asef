$(document).ready(function () {
    $("#frmAccess").loadForm();

    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();
        $("#frmAccess").makeRequest();
    });

});

function saveComplete(response) {
    callbackAlert(response.message, function () {
        document.location.href = "/admin/usergroup/list";
    })
}
