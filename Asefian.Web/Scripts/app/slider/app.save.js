$(document).ready(function () {
    $("#frmSlider").loadForm();

    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();
        $("#frmSlider").makeRequest();
    });

    $("#upload").on("click", function () {
        $("#file").click();
    });

    $("#clearFile").on("click", function () {
        $("#txtFileName, #hiddenFileId").val("");
        $("#file").val("");
        $("#file").change();
    });

    $("#file").on("change", function () {
        if ($("#file")[0].files.length == 0) {
            $("#txtFileName, #hiddenFileId").val("");
            $("#clearFile").addClass("hide");
            $("#upload").removeClass("hide");
        }
        else {
            $("#txtFileName").val($("#file")[0].files.item(0).name);
            $("#hiddenFileId").val("");
            $("#clearFile").removeClass("hide");
            $("#upload").addClass("hide");
        }
    });

});

function saveComplete(response) {
    callbackAlert(response.message, function () {
        document.location.href = "/admin/slider/list";
    })
}

function loadComplete(response) {
    if (response.status === 200 && response.data.fileId) {
        $("#clearFile").removeClass("hide");
        $("#upload").addClass("hide");
    }
}