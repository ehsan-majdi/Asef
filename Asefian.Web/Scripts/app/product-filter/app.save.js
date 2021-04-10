$(document).ready(function () {

    $("#frmFilterProduct").find('input, textarea, button, select').prop('disabled', true);
    $("#cmbCategory").fillSelect({
        callback: function (response) {
            $("#frmFilterProduct").find('input, textarea, button, select').prop('disabled', false);
            $("#frmFilterProduct").loadForm();
        }
    });

    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();
        $("#frmFilterProduct").makeRequest();
    });

});

function saveComplete(response) {
    callbackAlert(response.message, function () {
        document.location.href = "/admin/product/filter/list";
    })
}
