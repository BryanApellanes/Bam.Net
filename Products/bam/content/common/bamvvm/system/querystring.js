/*
	Copyright © Bryan Apellanes 2015  
*/
var querystring = (function () {
    "use strict";

    function get(key, query) {        
        query = query || window.location.search;

        var re = new RegExp("[?|&]" + key + "=(.*?)&"),
            matches = re.exec(query + "&");

        if (!matches || matches.length < 2) {
            return "";
        }

        return decodeURIComponent(matches[1].replace("+", " "));
    }

    function set(key, query) {
        query = query || window.location.search;

        var q = query + "&",
            re = new RegExp("[?|&]" + key + "=.*?&");

        if (!re.test(q)) {
            q += key + "=" + encodeURI(value);
        } else {
            q = q.replace(re, "&" + key + "=" + encodeURIComponent(value) + "&");
        }

        q = q.trimStart("&").trimEnd("&");

        return q[0] == "?" ? q : q = "?" + q;
    }

    $(function () {
        _.mixin({
            getQueryString: get,
            setQueryString: set
        });
    });

    return {
        get: get,
        set: set
    }
})();