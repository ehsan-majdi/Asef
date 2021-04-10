$(document).ready(function () {
    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();
        $("#frmChangePassword").makeRequest();
    });

});

function saveComplete(response) {
    alert(response.message);
    $("#frmChangePassword").clearEntity();
}