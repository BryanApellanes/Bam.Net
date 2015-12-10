(function () {
    String.prototype.trimEnd = function (c) {
        if (c)
            return this.replace(new RegExp(c.escapeRegExp() + "*$"), '');
        return this.replace(/\s+$/, '');
    }
    String.prototype.trimStart = function (c) {
        if (c)
            return this.replace(new RegExp("^" + c.escapeRegExp() + "*"), '');
        return this.replace(/^\s+/, '');
    }

    String.prototype.escapeRegExp = function () {
        return this.replace(/[.*+?^${}()|[\]\/\\]/g, "\\$0");
    };

    JSUI.getQueryStringValue = function (key, query) {
        if (!query)
            query = window.location.search;
        var re = new RegExp("[?|&]" + key + "=(.*?)&");
        var matches = re.exec(query + "&");
        if (!matches || matches.length < 2)
            return "";
        return decodeURIComponent(matches[1].replace("+", " "));
    }
    JSUI.setQueryStringValue = function (key, value, query) {

        query = query || window.location.search;
        var q = query + "&";
        var re = new RegExp("[?|&]" + key + "=.*?&");
        if (!re.test(q))
            q += key + "=" + encodeURI(value);
        else
            q = q.replace(re, "&" + key + "=" + encodeURIComponent(value) + "&");
        q = q.trimStart("&").trimEnd("&");
        return q[0] == "?" ? q : q = "?" + q;
    }
})()