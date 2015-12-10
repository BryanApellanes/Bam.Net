function errorHandler(e){
    $("#errorMessages").text(e);
}

var mainSearchViewModel = {
    searchProvider: {
        search: function(q, process){
            var items = [];
            items.push("Change this in ~/bam/js/viewModels/mainSearchViewModel.js");
            _.times(8, function (n) {
                items.push(q + ": " + n);
            });
            process(items);
        },
        highlighter: function(v){ // item formatter for bootstrap typeahead, intended to highlight            

            return v;
        },
        updater: function(it){ // click handler for bootstrap typeahead
            alert(it);
        }
    }
};