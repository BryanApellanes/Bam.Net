var sideBar = (function ($, _) {
    var sideBarViewData = {
        _default: {
            id: "sideBar",
            sections: [
                {
                    id: "casesSideBar",
                    title: "Cases",
                    links: [
                    ]
                },
                {
                    id: "clientsSideBar",
                    title: "Clients",
                    links: [
                    ]
                },
                {
                    id: "usersSideBar",
                    title: "Users",
                    links: []
                }
            ]
        },
        caseEdit: {
            id: "caseEditSideBar",
            sections: [
                {
                    id: "clientsSideBar",
                    title: "Clients",
                    links: [
                    ]
                },
                {
                    id: "usersSideBar",
                    title: "Users",
                    links: []
                }
            ]
        }
    }

    function viewModel(el) {
        var the = this,
            sidebarName = $(el).attr("data-sidebar") || "_default";

        this.element = el;

        this.view = sideBarViewData[sidebarName];        
        
        this.activate = function () { // view container must be tagged with itemscope
            $("#" + the.view.id).css("width", "225px").height("auto");
            _.each(the.view.sections, function (section, k) {
                var input = $(document.createElement("input")).attr("type", "text").css("width", "90%");
                $("#"+section.id).append(input);
            });
        }

        this.model = {
        };
    }

    return {
        viewModel: viewModel
    }
})(jQuery, _)