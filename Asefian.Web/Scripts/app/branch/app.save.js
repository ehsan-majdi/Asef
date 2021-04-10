$(document).ready(function () {
    $("#frmBranch").loadForm();

    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();
        $("#frmBranch").makeRequest();
    });

});

function saveComplete(response) {
    callbackAlert(response.message, function () {
        document.location.href = "/admin/branch/list";
    });
}
