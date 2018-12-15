
bam.app("main").setPageTransitionFilter("current", "next", function (tx, d) {
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
});

var fileTree;
bam.app("main").pageActivated("start", function(page){
    // force update of codeEditors
    //$('a[data-toggle=tab]')
    //    .on("shown", function () {
            _.delay(function () {
                $("[data-plugin=codeEditor]").data("CodeMirror").refresh();
            }, 1000);
        //});    
    // end force refresh

    fileTree = fstree({  // defined in fstree.js
        fileClicked: function(node){
            fs.readAllText(node.path)
                .done(function(txt){
                    var lastSlash = node.path.lastIndexOf("/"),
                        fileName = node.path.substr(lastSlash + 1),
                        ext = fileName.substr(fileName.lastIndexOf(".") + 1),
                        editor = bam.app.viewModels[ext + "Editor"];

                    if(!_.isUndefined(editor) && !_.isNull(editor)){
                        editor.setCode(txt);
                    }
                })
        },
        folderClicked: function(node){
            //alert("folder: " + node.path);
        }
    });
});



//bam.app("main").run("start");





