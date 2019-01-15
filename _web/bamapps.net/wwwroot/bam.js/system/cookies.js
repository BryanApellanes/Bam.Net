/*
	Copyright © Bryan Apellanes 2015  
*/
var cookies = (function () {
    function set(strName, value, expireAfterDays) {
        var expires = "";
        if (expireAfterDays) {
            var date = new Date();
            date.setTime(date.getTime() + (expireAfterDays * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        }

        document.cookie = strName + "=" + value + expires + "; path=/";
    }

    function clear(strName) {
        cookies.set(strName, "", -1);
    }

    function get(strName) {
        var cookieArr = document.cookie.split(';');
        for (var i = 0; i < cookieArr.length; i++) {
            var nameValuePair = cookieArr[i].split('=');
            if (nameValuePair[0].trim() == strName)
                return nameValuePair[1].trim();
        }

        return "";
    }

    var val = {
        set: set,
        clear: clear,
        get: get
    };
    $(function () {
        _.mixin({
			setCookie: set,
			clearCookie: clear,
			getCookie: get
		});
    });

    return val;
})();



