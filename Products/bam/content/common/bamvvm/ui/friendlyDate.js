/*
	Copyright © Bryan Apellanes 2015  
*/
(function ($) {
    "use strict";
    function getDate(el) {
        var dataset = $(el).data("dataset") || $(el).dataSet(),
            month = dataset.mo,
            day = dataset.dd,
            year = dataset.yyyy,
            hour = dataset.hh,
            minute = dataset.mm,
            second = dataset.ss,
            date = new Date();
        date.setUTCMonth(month - 1, day);
        date.setUTCFullYear(year);
        date.setUTCHours(hour, minute, second, 0);
        return date;
    }

    $.fn.date = function () {
        return this.each(function (i, v) {
            var setter = dataset.setDate || "text",
                format = dataset.format || "toLocaleString";
            var val = getDate(v);
            $(v)[setter](val[format]());
        });
    };

    function friendlyDate(date) {
        var diff = (((new Date()).getTime() - date.getTime()) / 1000),
            day_diff = Math.floor(diff / 86400);

        if (isNaN(day_diff) || day_diff < 0 || day_diff >= 31)
            return date.toLocaleString();

        return day_diff == 0 && (
            diff < 60 && "just now" ||
                diff < 120 && "1 minute ago" ||
                diff < 3600 && Math.floor(diff / 60) + " minutes ago" ||
                diff < 7200 && "1 hour ago" ||
                diff < 86400 && Math.floor(diff / 3600) + " hours ago") ||
            day_diff == 1 && "Yesterday" ||
            day_diff < 7 && day_diff + " days ago" ||
            day_diff < 31 && Math.ceil(day_diff / 7) + " weeks ago";
    }

    $.fn.friendlyDate = function () {
        return $(this).each(function () {
            var date = getDate($(this)),
                friendly = friendlyDate(date);
            $(this).text(friendly);
        });
    }
})(jQuery);