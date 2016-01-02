var using = (function ($, _) {
    var scripts = {},
        appDustTemplates = {};

    function dustTemplates(appName, cb) {
        var sp = _.format("/dust/bamtemplates?appName={0}&callback=using.dustTemplatesCallback('{0}')", appName);

        if (!appDustTemplates[appName]) {
            appDustTemplates[appName] = cb;
            var scr = $("<script>").attr("type", "text/javascript").attr("src", sp);
            $("head").append(scr);
        } else {
            cb(appName);
        }
    }

    function dustTemplatesCallback(appName) {
        appDustTemplates[appName](appName);
    }

    function script(sn, cb) {
        var sp = _.format("/scripts/load?name={0}&callback=using.scriptCallback('{0}')", sn);

        if (!scripts[sn]) {
            scripts[sn] = cb;
            var scr = $("<script>").attr("type", "text/javascript").attr("src", sp);
            $("head").append(scr);
        } else {
            cb(sn);
        }
    }

    function scriptCallback(sn) {
        scripts[sn](sn);
    }

    return {
        script: script,
        scriptCallback: scriptCallback,
        dustTemplates: dustTemplates,
        dustTemplatesCallback: dustTemplatesCallback
    }
})(jQuery, _);