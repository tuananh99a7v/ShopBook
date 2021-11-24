(function ($) {
    jQuery.fn.CE = function () {

        var args = Array.prototype.slice.call(arguments);

        if (args.length == 1 && typeof (args[0]) == "object") {
            build.call(this, args[0]);
        }
    };

    function build(options) {
        this.each(function () {
            CreateCE(this, options.maxHeight);
        });
    }

    function CreateCE(elementCE, maxHeight) {
        if ($(elementCE).height() >= maxHeight + 12) {
            $(elementCE).wrap("<div class='ContentReadMore'></div>");
            $(elementCE).parent().wrap("<div class='divContentReadMore'></div>");
            var divContentReadMore = $(elementCE).parent().parent();
            divContentReadMore.append("<a class='aReadMore' ><img src='/LibraryClient/CE/expanded.png'/></a>");

            if (maxHeight == 0)
                maxHeight = parseInt(divContentReadMore.css("max-height"));

            divContentReadMore.css("max-height", maxHeight);
            divContentReadMore.find(".aReadMore").css('display', 'block');
            divContentReadMore.find(".aReadMore").css('position', 'absolute');
            divContentReadMore.find(".aReadMore").attr("title", "");
            var color = divContentReadMore.parent().css("background-color");

            if ($.trim(color) == "rgba(0, 0, 0, 0)")
                color = "White";
            divContentReadMore.find(".aReadMore").css("background", "linear-gradient(#ffffff36," + color + ")");
            CreateTooltip(divContentReadMore.find(".aReadMore"), divContentReadMore.find(".ContentReadMore").html());

            divContentReadMore.children().each(function () {
                if (!$(this).hasClass("aReadMore")) {
                    $(this).css('line-height', '20px');
                    $(this).children().each(function () {
                        if (!$(this).hasClass("aReadMore")) {
                            $(this).css('line-height', '20px');
                        }
                    });
                }
            });
            var aReadMore = $(elementCE).parent().parent().children(".aReadMore");

            aReadMore.click(function (e) {
                var td = divContentReadMore.parent("td");
                if (td != null)
                    td.parent("tr").children().each(function () {
                        var divContentReadMore = $(this).find(".divContentReadMore");
                        if (divContentReadMore != null && divContentReadMore != $(this))
                            ReadMore_Click(divContentReadMore, maxHeight);
                    });
                else
                    ReadMore_Click($(this), maxHeight);
            });
            divContentReadMore.click(function (e) {
                if (e.ctrlKey) {
                    var divContentReadMorePatenr = $(this).parent("td").find(".divContentReadMore");
                    var coll = false;
                    if (divContentReadMorePatenr != null && divContentReadMorePatenr != $(this)) {
                        var content = divContentReadMorePatenr.find("img").attr('src');
                        if (divContentReadMorePatenr.find("img").attr("src") == "/LibraryClient/CE/expanded.png") {
                            coll = true;
                        }
                    }
                    $(this).parent("td").parent().parent().children().each(function () {
                        var divContentReadMore = $(this).find(".divContentReadMore");
                        if (divContentReadMore != null && divContentReadMore != $(this) && CheckCollapsed(divContentReadMore) == coll)
                            ReadMore_Click(divContentReadMore, maxHeight);
                    });
                }
            });
        }
    }

    function ReadMore_Click(e, maxHeight) {
        if (e.parent().find("img").attr("src") == "/LibraryClient/CE/expanded.png") {
            e.css('max-height', 'none');
            e.css('height', 'auto');
            e.parent().find(".aReadMore img").attr("src", "/LibraryClient/CE/collapsed.png");
            e.find(".aReadMore").css("background", "none");
            CreateTooltip(e.parent().find(".aReadMore"), "", 0, 0);
        }
        else {
            e.css('max-height', maxHeight);
            e.css('height', 'none');
            e.parent().find(".aReadMore img").attr("src", "/LibraryClient/CE/expanded.png");
            var color = e.parent().css("background-color");

            if ($.trim(color) == "rgba(0, 0, 0, 0)")
                color = "White";
            e.find(".aReadMore").css("background", "linear-gradient(#ffffff36," + color + ")");
            var content = e.find(".ContentReadMore").html();
            CreateTooltip(e.parent().find(".aReadMore"), content);
        }
    }

    function CreateTooltip(control, contentTooltip) {
        $(control).tooltip({
            content: contentTooltip,
            track: true,
            open: function (event, ui) {
                ui.tooltip.css("max-width", "600px");
                ui.tooltip.css("position", "fixed");
                ui.tooltip.css("white-space", "normal");
            }
        });
    }

    function CheckCollapsed(e) {
        if (e.parent().find("img").attr("src") == "/LibraryClient/CE/expanded.png") {
            return true;
        }
        else {
            return false;
        }
    }
})(jQuery);