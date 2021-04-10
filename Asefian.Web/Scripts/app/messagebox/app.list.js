$(document).ready(function () {
   
    $("#list").list();
    setParam('typeId',3)
    $(".search").on("keypress", function (event) {
        if (event.which == 13) {
            resetParam('word', $(this).val())
        }
    });

    $(document).on("click", ".view-message", function (event) {
        event.preventDefault();
        var id = $(this).attr("data-id");
        backgroundGet("/admin/messagebox/getmessage/" + id, {}, function (response) {
            if (response.status === 200) {
                $("#hiddenId").val(id);
                $("#messageBox").html(response.data.text);
                UIkit.modal($("#message-modal")).show();
            }
            else {
                alert(response.message);
            }
        });
    });

    $("#btnRead").on("click", function (event) {
        event.preventDefault();
        var id = $("#hiddenId").val();
        confirmMessage("آیا از ثبت وضعیت خوانده شده برای این پیام اطمینان دارید؟", function () {
            backgroundPost('/admin/messagebox/read/' + id, null, function (response) {
                if (response.status == 200) {
                    UIkit.modal($("#message-modal")).hide();
                }
            });
        });
    });

});

