var proto = {
    getSource: function getSource(el) {
        return $(el).data("CodeMirror").getValue();
    },
    setSource: function (el, val) {
        $(el).data("CodeMirror").setValue(val);
    }
};

function writeHistoryFile(content, n, oprom){
    var num = n || 1,
        fileName="workspace/history/" + num.toString() + ".js";
    return $.Deferred(function(){
        var prom = this;
        fs.fileExists(fileName)
            .done(function(r){
                if(r){
                    writeHistoryFile(content, ++n, oprom);
                }else{
                    prom.resolve(fileName);
                    if(oprom){
                        oprom.resolve(fileName);
                    }
                    fs.write(fileName, content, true);
                }
            })
    }).promise();
}

var db = {};

var viewModels = {
    jsonEditor: {
        view: {
            viewModelName: "jsonEditor",
            title: "Json",
            mode: "json",
            theme: "monokai",
            editorId: "jsonCode",
            buttons: [
                { text: "create form", action: "createForm" }
            ]            
        },
        model: {
            source: JSON.stringify({ name: "Test", value: "value" }),
            createForm: function (n, o, e) { // new, old, event
                _.act("dust", "writeTemplate", {appName: b.app.name, json: JSON.stringify(n), setValues: true});
            }
        },
        setCode: function(code){
            bam.app.viewModels.jsonEditor.model.setSource($("#jsonCode"), code);
        }
    },
    jsEditor: {
        view: {
            viewModelName: "jsEditor",
            title: "Javascript Code",
            mode: "javascript",
            theme: "monokai",
            editorId: "jsCode",
            buttons: [
                { action: "run", text: "Run" }
            ]
        },
        model: _.observe({
            source: "(function(){" +
                    "   var database = { // must be database\r\n" +
                    "       nameSpace: \"Namespace\",\r\n" +
                    "       schemaName: \"SchemaName\",\r\n" +
                    "       xrefs: [\r\n" +
                    "            [\"Left\", \"Right\"],\r\n" +
                    "       ],\r\n" +
                    "       tables: [\r\n" +
                    "           {\r\n" +
                    "               name: \"tableName\",\r\n" +
                    "               cols: [\r\n" +
                    "                   { ColumnName: \"String\", Null: false },\r\n" +
                    "               ],\r\n" +
                    "               fks: [\r\n" +
                    "                    { ColumnName: \"TableName\" }\r\n" +
                    "               ]\r\n" +
                    "           }\r\n" +
                    "       ]\r\n" +
                    "   }\r\n" +
                    "return database;\r\n" +
                    "})();"
            ,
            run: function run(curData, origData, event) {
                var code = curData.source;
                writeHistoryFile(code, 1);
                var result = eval(code);
                if (!_.isUndefined(result) && !_.isNull(result)) {                    
                    db = dbs.schemafy(result);
                    bam.app.viewModels.jsonEditor.model.setSource($("#jsonCode"), JSON.stringify(db));
                    bam.app.writeTemplate(result);
                }
            }
        }),
        setCode: function(code){
            bam.app.viewModels.jsEditor.model.setSource($("#jsCode"), code);
        }
    },
    htmlEditor: {
        view: {
            viewModelName: "htmlEditor",
            title: "Html",
            mode: "htmlmixed",
            theme: "monokai",
            action: "save",
            actionText: "save",
            editorId: "htmlCode",
            buttons: [
                { action: "save", text: "save" }
            ]
        },
        model: {
            source: "<html></html>",
            save: function save(curData, origData, event) {
                alert(curData.source);
            }
        },
        setCode: function(code){
            bam.app.viewModels.htmlEditor.model.setSource($("#htmlCode"), code);
        }
    }
};

_.each(viewModels, function (viewModel, name) {
    $.extend(viewModel.model, proto);
});

bam.app("main").setViewModel("htmlEditor", viewModels.htmlEditor);
bam.app("main").setViewModel("jsEditor", viewModels.jsEditor);
bam.app("main").setViewModel("jsonEditor", viewModels.jsonEditor);

// the stuff below works but the above is easier to read

//function editor(src) {
//    this.source = src;
//    this.save = function () { };
//}

//editor.prototype = proto;

//var jsEditor = new editor("(function($, _, b, d, q, w){\r\n\r\n\r\n\r\n})(jQuery, _, bam, dao, qi, window || {});");
//jsEditor.save = function save(curData, origData, event) { // attached as an action (currentValue, originalValue, event)
//    eval(curData.source);
//};

//var htmlEditor = new editor("<html></html>");
//htmlEditor.save = function save(cur, orig, ev) {
//    alert("html monkey");
//};

//bam.app.setViewModel("htmlEditor", htmlEditor);
//bam.app.setViewModel("jsEditor", jsEditor);
