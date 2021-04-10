
$(document).ready(function () {

    getList();

    $("#btnAddValue").on("click", function (event) {
        event.preventDefault();

        var entity = {
            categoryFeatureId: $("#hiddenId").val(),
            title: $("#txtValue").val()
        }

        if (!entity.title) {
            alert("وارد کردن عنوان اجباری است.");
            return;
        }

        backgroundPost('/admin/category/saveValueList', entity, function (response) {
            if (response.status == 200) {
                callbackAlert(response.message, function () {
                    $("#txtValue").val('');
                    getList();
                });
            }
            else {
                alert(response.message);
            }
        });

    });

    $("#btnSaveRearrange").on("click", function (event) {
        event.preventDefault();

        var entity = {
            id: $("#hiddenId").val(),
            valueList: []
        };

        $("#valueList").find(".value-item").each(function (index, element) {
            entity.valueList.push({ order: index + 1, id: $(element).attr("data-id") });
        });

        backgroundPost('/admin/category/rearrangeValueList', entity, function (response) {
            if (response.status == 200) {
                callbackAlert(response.message, function () {
                    $("#btnSaveRearrange").addClass("uk-hidden");

                    getList();
                });
            }
            else {
                alert(response.message);
            }
        });
    });

});

function getList() {
    $("#list").list({
        success: function () {

            UIkit.util.on('#valueList', 'stop', function () {
                $("#btnSaveRearrange").removeClass("uk-hidden");
                console.log(123)
            });
        }
    });
}