$(document).ready(function () {

    $("#frmFaqCategory").loadForm();

    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();
        $("#frmFaqCategory").makeRequest();
    });

});

function saveComplete(response) {
    document.location.href = "/admin/faqcategory/list";
}
