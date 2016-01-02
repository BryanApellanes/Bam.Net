var models = (function($, _, b, d, q, w){
    var sources = {};

    function setModel(name, data){
        sources[name] = data;
    }

    function getModel(name){
        return sources[name];
    }

    return function(opts){
        var config = {

        };

        $.extend(config, opts);

        return {
            getModel: getModel,
            setModel: setModel
        }
    }
})(jQuery, _, bam, dao, qi, window || {});