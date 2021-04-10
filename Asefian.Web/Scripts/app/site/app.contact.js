$(document).ready(function () {

    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();
        $("#frmSettings").makeRequest();
    });
    $("#btnAddTelephone").on("click", function (e) {
        $("#telephoneContent").append($("#telephoneTemplate").html());
    });
    $(document).on("click", ".delete", function () {
        $(this).parent(".uk-margin").closest("div").remove();
    });
    $("#frmSettings").loadForm();
});

function saveComplete(response) {
    alert(response.message);
}

function beforeSave(entity) {
    entity.phoneNumber = [];
    $(".telephone").each(function (index, element) {
        var phoneNumber = $(element).find(".txtPhoneNumber").val();
        entity.phoneNumber.push(phoneNumber);
    });
    entity.phoneNumber = entity.phoneNumber.join("%");
    console.log(entity.phoneNumber)
    return entity;
}