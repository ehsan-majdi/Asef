$(document).ready(function () {

    $("#frmService").loadForm();

    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();
        $("#frmService").makeRequest();
    });

    $("#txtDescription").froalaEditor({
        height: 300,
        width: '99.5%',
        language: 'fa',
        imageUploadURL: '/admin/service/upload',
        fileUploadMethod: 'POST'
    })
        .on('froalaEditor.image.removed', function (e, editor, $img) {
            console.log($img);
            $.ajax({
                method: "POST",
                url: "/admin/service/removefile",
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
        document.location.href = "/admin/service/list";
    });
}

function loadComplete(response) {
    if (response.status === 200 && response.data.fileId) {
        $('#txtDescription').froalaEditor('html.set', response.data.description);
        $("#clearFile").removeClass("hide");
        $("#upload").addClass("hide");
    }
}