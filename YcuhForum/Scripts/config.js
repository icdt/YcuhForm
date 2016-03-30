//config 設定
$(document).ready(function () {
    //footer置底
    $(window).bind("load", function () {
        var footer = $("footer");
        var pos = footer.position();
        var height = $(window).height();
        height = height - pos.top;
        height = height - footer.height();
        if (height > 0) {
            footer.css({
                'margin-top': height + 'px'
            });
        }
    });

    //TOP置顶
    //$(".goto-top").click(function (e) {
    //    e.preventDefault();
    //    $(document.documentElement).animate({
    //        scrollTop: 0
    //    }, 500);
    //    支持chrome
    //    $(document.body).animate({
    //        scrollTop: 0
    //    }, 500);
    //});


});