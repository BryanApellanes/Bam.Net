/**
 * used by bam\pages\case\edit
 * provides the viewModel and related utilities
 */

var caseEdit = (function($, _, app){
    var _currentCase = null;

    /**
     * @constructor
     * @param el
     */
    function viewModel(el){
        var the = this;
        this.element = el;

        this.activate = function(itemscopeElement){
            $.extend(the.model, app.currentEntity);
            _.setItem(itemscopeElement, the.model);
            _.act("data", "retrieveCase", {type: "case", id: the.model.id})
                .done(function(r){
                    if (r.Success) {
                        var $tableBody = $("#matterNumberList");
                        _.each(r.Data.matterNumbers, function (mnObj) {
                            var matterNumberRowValue = { value: mnObj.value, type: bam.app.matterNumberTypes[mnObj.typeId].name };
                            bam.app.view("eDATBilling.matterNumberRow", matterNumberRowValue)
                                .done(function (h) {
                                    $tableBody.append(h);
                                });
                        });
                        $tableBody.activate(); // activates plugins
                        bam.app.currentEntity = r.Data;
                        //since the current entity is just a stub this will set the remainder of the properties in the ui
                        _.setItem(itemscopeElement, r.Data); 
                    }
                })

            _.each(bam.app.userRoles, function (userRole) {
                _.act("data", "getTeamMembers", { caseId: the.model.id, roleConstant: userRole.constant })
                    .done(function (r) {
                        if (r.Success) {
                            alert("this is not fully implemented");
                        }
                    })
            });
        };

        // used by dust template
        this.view = {
            caseTypes: app.caseTypes, // loaded in init.js
            caseStatuses: app.caseStatuses, // loaded in init.js
            typeAheadSource: "mainSearchViewModel.searchProvider.searchClients",
            typeAheadHighlighter: "mainSearchViewModel.searchProvider.highlighter",
            typeAheadUpdater: "caseEdit.selectClient"
        };

        // attached by sdo.setItem
        this.model = {
            setMatterNumbers: function () {
                alert("setMatterNumbers");
            },
            addBillingNumber: function (c, o, e) {
                alert("add billing number");
            },
            save: function (c, o, e) {
                alert(_.format("caseStatusId:{0}", c.caseStatusId));
            }
        }
    }

    function setCase(o){
        _currentCase = o;
    }

    function getCase(){
        if(!_.isUndefined(mainSearchViewModel.currentEntity) && !_.isNull(mainSearchViewModel.currentEntity)){
            if(mainSearchViewModel.currentEntity.type == "case"){  // was a case selected from the main search box ??
                _currentCase = mainSearchViewModel.currentEntity;
            }
        }
        return _currentCase;
    }

    function selectClient(clientJson){
        var client = JSON.parse(clientJson);
        $("#caseEditClientId").val(client.id);
        return client.name;
    }

    return {
        viewModel: viewModel,
        selectClient: selectClient,
        setCase: setCase,
        getCase: getCase
    }
})(jQuery, _, bam.app("eDATBilling"));
