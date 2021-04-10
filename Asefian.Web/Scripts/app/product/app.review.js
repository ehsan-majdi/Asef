$(document).ready(function () {

    $("#frmReview").loadForm({
        success: function (response) {
            $('#txtReview').froalaEditor('html.set', response.data.review);
        }
    });

    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();
        $("#frmReview").makeRequest();
    });

    $("#txtReview").froalaEditor({
        height: 500,
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

});

function saveComplete(response) {
    document.location.href = "/admin/product/list";
}
