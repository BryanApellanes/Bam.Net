
JSUI.$$JsonId$$ = {};
JSUI.$$JsonId$$.el = jQuery('[jsonid=$$JsonId$$]')[0];
JSUI.$$JsonId$$.deleteFolderPrompt = "$$DeleteFolderPrompt$$";
JSUI.$$JsonId$$.newFileDropped = function(jsonid, droppedEl) { }; 
JSUI.$$JsonId$$.folderAction = function(action, el, pos) {
    JSUI.DataTree.actionImpl("$$JsonId$$", action, el, pos);
};

JSUI.$$JsonId$$.fileAction = function(action, el, pos) {
    JSUI.DataTree.actionImpl("$$JsonId$$", action, el, pos);
};

JSUI.$$JsonId$$.init = function() {
    JSUI.Conf.usingProxy("dataTree", function(inf) {
        JSUI.assimilate(ProxyUtil.getProxy(inf.varname), JSUI.$$JsonId$$);
        JSUI.assimilate(JSUI.DataTree, JSUI.$$JsonId$$);
    });

    JSUI.Conf.usingResource(["naizari.javascript.jsui.jquery.contextmenu.js", "naizari.javascript.jsui.datatree.util.js"], function(s) {
        jQuery('[jsonid=$$JsonId$$]').treeview({
            animated: "fast",
            persist: "location"
        }).show();
        jQuery('[jsonid=$$JsonId$$] .file').each(function(k, el) {
            jQuery(el)
            .draggable({ revert: "invalid", helper: "clone" })
            .contextMenu({ menu: "$$JsonId$$_filemenu" }, $$OnFileOptionClicked$$); // default is JSUI.$$JsonId$$.fileAction
            JSUI.handCursor(el);
        });
        var menuOptions = { menu: "$$JsonId$$_foldermenu" };
        jQuery('[jsonid=$$JsonId$$] .folder').each(function(k, el) {
            jQuery(el)
            .draggable({ revert: "invalid", helper: "clone" })
            .droppable({
                drop: function(e, ui) {
                    var folderId = jQuery(this).attr("dataid");
                    var fileId = jQuery(ui.draggable).attr("dataid");
                    if (!JSUI.isNullOrUndef(fileId)) {
                        JSUI.$$JsonId$$.MoveToEx(fileId, folderId, function(result) {
                            if (result.Status == "Success") {
                                if (JSUI.isNullOrUndef(result.Data) || result.Data.Success) {
                                    JSUI.DataTree.refresh("$$JsonId$$");
                                } else if (!JSUI.isNullOrUndef(result.Data) && !result.Data.Success && result.Data.Code == "ItemExists") {
                                    JSUI.Conf.usingResource(["naizari.javascript.jsui.messagebox.js"], function(s) {
                                        MessageBox.show("An item with the same name already exists in that folder.");
                                    });
                                }
                            }
                        });
                    } else {
                        JSUI.$$JsonId$$.newFileDropped("$$JsonId$$", ui.draggable);
                    }
                }
            })
            .contextMenu(menuOptions, $$OnFolderOptionClicked$$);
            JSUI.handCursor(el);
        });
        jQuery('#$$DomId$$').contextMenu(menuOptions, $$OnFolderOptionClicked$$);
    });
}

JSUI.$$JsonId$$.init();
