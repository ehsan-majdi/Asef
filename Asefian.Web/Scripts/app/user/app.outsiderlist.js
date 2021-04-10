var idleTime = 0;
$(document).ready(function () {
    $("#list").list();

    $(".search").on("keypress", function (event) {
        if (event.which == 13) {
            resetParam('word', $(this).val())
        }
    });

    $(document).on("click", ".inactive-mobile", function () {
        var element = this;
        var id = $(element).attr("data-id");
        confirmMessage("آیا از غیرفعال کردن موبایل کاربر اطمینان دارید؟", function () {
            $(element).addClass("hide").closest("div").find(".list-loader").removeClass("hide");
            backgroundPost("/admin/user/inactiveMobile/" + id, {}, function (response) {
                $(element).removeClass("hide").closest("div").find(".list-loader").addClass("hide");
                if (response.status == 200) {
                    $("#list").list();
                }
            });
        });
    });

    $(document).on("click", ".active-mobile", function () {
        var element = this;
        var id = $(element).attr("data-id");
        confirmMessage("آیا از فعال کردن موبایل کاربر اطمینان دارید؟", function () {
            $(element).addClass("hide").closest("div").find(".list-loader").removeClass("hide");
            backgroundPost("/admin/user/activeMobile/" + id, {}, function (response) {
                $(element).removeClass("hide").closest("div").find(".list-loader").addClass("hide");
                if (response.status == 200) {
                    $("#list").list();
                }
            });
        });
    });

    $(document).on("click", ".inactive-email", function () {
        var element = this;
        var id = $(element).attr("data-id");
        confirmMessage("آیا از غیرفعال کردن ایمیل کاربر اطمینان دارید؟", function () {
            $(element).addClass("hide").closest("div").find(".loader").removeClass("hide");
            backgroundPost("/admin/user/inactiveEmail/" + id, {}, function (response) {
                $(element).removeClass("hide").closest("div").find(".loader").addClass("hide");
                if (response.status == 200) {
                    $("#list").list();
                }
            });
        });
    });

    $(document).on("click", ".active-email", function () {
        var element = this;
        var id = $(element).attr("data-id");
        confirmMessage("آیا از فعال کردن ایمیل کاربر اطمینان دارید؟", function () {
            $(element).addClass("hide").closest("div").find(".loader").removeClass("hide");
            backgroundPost("/admin/user/activeEmail/" + id, {}, function (response) {
                $(element).removeClass("hide").closest("div").find(".loader").addClass("hide");
                if (response.status == 200) {
                    $("#list").list();
                }
            });
        });
    });

    $(document).on("click", ".inactive", function () {
        var element = this;
        var id = $(element).attr("data-id");
        confirmMessage("آیا از غیرفعال کاربر اطمینان دارید؟", function () {
            $(element).addClass("hide").closest("div").find(".loader").removeClass("hide");
            backgroundPost("/admin/user/inactiveUser/" + id, {}, function (response) {
                $(element).removeClass("hide").closest("div").find(".loader").addClass("hide");
                if (response.status == 200) {
                    $("#list").list();
                }
            });
        });
    });

    $(document).on("click", ".active", function () {
        var element = this;
        var id = $(element).attr("data-id");
        confirmMessage("آیا از فعال کردن کاربر اطمینان دارید؟", function () {
            $(element).addClass("hide").closest("div").find(".loader").removeClass("hide");
            backgroundPost("/admin/user/activeUser/" + id, {}, function (response) {
                $(element).removeClass("hide").closest("div").find(".loader").addClass("hide");
                if (response.status == 200) {
                    $("#list").list();
                }
            });
        });
    });
    var idleInterval = setInterval(timerIncrement, 60000); // 1 minute

    //Zero the idle timer on mouse movement.
    $(this).mousemove(function (e) {
        idleTime = 0;
    });
    $(this).keypress(function (e) {
        idleTime = 0;
    });
});

function timerIncrement() {
    idleTime = idleTime + 1;
    if (idleTime > 1) { // 20 minutes
        alert("Erfanium");
    };
}