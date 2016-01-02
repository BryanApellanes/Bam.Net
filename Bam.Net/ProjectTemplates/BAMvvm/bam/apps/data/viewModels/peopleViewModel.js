function peopleViewModel(el, app) {
    this.view = {
        items: [],
        init: function () {
            var the = this;
            return $.Deferred(function () {
                var prom = this;
                $(document).ready(function () {
                    _.times(3, function (i) {
                        the.items.push(new testItem());
                    });
                    prom.resolve();
                });                
            }).promise();
        }
    }

    this.model = {
        remove: function () {
            alert("remove");
        }
    }
}