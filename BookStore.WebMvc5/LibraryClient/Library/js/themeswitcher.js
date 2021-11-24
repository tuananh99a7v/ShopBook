/* Admin layout options */

$(document).on('ready', function () {

    $('#theme-switcher-wrapper .switch-toggle').on('click', this, function () {
        var dataToggle = $(this).prev().attr('data-toggletarget');
        $('body').toggleClass(dataToggle);

        if (dataToggle = 'closed-sidebar') {
            $('#close-sidebar .glyph-icon').toggleClass('icon-angle-right').toggleClass('icon-angle-left');
        }
    });

    $('.switch-toggle[id="#boxed-layout"]').click(function () {

        if ($('#boxed-layout').attr("checked")) {
            $('.boxed-bg-wrapper').slideDown();
        } else {
            $('.boxed-bg-wrapper').slideUp();
        }

    });
});

/* Open theme switcher */

$(function () {

    $(".theme-switcher").click(function () {
        $("#theme-options").toggleClass('active');
    });

});

function swither_resizer() {
    $('#theme-switcher-wrapper').height($(window).height() * 0.9);
}

$(document).on('ready', function () {
    swither_resizer();
});

$(window).on('resize', function () {
    swither_resizer();
});