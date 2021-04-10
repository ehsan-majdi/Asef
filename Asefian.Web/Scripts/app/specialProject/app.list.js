$(document).ready(function () {
    $("#list").list();
    $(document).on("click", ".removeGalleryFile", function (event) {
        event.preventDefault();
        var id = $(this).attr("data-id");

        deleteEntity("/admin/specialProject/removeGalleryFile/" + id, function (response) {
            if (response.status == 200) {
                getList();
            }
            else {
                alert(response.message);
            }
        });
    });
    $(document).on("click", ".editGalleryFile", function (event) {
        event.preventDefault();
        var id = $(this).attr("data-id");

    });
    
    $(".search").on("keypress", function (event) {
        if (event.which == 13) {
            var entity = {
                word: $(this).val(),
                page: 0
            }
            resetParam(entity)
        }
    });
    $("#btnAddProjectGallery").on("click", function (event) {
        event.preventDefault();
        $("#frmAddProjectGallery").makeRequest();
    });
    $(document).on("click", ".addGallery", function () {
        var id = $(this).parents(".specialProjectRow").attr("data-id");
        $("#gallery-list").attr("data-url", "/admin/specialProject/specialProjectImageList/" + id)
        $("#hiddenId").val(id)
        getList()
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
    $("#gallery-list").list({
        template: "#gallery-template",
        success: function (response) {
            $(".gallery-file").fancybox({
                openEffect: 'elastic',
                closeEffect: 'elastic',
                helpers: { title: { type: 'inside' } }
            });
            if (response.status == 200 && response.data.list.length > 0) {
                var id = $("#hiddenId").val();
                if (id == response.data.specialProjectId) {
                    $(".specialProjectRow[data-id=" + id + "]").find(".addGallery").html(" <i style=\"color: green\" class=\"fa fa-check\"></i>")

                }
            }
            else if ((response.status == 200 && response.data.list.length == 0)) {
                var id = $("#hiddenId").val();
                if (id == response.data.specialProjectId) {
                    $(".specialProjectRow[data-id=" + id + "]").find(".addGallery").html(" <i style=\"color: red\" class=\"fa fa-times\"></i>")
                }
            }
        }
    });
}

function addspecialProjectImageSuccess(response) {
    callbackAlert(response.message, function () {
        location.reload();
        $("#txtFileName").val("")
    });
}

