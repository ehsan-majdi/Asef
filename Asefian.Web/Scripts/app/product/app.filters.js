$(document).ready(function () {
    loadFilters();

    $("#btnSubmit").on("click", function (event) {
        event.preventDefault();

        saveFilters();
    })
});


function loadFilters() {
    boxLoader("#container", true);
    backgroundGet('/admin/product/getFilterValue/' + $("#hiddenId").val(), {}, function (response) {
        boxLoader("#container", false);
        if (response.status == 200) {
            for (var i = 0; i < response.data.length; i++) {
                var item = response.data[i];
                var element = $(".hiddenFilterId[value=" + item.productFilterId + "]").closest(".item");

                var filterId = $(element).find(".hiddenFilterId").val();
                var filterType = parseInt($(element).find(".hiddenFilterType").val());

                switch (filterType) {
                    case 1:
                        $(element).find("[name=datavalue]").val(item.valueList[0].value);
                        break;
                    case 2:
                    case 3:
                        for (var j = 0; j < item.valueList.length; j++) {
                            $(element).find("[name=datavalue][value=" + item.valueList[j].productFilterValueId + "]").prop("checked", true);
                        }
                        break;
                    case 4:
                        $(element).find(".valueBoolean[value=" + item.valueList[0].valueBoolean + "]").prop('checked', true);
                        break;
                }
            }
        }
        else {
            alert(response.message);
        }
    });
}

function saveFilters() {
    var entity = [];

    $(".item").each(function (index, element) {
        var filterId = $(element).find(".hiddenFilterId").val();
        var filterType = parseInt($(element).find(".hiddenFilterType").val());

        switch (filterType) {
            case 1:
                entity.push({ productFilterId: filterId, valueList: [{ value: $(element).find("[name=datavalue]").val() }] });
                break;
            case 2:
            case 3:
                var valueList = []
                $(element).find("[name=datavalue]:checked").each(function () {
                    valueList.push({ productFilterValueId: $(this).val() });
                });

                entity.push({ productFilterId: filterId, valueList: valueList });
                break;
            case 4:
                entity.push({ productFilterId: filterId, valueList: [{ valueBoolean: $(element).find(".valueBoolean:checked").val() }] });
                break;
        }
    });

    boxLoader("#container", true);
    backgroundPost('/admin/product/setFilterValue/' + $("#hiddenId").val(), { model: entity }, function (response) {
        boxLoader("#container", false);
        if (response.status == 200) {
            callbackAlert(response.message, function () {
                document.location.href = "/admin/product/list";
            })
        }
        else {
            alert(response.message);
        }
    });
}