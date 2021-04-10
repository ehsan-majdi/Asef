
$(document).ready(function () {
    $(window).scroll(function () {
        if ($(document).scrollTop() > 0) {
            $('.language-type').hide();
            $('#txtSearch').hide();
        } else {
            $('.language-type').show();
            $('#txtSearch').show();
        }
    });
    $(".bold-icon").find("a svg").css({ "width": "20", "height": "40" , "color" : "black" });
    $(".bold-icon").find("a svg").css({ "width": "20", "height": "40", "color": "black" });
    $(".nav-menu").click(function (event) {
        $(this).next('.showHideList').slideToggle();
    }
    );
    $('#nav-icon').click(function () {
        $(this).toggleClass('open');
    });
    //$("body").on("contextmenu", function (e) {
    //    return false;
    //});
    $(".button-search").on("click", function () {
        $(".txtHeaderSearch").fadeToggle(500);
    });
    $("#category-menu").mCustomScrollbar({
        theme: "dark"
    });

    $(".txtHeaderSearch").on("keypress", function (e) {
        if (e.which == 13) {
            e.preventDefault();
            var term = $(".txtHeaderSearch").val();
            document.location.href = "/fa/search?term=" + term;
        }
    });

    if ($("#phoneNumber").html()) {
        $("#lnkMakeCall").removeClass("uk-hidden");
        $("#lnkMakeCall").attr("href", "tel:" + $("#phoneNumber").attr("data-value"))
    }
    else {
        $("#lnkMakeCall").remove();
    }

    if (latitude && longitude) {
        var map = L.map('map').setView([latitude, longitude], 15);

        L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
            maxZoom: 17,
            minZoom: 14,
            attribution: '',
            id: 'mapbox.streets'
        }).addTo(map);

        L.marker([latitude, longitude]).addTo(map)
            .openPopup();
    }

});


function getProductImage(item) {
    if (item.fileId) {
        return '/upload/file/' + item.fileId + '/' + item.fileName;
    }
    else {
        return "/images/no-image.png";
    }
}

function addToInquiry(id) {
    backgroundPost('/inquiry/addToInquiry', { id: id }, function (response) {
        if (response.status == 200) {
            if (typeof refreshInquiryCart == 'function') {
                refreshInquiryCart();
            }
        }
        notif(response.message);
    });
}