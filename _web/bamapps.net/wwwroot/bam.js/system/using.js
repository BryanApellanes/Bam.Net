var using = (function ($, _) {
    var scriptsCallbacks = {},
        appDustTemplatesCallbacks = {},
        appScriptsCallbacks = {};

    function dustTemplates(appName, cb) {
        var sp = _.format("/bam/gettemplates?callback=using.dustTemplatesCallback('{0}')", appName);

        if (!appDustTemplatesCallbacks[appName]) {
            appDustTemplatesCallbacks[appName] = cb;
            var scr = $("<script>").attr("type", "text/javascript").attr("src", sp);
            $("head").append(scr);
        } else {
            cb(appName);
        }
    }

    function appScripts(appName, cb){
        var sp = _.format("/bam/getappscripts?appName={0}&callback=using.appScriptsCallback('{0}')", appName);

        if(!appScriptsCallbacks[appName]){
            appScriptsCallbacks[appName] = cb;
            var scr = $("<script>").attr("type", "text/javascript").attr("src", sp);
            $("head").append(scr);
        }else{
            cb(appName);
        }
    }

    function appScriptsCallback(appName){
        appScriptsCallbacks[appName](appName);
    }

    function dustTemplatesCallback(appName) {
        appDustTemplatesCallbacks[appName](appName);
    }

    function script(sn, cb) {
        var sp = _.format("/scripts/load?name={0}&callback=using.scriptCallback('{0}')", sn);

        if (!scriptsCallbacks[sn]) {
            scriptsCallbacks[sn] = cb;
            var scr = $("<script>").attr("type", "text/javascript").attr("src", sp);
            $("head").append(scr);
        } else {
            cb(sn);
        }
    }

    function scriptCallback(sn) {
        scriptsCallbacks[sn](sn);
    }

    return {
        script: script,
        scriptCallback: scriptCallback,
        dustTemplates: dustTemplates,
        dustTemplatesCallback: dustTemplatesCallback,
        appScripts: appScripts,
        appScriptsCallback: appScriptsCallback
    }
})(jQuery, _);