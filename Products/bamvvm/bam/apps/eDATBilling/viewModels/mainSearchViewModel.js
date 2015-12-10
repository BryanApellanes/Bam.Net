function errorHandler(e){
    $("#errorMessages").text(e);
}

var mainSearchViewModel = {
    searchProvider: {
        search: function(q, process, type){
            if(_.isUndefined(type) || _.isNull(type)){
                type = "all";
            }

            _.act("data", "search", { query: q, count: 8, type: type })
                .done(function (r) {
                    if (r.Success) {
                        var items = [];
                        _.each(r.Data, function(item){
                            items.push(JSON.stringify(item));
                        });
                        process(items);
                    }
                })
                .fail(errorHandler);
        },
        searchUsers: function(q, process){
            mainSearchViewModel.searchProvider.search(q, process, "user");
        },
        searchClients: function(q, process){
            mainSearchViewModel.searchProvider.search(q, process, "client");
        },
        searchCases: function(q, process){
            mainSearchViewModel.searchProvider.search(q, process, "case");
        },
        searchAll: function (q, process) {
            mainSearchViewModel.searchProvider.search(q, process, "all");
        },
        highlighter: function(j){ // item formatter for bootstrap typeahead, intended to highlight but being used here as a formatter
            var o = JSON.parse(j),
                icos = {"case": "briefcase", "user": "user", "client": "asterisk"},
                ico = icos[o.type];

            return _.format("<div data-id='{0}'><i class='icon-{1}'></i>&nbsp;&nbsp;&nbsp;{2}</div>", o.id, ico, o.name);
        },
        updater: function(it){ // click handler for bootstrap typeahead
            var o = JSON.parse(it);

            bam.app("eDATBilling").currentEntity = o;
            bam.app("eDATBilling").transitionToPage(o.type + "\\edit", o);
        }
    }
};