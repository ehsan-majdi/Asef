$(document).ready(function () {
   

    $(document).on("click", ".delete", function (event) {
        event.preventDefault();
        var id = $(this).attr("data-id");

        deleteEntity("/admin/specialProject/removeGalleryFile/" + id, function (response) {
            if (response.status == 200) {
                //getList();
            }
            else {
                alert(response.message);
            }
        });
    });
    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();
        $("#frmSpecialProject").makeRequest();
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

function getList() {
    $("#list").list({
        success: function () {
            $(".gallery-file").fancybox({
                openEffect: 'elastic',
                closeEffect: 'elastic',
                helpers: { title: { type: 'inside' } }
            });
        }
    });
}