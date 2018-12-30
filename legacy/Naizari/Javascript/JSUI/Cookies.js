var Cookies = {};
Cookies.setCookie = function(strName, value, expireAfterDays){
    var expires = "";
    if (expireAfterDays) {
	    var date = new Date();
	    date.setTime(date.getTime()+(expireAfterDays*24*60*60*1000));
	    expires = "; expires=" + date.toGMTString();
    }
    
    document.cookie = strName + "=" + value + expires + "; path=/";
}

Cookies.clearCookie = function(strName) {
    Cookies.setCookie(strName, "", -1);
}

Cookies.getCookie = function(strName){
    var cookieArr = document.cookie.split(';');
    for(var i = 0; i < cookieArr.length; i++){
        var nameValuePair = cookieArr[i].split('=');
        if( nameValuePair[0].trim() == strName )
            return nameValuePair[1].trim();
    }
    
    return "";
}

if (JSUI !== null && JSUI !== undefined) {
    JSUI.assimilate(Cookies);
}