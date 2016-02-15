/*
	Copyright © Bryan Apellanes 2015  
*/
window.fbAsyncInit = function() {
    FB.init({
        appId      : FbApplicationId,
        status     : true,
        xfbml      : true
    });

    FB.Event.subscribe('auth.authResponseChange', function(response){
        if(response.status === 'connected'){

            //$("#mainLoginLink").text("")
        }
    });
};

(function(d, s, id){
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) {return;}
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/all.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));
