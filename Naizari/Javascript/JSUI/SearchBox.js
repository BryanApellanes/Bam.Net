if (JSUI === null || JSUI === undefined)
    alert("The core JSUI.js file was not loaded");
if (jQuery === null || jQuery === undefined)
    alert("jQuery was not loaded");

JSUI.searchBox = function(txtBox, proxy, methodname, options, onselect) {
    if (JSUI.isNullOrUndef(onselect))
        onselect = function() { };
    var config = {
        textbox: JSUI.GetElement(txtBox),
        onselect: onselect,
        minchars: 3,
        template: "default",
        orientation: "BottomCenter",
        highlight: "#0092B5",
        overlap: -5,
        width: 350,
        height: null,
        working: null,
        notworking: null,
        resetonselect: true,
        clearonreset: true
    };

    if (JSUI.isFunction(options))
        config.onselect = options;
    else if (JSUI.isObject(options))
        JSUI.assimilate(options, config);

    if (JSUI.isString(config.working))
        config.working = JSUI.getElement(config.working);
    if (JSUI.isString(config.notworking))
        config.notworking = JSUI.getElement(config.notworking);
    config.proxy = proxy;
    config.methodname = methodname;

    var results = document.createElement('div'); //JSUI.getElement(resultsDiv);

    results.style.width = config.width + "px";
    if (config.height !== null)
        results.style.height = config.height + "px";

    results.style.display = "none";
    results.textbox = config.textbox;
    results.style.backgroundColor = "#FFFFFF";
    results.style.border = "1px solid #000000";
    results.id = JSUI.randomString(8);
    results.searcher = this;
    results.config = config;
    JSUI.getDocumentBody().appendChild(results);

    results.currentInput = "";

    JSUI.orient(results, { reference: config.textbox, orientation: config.orientation, overlap: config.overlap });


    jQuery(config.textbox).keyup(function(e) {
        if (config.textbox.value.length >= config.minchars && config.textbox.value != results.currentInput && e.keyCode != 13) {
            results.currentInput = config.textbox.value;
            if (config.working !== null) {
                jQuery(config.working).show();
            }
            if (config.notworking !== null) {
                jQuery(config.notworking).hide();
            }
            config.proxy[config.methodname + "Ex"](config.textbox.value, { format: "html", target: results, template: config.template }, function(resultsEl) {
                JSUI.clearEventHandlers(config.textbox, "keyup");
                JSUI.clearEventHandlers(config.textbox, "keydown");
                JSUI.clearEventHandlers(config.textbox, "keypress");
                JSUI.clearEventHandlers(config.textbox, "blur");
                resultsEl.arrowList = new ArrowList.ArrowListClass(resultsEl, resultsEl.textbox, "div");
                resultsEl.arrowList.HighlightBackgroundColor = resultsEl.config.highlight;
                resultsEl.arrowList.ResetOnSelect = resultsEl.config.resetonselect;
                resultsEl.arrowList.Initialize();
                var tb = resultsEl.config.textbox;
                resultsEl.arrowList.AddResetListener(function(e) {
                    if (resultsEl.config.clearonreset)
                        tb.value = "";
                    jQuery(resultsEl).hide();
                });
                JSUI.orient(resultsEl, { reference: resultsEl.config.textbox, orientation: resultsEl.config.orientation, overlap: resultsEl.config.overlap });
                JSUI.toFront(resultsEl);
                resultsEl.arrowList.AddSelectListener(resultsEl.config.onselect);
                jQuery(resultsEl).show();
                if (config.working !== null) {
                    jQuery(config.working).hide();
                }
                if (config.notworking !== null) {
                    jQuery(config.notworking).show();
                }
            });
        }
    });
} 

 