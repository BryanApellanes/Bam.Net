/*
	Copyright © Bryan Apellanes 2015  
*/
var fstree = (function($,_,b,f){
    "use strict";
    var id = 1,
        treeNodes = {},
        openNodes = [];

    return function(opts){
        var config = {
                selector: "#fsTree",
                fileClicked: function(){},
                folderClicked: function(){}
            },
            rootNode = new treeNode("/", b.app.name);

        function nodeId(){
            return id++;
        }

        /**
         * @constructor
         */
        function treeNode(path, label, type){
            this.path = path;
            this.label = label;
            this.id = nodeId();
            this.nodeType = type || "folder";
            this.children = [{label: " ", id: nodeId()}];
        }

        if(_.isString(opts)){
            config.selector = opts;
        }

        $.extend(config, opts);

        $(config.selector)
            .tree({
                data:[rootNode],
                dragAndDrop: true
            });

        function moveNode(ev){
            var info = ev.move_info, // yuck !! should consider creating my own tree plugin
                moving = info.moved_node,
                target = info.target_node;

            if(target.nodeType == "folder"){
                var methodName = moving.nodeType == "file" ? "moveFile": "moveFolder";
                fs[methodName](moving.path, target.path);
            }
        }

        function loadNode(ev){
            var path = ev.node.path,
                nodeId = ev.node.id,
                node = ev.node,
                nodeType = node.nodeType,
                $tree = $(config.selector);

            if(nodeType == "folder"){
                node.removeChildren();
                fs.getDirectories(path, function(ds){
                    _.each(ds, function(dir){
                        var nodePath = _.format("{0}{1}/", path, dir),
                            dirNode = new treeNode(nodePath, dir);

                        treeNodes[nodePath] = dirNode;

                        $tree.tree("appendNode", dirNode, node);
                        var newNode = $tree.tree("getNodeById", dirNode.id);
                        $tree.tree("appendNode", {label: " "}, newNode);
                    });

                    var openNodesJson = _.getCookie("openFsNodes");
                    _.each(JSON.parse(openNodesJson), function(v, i){
                        var node = treeNodes[v];
                        if(!_.isUndefined(node)){
                            var theirs = $tree.tree("getNodeById", node.id);
                            $tree.tree("openNode", theirs, false);
                        }
                    });

                    fs.getFiles(path, "*", function(files){
                        _.each(files, function(file){
                            var nodePath = _.format("{0}{1}", path, file),
                                fileNode = new treeNode(nodePath, file, "file");

                            treeNodes[nodePath] = fileNode;
                            $(config.selector)
                                .tree("appendNode", fileNode, node);
                        });
                    })
                });
            }
            $(config.selector)
                .tree("removeFromSelection", node);


            if(nodeType == "folder"){
                config.folderClicked(node);
            }

            if(nodeType == "file"){
                config.fileClicked(node);
            }
        }

        function openNode(ev){
            openNodes.push(ev.node.path);
            _.setCookie("openFsNodes", JSON.stringify(openNodes));
            loadNode(ev);
        }

        function closeNode(ev){
            _.each(openNodes, function(v, i){
                if(v == ev.node.path){
                    openNodes.splice(i, 1); // remove the entry for the open node
                }
            })
        }

        $(config.selector).on('tree.click', loadNode);
        $(config.selector).on('tree.open', openNode);
        $(config.selector).on('tree.close', closeNode);
        $(config.selector).on('tree.move', moveNode);

        return {
            tree: $(config.selector),
            fs: fs
        }
    }
})(jQuery, _, bam, fs);
