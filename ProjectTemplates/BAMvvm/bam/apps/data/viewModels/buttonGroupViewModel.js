function buttonGroupViewModel(el, app) {
    var buttons = new templates.buttongroupModel();
    buttons.addItem("feed", function(){
        app.navigateTo("feed");
    });
    buttons.addItem("people", function(){
        app.navigateTo("people");
    });
    buttons.addItem("items", function(){
        app.navigateTo("items");
    });

    buttons.renderIn(el);
}