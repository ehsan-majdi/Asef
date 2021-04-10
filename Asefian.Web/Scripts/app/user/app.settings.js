$(document).ready(function () {

    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();
        $("#frmSettings").makeRequest();
    });
});

function saveComplete(response) {
    alert(response.message);
}
