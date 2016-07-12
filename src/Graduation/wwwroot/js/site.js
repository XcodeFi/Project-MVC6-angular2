// Write your Javascript code.
$(document).ready(function () {
    $(".search-btn a").click(function () {
        $(".search-collapse").fadeOut(function () {
            $(".search-box").fadeIn();
        });
    });

    $(".search-btn-close").click(function () {
        $(".search-box").fadeOut(function () {
            $(".search-collapse").fadeIn();
        });
    });
});