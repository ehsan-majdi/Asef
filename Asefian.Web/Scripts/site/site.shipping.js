
$(document).ready(function () {
    $("#hiddenCouponCode").val('');

    $("#list").list({
        success() {
            checkAddress();
        }
    });
    getCartList();

    $("#cmbProvince").fillSelect();

    $("#cmbProvince").on("change", function () {
        var id = $(this).val();
        loadChild(id);
    });

    $("#btnSaveAddress").on("click", function (event) {
        event.preventDefault();

        $("#frmAddress").makeRequest();
    });

    $("#txtCode").on("keypress", function (event) {
        if (event.which == 13) {
            checkCode();
        }
    });

    $(document).on("click", ".add-address", function () {
        $("#frmAddress").clearEntity();
        UIkit.modal($("#modalAddress")).show();
    });

    $(document).on("change", ".address", function () {
        checkAddress();
    });

    $(document).on("change", ".delivery-type", function () {
        checkDeliveryType();
    });

    $(document).on("click", ".edit-address", function (event) {
        event.preventDefault();
        var id = $(this).attr("data-id");
        $("#frmAddress").clearEntity();
        $("#hiddenId").val(id);
        UIkit.modal($("#modalAddress")).show();
        $("#frmAddress").loadForm({
            success: function (response) {
                loadChild(response.data.provinceId, response.data.cityId);
            }
        });
    });

    $(document).on("click", ".delete-address", function (event) {
        event.preventDefault();
        var id = $(this).attr("data-id");
        $("#hiddenId").val(id);
        UIkit.modal($("#modalAddress")).show();
        $("#frmAddress").loadForm({
            success: function (response) {
                loadChild(response.data.provinceId, response.data.cityId);
            }
        });
    });

    $("#btnCheckCoupon").on("click", function (event) {
        event.preventDefault();

        checkCode();
    });

    $(".remove-coupon").on("click", function (event) {
        event.preventDefault();

        $("#hiddenCouponCode").val('');
        $("#hiddenCouponType").val('');
        $("#hiddenCouponValue").val('');

        calculatePrice();
    });

    $("#btnSubmit").on("click", function () {

        $("#alertResponse").addClass("uk-hidden");

        var entity = $("#main-page-container").getEntity();
        if (!entity.addressId) {
            dangerNotif("لطفا آدرس تحویل سفارش خود را مشخص کنید.");
            return;
        }

        if (!entity.deliveryType) {
            dangerNotif("لطفا نحوه تحویل سفارش خود را مشخص کنید.");
            return;
        }

        if (entity.deliveryType == 1 && !entity.time) {
            dangerNotif("لطفا زمان تحویل سفارش خود را مشخص کنید.");
            return;
        }

        $("#main-page-container").makeRequest();
    });

    checkDeliveryType();
    calculatePrice();
});

function calculatePrice() {
    var totalPrice = parseInt($("#hiddenTotalPrice").val());
    var discount = parseInt($("#hiddenDiscount").val());
    var price = parseInt($("#hiddenPrice").val());
    var minDeliveryPrice = parseInt($("#hiddenMinDeliveryPrice").val());
    var deliveryPrice = parseInt($("#hiddenDeliveryPrice").val());
    var couponCode = $("#hiddenCouponCode").val();
    var couponType = parseInt($("#hiddenCouponType").val());
    var couponValue = parseInt($("#hiddenCouponValue").val());

    $("#totalPrice").html(toPersianNum(toSeparator(totalPrice)));
    $("#discountPrice").html(toPersianNum(toSeparator(discount)));

    if (price >= minDeliveryPrice) {
        $("#deliveryPrice").html("رایگان");
    }
    else {
        $("#deliveryPrice").html(toPersianNum(toSeparator(deliveryPrice)) + " " + currencyName);
    }
    if (couponCode) {
        $("#discountBox").slideDown();
        $("#discountCodeBox").slideUp();

        if (couponType == amount) {
            price = price - couponValue;
            $("#couponValue").html(toPersianNum(toSeparator(couponValue)) + " " + currencyName);
        }
        else if (couponType == percentage) {
            price = price - ((price / 100) * couponValue);
            $("#couponValue").html(toPersianNum(toSeparator(couponValue)) + " درصد");
        }

        if (price < 0)
            price = 0;
    }
    else {
        $("#discountBox").slideUp();
        $("#discountCodeBox").slideDown();
    }

    $("#price").html(toPersianNum(toSeparator(price)));

}

function checkAddress() {
    $(".table-row").removeClass("active-address");
    $(".address:checked").closest(".table-row").addClass("active-address");
    var cityId = parseInt($(".address:checked").closest(".table-row").attr("data-city-id"));

    if (cityForDelivery.indexOf(cityId) == -1) {
        if ($("[name=deliveryType]:checked").val() == 1) {
            dangerNotif("ارسال سفارش از طریق پیک امکان پذیر نیست.");
        }
        $("[name=deliveryType][value=1]").prop("checked", false).prop("disabled", true).change();
        $(".delivery-man").addClass("opa50");
    }
    else {
        $("[name=deliveryType][value=1]").prop("disabled", false);
        $(".delivery-man").removeClass("opa50");
    }
}

function saveComplete(response) {
    if (response.status == 200) {
        UIkit.modal($("#modalAddress")).hide();
        $("#list").list();
    }
    else {
        dangerNotif(response.message);
    }
}

function makeOrderComplete(response) {
    if (response.status == 200) {
        document.location.href = "/invoice/payment/" + response.data.id;
    }
    else {
        $("#alertResponse").removeClass("uk-hidden");
        $("#alertResponse").find("p").html(response.message);
    }
}

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

function checkCode() {
    var entity = {
        code: $("#txtCode").val()
    }

    if (!entity.code) {
        dangerNotif("وارد کردن کد تخفیف اجباری است.");
        return;
    }

    boxLoader(".box-price", true);
    backgroundPost('/api/v1/coupon/check', entity, function (response) {
        boxLoader(".box-price", false);
        if (response.status == 200) {
            $("#hiddenCouponCode").val(entity.code);
            $("#hiddenCouponType").val(response.data.type);
            $("#hiddenCouponValue").val(response.data.value);

            $("#txtCode").val('');
            calculatePrice();
        }
        else {
            dangerNotif(response.message);
        }
    });
}

function checkDeliveryType() {
    if ($(".delivery-type:checked").val() == 1) {
        $("#listDate").removeClass("uk-hidden").stop().fadeIn();
    }
    else {
        $("#listDate").stop().fadeOut(function () {
            $("[name=time]").prop("checked", false);
            $("#listDate").addClass("uk-hidden");
        });
    }
}

function getCartList() {
    $("#listCart").list();
}
