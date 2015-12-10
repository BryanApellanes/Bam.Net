if (JSUI === null || JSUI === undefined)
    alert("The core JSUI.js file was not loaded");

(function() {
    var Bacon = {};
    Bacon.topWithBacon = []; // key by jsonid
    Bacon.needsBacon = function(jsonid, options) {
        Bacon.topWithBacon.push({ jsonid: jsonid, options: options });
    }

    Bacon.sprinkle = function() {
        JSUI.forEach(Bacon.topWithBacon, function(i, needsToppings) {
            var jsonid = needsToppings.jsonid;
            var el = jQuery('[jsonid=' + jsonid + ']')[0];
            var ui = jQuery(el).attr('ui');
            if (JSUI.isNullOrUndef(ui))
                ui = jQuery(el).attr('bacon');

            jQuery(el)[ui](needsToppings.options);
        });
    }

    JSUI.Bacon = Bacon;
    JSUI.assimilate(Bacon, JSUI);
    jQuery(document).ready(JSUI.Bacon.sprinkle);
})()
