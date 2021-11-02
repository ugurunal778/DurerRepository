$(function () {
    var rows = $(".Row"),
      content = rows.filter(".Expand");

    if (rows.css("display") === "block") {
        function reflow() {
            var height = rows.parent().height();
            rows.not(content).each(function () {
                height -= $(this).height();
            });
            content.height(height);
        }
        reflow();
        $(window).bind("load resize", reflow);
    }
});
$(window).on('load resize', function () {
    $.featherlight.defaults.afterContent = function (event) {
        this.$legend = this.$legend || $('<h1 class="image-title"/>').insertBefore(this.$content);
        this.$legend.text(this.$currentTarget.attr('alt'));
        $("#zoom-image").parent().css("cursor", "crosshair");
        if ($(".iron-steel").parent().hasClass("featherlight-inner")) {
            $(".iron-steel").parent().parent().attr("id", "small-featherlight");
        }
        else {
            $(".iron-steel").parent().attr("id", "small-featherlight");
        }
        $("#zoom-image")
            .wrap('<span style="display:inline-block"></span>')
            .css('display', 'block')
            .parent()
            .zoom();
    }
    var windowHeight = $(window).height();
    var footerHeight = $(".footer-wrapper").outerHeight();
    var topHeight = $(".top-wrapper").outerHeight();
    $(".slider-wrapper li").height(windowHeight - footerHeight - topHeight);
    if ($(window).width() > 1023) {
        var listHeight = $(".sub-links").height();
        var contentHeight = $(".content").height();
        if (listHeight < contentHeight) {
            $(".sub-links").height(contentHeight);
        }
    }
    else {
        var crClone = $(".copyright").clone();
        $(".copyright").remove();
        crClone.appendTo(".footer-wrapper");
    }
});
var count = 1;
function slide() {
    var slideCount = $(".slider-wrapper li").length;
    if (count < slideCount) {
        var next = $(".slider-wrapper li.active").next();
    }
    else {
        var next = $(".slider-wrapper li:first-child");
    }
    $(".slider-wrapper li.active").stop(true).fadeOut("slow", function () {
        $(this).removeClass("active");
        next.addClass("active").fadeIn("slow");
    });
    if (count < slideCount) {
        count++;
    }
    else {
        count = 1;
    }
};
$(document).ready(function () {
    $('nav').meanmenu({
        meanMenuContainer: '.top-wrapper',
        meanScreenWidth: '1024'
    });
    $('.combustion-chamber').featherlightGallery();
    $('.reheat-furnace').featherlightGallery();
    $('.eao').featherlightGallery();
    if ($(window).width() < 1024) {
        $(".slider-wrapper li img").each(function () {
            var a = $(this).attr("src");
            var b = "-mobile";
            var position = -4;
            var output = [a.slice(0, position), b, a.slice(position)].join('');
            console.log(output);
            $(this).attr("src", output);
        });
    }
    setInterval(slide, 3000);
    $(".news-title").click(function () {
        if ($(this).next().is(':visible')) {
            $(this).next().slideToggle();
        }
        else {
            $(".news-accordion").hide();
            $(this).next().slideToggle();
        }
    });

    $(".mobile-nav").click(function () {
        $(".navbar-wrapper").slideToggle();
    });
});