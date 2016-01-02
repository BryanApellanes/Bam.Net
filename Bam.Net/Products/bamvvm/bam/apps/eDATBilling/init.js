
bam.app("eDATBilling").setPageTransitionFilter("current", "next", function (tx, d) {
    // tx is the transitionHandler which looks like this
    // {
    //      name: <string>,
    //      from: <string>, // the name of the page the transition is from
    //      to: <string>, // the name of the page the transition is to
    //      play: function(data), // plays the transition passing in optional data
    //      also triggers start and end events before and after play
    // }
    // analyze the data d to determine if the transition will be allowed or
    // directly analyze the state of the dom.
    // return false to stop the transition from current to next page
})
.pageActivated("test", function (page, data) {
    //test().go();
})
.pageActivated("case\\edit", function (page, data) {
    if (!_.isUndefined(data) && !_.isNull(data)) {

    }
});

(function (app) {
    $(document).ready(function () {

        var startPage = _.getQueryString("sp"),
            initCount = 0,
            expected = 4;
        if (startPage == "" || _.isNull(startPage) || _.isUndefined(startPage)) {
            startPage = "start";
        }

        function run() {
            if (initCount == expected) {
                app.run(startPage);
            }
        }

        _.act("data", "getCaseTypes")
            .then(function (r) {
                if (r.Success) {
                    app.caseTypes = r.Data;
                } else {
                    app.caseTypes = [{ id: -1, name: _.format("Error Loading Case Types ({0})", r.Message) }];
                }
                initCount++;
            }).always(run);

        _.act("data", "getCaseStatuses")
            .then(function (r) {
                if (r.Success) {
                    app.caseStatuses = r.Data;
                } else {
                    app.caseStatuses = [{ id: -1, name: _.format("Error Loading Case Statuses ({0})", r.Message) }];
                }
                initCount++;
            }).always(run);

        _.act("data", "getUserRoles")
            .then(function (r) {
                if (r.Success) {
                    app.userRoles = r.Data;
                } else {
                    app.userRoles = [{ id: -1, name: _.format("Error Loading User Roles ({0})}", r.Message) }];
                }
                initCount++;
            }).always(run);

        _.act("data", "getMatterNumberTypes")
            .then(function (r) {
                if (r.Success) {
                    app.matterNumberTypes = {};
                    _.each(r.Data, function (v) {
                        app.matterNumberTypes[v.id] = v;
                    });
                } else {
                    app.matterNumberTypes = [{ id: -1, name: _.format("Error Loading Matter Number Types ({0})", r.Message) }];
                }
                initCount++;
            }).always(run);
    });
})(bam.app("eDATBilling"))
