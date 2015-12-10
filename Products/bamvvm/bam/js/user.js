var user = (function(){
    function ctor(name, isAuth){
        this.name = name;

        this.isAuthenticated = isAuth || false;
    }

    return ctor;
})(jQuery, _);