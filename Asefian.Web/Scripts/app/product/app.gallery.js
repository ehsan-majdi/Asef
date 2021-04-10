$(document).ready(function () {
    getList();

    $(document).on("click", ".delete", function (event) {
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