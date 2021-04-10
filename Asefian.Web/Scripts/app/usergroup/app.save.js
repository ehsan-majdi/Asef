$(document).ready(function () {
    $("#frmUserGroup").loadForm();

    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();
        $("#frmUserGroup").makeRequest();
    });

});

function saveComplete(response) {
    callbackAlert(response.message, function () {
        document.location.href = "/admin/usergroup/list";
    })
}
