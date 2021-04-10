$(document).ready(function () {
    $("#cmbSearchProductGroup").on("change", function () {
        $("#cmbSearchCategory, #cmbSearchSeries").html("<option value=\"\">...</option>");
        $("#cmbSearchCategory").fillSelect({
            url: '/api/v1/category/select/' + $("#cmbSearchProductGroup").val()
        });
    });

    $("#cmbSearchCategory").on("change", function () {
        $("#cmbSearchSeries").html("<option value=\"\">...</option>");
        $("#cmbSearchSeries").fillSelect({
            url: '/api/v1/category/select/' + $("#cmbSearchCategory").val()
        });
    });

    $("#brandSlider").mCustomScrollbar({
        theme: "minimal",
        axis: "x"
    });

    setInterval(function () {
        var position = parseInt($('#brandSlider').find(".mCSB_container").position().left) - 164;

        if ($('#brandSlider').find(".mCSB_container").width() - $('#brandSlider').width() < Math.abs(position)) {
            position = 0;
        }

        $("#brandSlider").mCustomScrollbar("scrollTo", position, { scrollInertia: 4000, scrollEasing: "linear" });
    }, 4100);

});

function showCategory(id) {
    $(".category-item").removeClass("active");
    $(".category-item[data-id=" + id + "]").addClass("active");
    boxLoader("#categoryProductList", true);
    $("#categoryProductList").list({
        url: '/api/v1/category/list/' + id,
        success: function () {
            boxLoader("#categoryProductList", false);
            $("#categorySlider").mCustomScrollbar({
                theme: "dark",
                axis: "x"
            });
        }
    });
}
