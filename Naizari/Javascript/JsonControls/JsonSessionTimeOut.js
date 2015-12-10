if(JSUI === undefined || JSUI === null)
    alert("The core JSUI.js file was not loaded");
    
var Session = {};
Session.Timeout = 20;
Session.TimeoutMessage = "Your session has timed out.  Please restart your browser.";
Session.TimerId = null;
Session.LogOutPage = "LogOut.aspx";

Session.expire = function() {
    Cookies.clearCookie("ASP.NET_SessionId");
    location.href = Session.LogOutPage;
}

Session.expireSessionInMinutes = function(intMinutes){
    if(!intMinutes)
        intMinutes = Session.Timeout;
    
    if(Session.TimerId)
        clearInterval(Session.TimerId);
    
    var milliseconds = 1000 * 60 * intMinutes;
    Session.Timeout = intMinutes;            
    Session.TimerId = setTimeout(Session.expire, milliseconds); 
}

Session.reset = function() {
    Session.expireSessionInMinutes(Session.Timeout);
}
