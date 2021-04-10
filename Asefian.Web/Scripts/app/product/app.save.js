$(document).ready(function () {
    editor()
    
    $("#cmbCategory").fillSelect({
        callback: function (response) {
            $("#cmbBrand").fillSelect({
                callback: function (response) {
                    $("#frmProduct").find('input, textarea, button, select').prop('disabled', false);
                    $("#frmProduct").loadForm();
                }
            });

        }
    });

    //$("#cmbCategory").on("change", function () {
    //    if ($("#cmbCategory").val()) {
    //        $("#frmProduct").find('input, textarea, button, select').prop('disabled', true);

    //    }
    //});

    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();
        $("#frmProduct").makeRequest();
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

    $(".txtDescription").froalaEditor({
        height: 300,
        width: '99.5%',
        language: 'fa',
        imageUploadURL: '/admin/site/upload',
        fileUploadMethod: 'POST'
    })
        .on('froalaEditor.image.removed', function (e, editor, $img) {
            console.log($img);
            $.ajax({
                method: "POST",
                url: "/admin/site/removefile",
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
});

function saveComplete(response) {
    callbackAlert(response.message, function () {
        document.location.href = "/admin/product/list";
    })
}

function loadComplete(response) {
    if (response.status === 200) {
        //$("#cmbCategory").fillSelect({
        //    url: '/admin/category/loadchild/' + response.data.groupId,
        //    selected: response.data.categoryId
        //});


        $("#cmbCategoryFeature").fillSelect({
            url: '/admin/category/featureSelect/' + response.data.categoryId,
            selected: response.data.categoryFeatureId
        });

        if (response.data.fileId) {
            $('#txtDescription').froalaEditor('html.set', response.data.description);
            $("#clearFile").removeClass("hide");
            $("#upload").addClass("hide");
        }
    }
}

function editor() {
    $("#txtDescription").froalaEditor({
        height: 300,
        width: '99.5%',
        language: 'fa',
        imageUploadURL: '/admin/news/upload',
        fileUploadMethod: 'POST'
    })
        .on('froalaEditor.image.removed', function (e, editor, $img) {
            console.log($img);
            $.ajax({
                method: "POST",
                url: "/admin/news/removefile",
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