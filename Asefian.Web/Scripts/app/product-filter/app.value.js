
$(document).ready(function () {

    getList();

    $("#btnAddValue").on("click", function (event) {
        event.preventDefault();

        //var entity = {
        //    productFilterId: $("#hiddenId").val(),
        //    value: $("#txtValue").val()
        //}

        //if (!entity.value) {
        //    alert("وارد کردن عنوان اجباری است.");
        //    return;
        //}

        //backgroundPost('/admin/product/Filter/saveValueList', entity, function (response) {
        //    if (response.status == 200) {
        //        callbackAlert(response.message, function () {
        //            $("#txtValue").val('');
        //            getList();
        //        });
        //    }
        //    else {
        //        alert(response.message);
        //    }
        //});
        $("#frmProductFilterValue").makeRequest();
    });

    $(document).on("click", ".active", function () {
        var id = $(this).closest(".value-item").attr("data-id");

        confirmMessage("آیا از فعال کردن این مقدار اطمینان دارید؟", function () {
            backgroundPost('/admin/productFilter/activeFilterValue/' + id, {}, function (response) {
                if (response.status == 200) {
                    $("#btnSaveRearrange").addClass("uk-hidden");
                    getList();
                }
                else {
                    alert(response.message);
                }
            });
        });
    });

    $(document).on("click", ".inactive", function () {
        var id = $(this).closest(".value-item").attr("data-id");

        confirmMessage("آیا از غیر فعال کردن این مقدار اطمینان دارید؟", function () {
            backgroundPost('/admin/productFilter/inactiveFilterValue/' + id, {}, function (response) {
                if (response.status == 200) {
                    $("#btnSaveRearrange").addClass("uk-hidden");
                    getList();
                }
                else {
                    alert(response.message);
                }
            });
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

        backgroundPost('/admin/productFilter/rearrangeValueList', entity, function (response) {
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

function saveComplete(response) {
    callbackAlert(response.message, function () {
        $(".txtValue").val('');
        getList()
    })
}

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