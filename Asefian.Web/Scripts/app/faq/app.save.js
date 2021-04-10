$(document).ready(function () {

    $("#frmFaq").loadForm({
        success: function (response) {
            $('#lnkParentList').attr('href', "/admin/faq/list/" + response.data.faqCategoryId);
            $('#txtAnswer').froalaEditor('html.set', response.data.answer);
        }
    });

    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();
        $("#frmFaq").makeRequest();
    });

    $("#txtAnswer").froalaEditor({
        height: 300,
        width: '99.5%',
        language: 'fa',
        imageUploadURL: '/admin/faq/upload',
        fileUploadMethod: 'POST'
    })
        .on('froalaEditor.image.removed', function (e, editor, $img) {
            console.log($img);
            $.ajax({
                method: "POST",
                url: "/admin/faq/removefile",
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
    document.location.href = "/admin/faq/list/" + $("[name=faqCategoryId]").val();
}
