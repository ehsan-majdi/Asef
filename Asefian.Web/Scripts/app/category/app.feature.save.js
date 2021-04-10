$(document).ready(function () {

    $("#frmCategoryFeature").loadForm();

    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();
        $("#frmCategoryFeature").makeRequest();
    });

});

function saveComplete(response) {
    callbackAlert(response.message, function () {
        document.location.href = "/admin/category/feature/" + $("#hiddenCategoryId").val();
    })
}
