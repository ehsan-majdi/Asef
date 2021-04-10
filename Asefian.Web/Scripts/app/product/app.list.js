$(document).ready(function () {
    editor()
    productUpload()
    $("#list").list();
    
    $(document).on("click", ".removeGalleryFile", function (event) {
        event.preventDefault();
        var id = $(this).attr("data-id");

        deleteEntity("/admin/product/removeGalleryFile/" + id, function (response) {
            if (response.status == 200) {
                getList();
            }
            else {
                alert(response.message);
            }
        });
    });
    $(document).on("click", ".add", function (event) {
        event.preventDefault();
        $("#file").click();
    });

    $('#file').fileupload({
        dataType: 'json',
        url: '/admin/product/addImage/' + $("#hiddenId").val(),
        start: function () {
            //loader(true);
        },
        success: function (result, textStatus, jqXHR) {
            if (result.status == 200) {
                $("#hiddenFileId").val(result.data.id);
                $("#hiddenFileName").val(result.data.fileName);

                getList();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //loader(false);
        },
        complete: function (result, textStatus, jqXHR) {
            //loader(false);
        }
    });
    $("#removeParam").on("click", function () {
        removeParam()
        $("#hasPhoto").val("")
        $("#hasFilter").val("")
        $("#statusId").val("")
        $("#hasDescription").val("")
        $(".search").val("")
    })
    $(".search").on("keypress", function (event) {
        if (event.which == 13) {
            var entity = {
                word: $(this).val(),
                page : 0
            }
            resetParam(entity)
        }
    });
    $("#hasPhoto").on("change", function () {
        setParam("hasPhoto" , $(this).val())
        resetParam("page", 0)
    })
    $("#hasFilter").on("change", function () {
        setParam("hasFilter", $(this).val())
        resetParam("page",0)
    })
    $("#statusId").on("change", function () {
        setParam("statusId", $(this).val())
        resetParam("page", 0)
    })
    $("#hasDescription").on("change", function () {
        setParam("hasDescription", $(this).val())
        resetParam("page", 0)
    })
   
    $(document).on("click", ".changeStatus", function () {
        changeStatus($(this).attr("data-id"), $(this).attr("data-status-id"))
    });
    $(document).on("click", ".addProductImage", function () {
        var id = $(this).parents(".productRow").attr("data-id")
        $("#hiddenProductId").val(id)
    });
    $(document).on("click", "#btnSubmitDescription", function () {
        $("#frmDescription").makeRequest();
    });
    $(document).on("click", "#btnAddProductImage", function () {
        $("#frmAddProductImage").makeRequest();
    });
    $(document).on("click", ".addFilter", function () {

    });
    $(document).on("click", ".addGallery", function () {
        var id = $(this).parents(".productRow").attr("data-id")
        $("#gallery-list").attr("data-url", "/admin/product/productImageList/" + id)
        $("#hiddenId").val(id)
        getList()
    });
    $(document).on("click", ".addDescription", function () {
        var id = $(this).parents(".productRow").attr("data-id")
        //$("#txtDescription").val()
        $('#txtDescription').froalaEditor('html.set', $(this).attr("data-description"));
        $("#hiddenDescriptionId").val(id)
    });

});
function productUpload() {
    $("#productUpload").on("click", function () {
        $("#productFile").click();
    });

    $("#productClearFile").on("click", function () {
        $("#txtProductFileName, #hiddenProductFileId").val("");
        $("#productFile").val("");
        $("#productFile").change();
    });

    $("#productFile").on("change", function () {
        if ($("#productFile")[0].files.length == 0) {
            $("#txtFileName, #hiddenProductFileId").val("");
            $("#productClearFile").addClass("hide");
            $("#productUpload").removeClass("hide");
        }
        else {
            $("#txtProductFileName").val($("#productFile")[0].files.item(0).name);
            $("#hiddenProductFileId").val("");
            $("#productClearFile").removeClass("hide");
            $("#productUpload").addClass("hide");
        }
    });
}
function changeStatus(id, statusId) {
    var params = {
        id: id,
        statusId: statusId,
    };

    boxLoader(".content", true)
    $.post("/admin/product/changeStatus", params, function (response) {
        if (response.status == 200) {
            //setParam({ statusId: 1 }, ["page"]);
            location.reload();
        }
        boxLoader(".content", false)
    });
}
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
                if (id == response.data.productId) {
                    $(".productRow[data-id=" + id + "]").find(".addGallery").html(" <i style=\"color: green\" class=\"fa fa-check\"></i>")

                }
            }
            else if ((response.status == 200 && response.data.list.length == 0)) {
                var id = $("#hiddenId").val();
                if (id == response.data.productId) {
                    $(".productRow[data-id=" + id + "]").find(".addGallery").html(" <i style=\"color: red\" class=\"fa fa-times\"></i>")
                }
            }
        }
    });
}

function addDescriptionSuccess(response) {
    callbackAlert(response.message, function () {
        if (response.status == 200 && response.data.description != null) {
            var id = $("#hiddenDescriptionId").val();
            if (id == response.data.id) {
                $(".productRow[data-id=" + id + "]").find(".addDescription").attr("data-description", response.data.description)
                
                $(".productRow[data-id=" + id + "]").find(".addDescription").html(" <i style=\"color: green\" class=\"fa fa-check\"></i>")
            }
        }
        else if (response.status == 200 && response.data.description == null) {
            var id = $("#hiddenDescriptionId").val();
            if (id == response.data.id) {
                $(".productRow[data-id=" + id + "]").find(".addDescription").attr("data-description", response.data.description)
                $(".productRow[data-id=" + id + "]").find(".addDescription").html(" <i style=\"color: red\" class=\"fa fa-times\"></i>")
            }
        }
        else {
            alert("موردی یافت نشد!")
        }
        //document.location.href = "/admin/product/list";
    });
}

function addProductImageSuccess(response) {
    callbackAlert(response.message, function () {
        location.reload();
        $("#txtProductFileName").val("")
    });
}

function editor() {
    $("#txtDescription").froalaEditor({
        height: 300,
        width: '99.5%',
        language: 'fa',
        imageUploadURL: '/admin/product/upload',
        fileUploadMethod: 'POST'
    })
        .on('froalaEditor.image.removed', function (e, editor, $img) {
            console.log($img);
            $.ajax({
                method: "POST",
                url: "/admin/product/removefile",
                data: {
                    id: $img.attr('data-id'),
                    fileName: $img.attr('data-file-name')
                }
            })
        })
        .on('froalaEditor.image.uploaded', function (e, editor, response) {
            response = JSON.parse(response);

            var url = response.data.link;
            editor.image.insert(url, true, { "id": response.data.id, "file-name": response.data.fileName }, editor.image.get(), null);

            return false;
        });
}