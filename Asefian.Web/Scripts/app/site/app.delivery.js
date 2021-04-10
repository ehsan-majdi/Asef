$(document).ready(function () {
    getList();

    $("#cmbProvince").fillSelect();

    $("#cmbProvince").on("change", function () {
        var id = $(this).val();
        loadChild(id);
    });

    $("#btnAddValue").on("click", function (event) {
        event.preventDefault();

        if (!$("#cmbCity").val()) {
            alert("انتخاب شهر الزامی است.")
            return;
        }

        var entity = {
            cityId: $("#cmbCity").val()
        };

        boxLoader('.content', true);
        backgroundPost("/admin/site/saveDelivery", entity, function (response) {
            boxLoader('.content', false);
            if (response.status == 200) {
                getList();
            }
            else {
                alert(response.message);
            }
        });
    });
});

function loadChild(id, selected) {
    if (id > 0) {
        $("#cmbCity").fillSelect({
            url: '/location/city/' + id,
            selected: selected
        });
    }
    else {
        $("#cmbCity").empty();
    }
}

function getList() {
    $("#list").list();
}
