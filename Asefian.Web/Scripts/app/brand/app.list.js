$(document).ready(function () {

    $("#list").list();

    $(".search").on("keypress", function (event) {
        if (event.which == 13) {
            resetParam('word', $(this).val())
        }
    });
    
});
