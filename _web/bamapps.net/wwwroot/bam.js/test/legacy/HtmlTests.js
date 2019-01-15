/*
	Copyright © Bryan Apellanes 2015  
*/
$(document).ready(function(){
    bam.activatePlugins();
});

QUnit.test("html div test", 1, function(assert){
    var result = div({id:"testDiv"});
    assert.ok(_.isObject(result));
});

QUnit.test("html root test", function(assert){
    var h = html("div");
    assert.ok(_.isObject(h.root));
});

QUnit.test("html attribute test", function(assert){
    var h = html("div", {id: "rootDiv"});
    assert.ok(h.siblings[0].element.attr("id") == "rootDiv");
});

QUnit.test("html render test", function(assert){
    var h = html("div", {id: "rootDiv"});
    h.render(function(result){
        assert.ok(_.isString(result));
        assert.ok(result.indexOf("<div") == 0);

    });
});

QUnit.test("fluent test", function(assert){
    var h = div({id: "fluentTestDiv"}).h1("fluent test");
    h.render(function(result){
        var tmp = document.createElement("div");
        $(tmp).html(result);
        assert.ok($("#fluentTestDiv", tmp).length == 1);
        $("#scratch").append(tmp);
    })
});

function doBaseAssertions(ids, h, assert) {
    h.render(function (result) {
        var tmp = document.createElement("div");
        $(tmp).html(result);

        _.each(ids, function(id, k){
            assert.ok($(id, tmp).length == 1);
        });
        $("#scratch").append(tmp);
    })
}
QUnit.test("abbr test", function(assert){
    var h = div({id: "abbrTest"}).abbr("this is an abbr");
    doBaseAssertions(["#abbrTest"], h, assert);
});

QUnit.test("multiple fluent calls test", function(assert){
    var h = div({id: "multipleFluentCallsTest"})
            .h2("Multiple Calls Test")
            .p("This is a paragraph with no id")
            .p({id: "paragraphFluentTest"}, "This is a paragraph with an id");

    doBaseAssertions(["#multipleFluentCallsTest"], h, assert);
});

QUnit.test("fluent nesting test", function(assert){
    var toNest = div({id: "nestedDiv"}).p("nested paragraph");
    var h = div({id: "fluentNestingTest"})
        .h2("Fluent Nesting Test")
        .div("another div")
        .div(toNest);

    doBaseAssertions(["#fluentNestingTest", "#nestedDiv"], h, assert);
});

QUnit.test("bold test", function(assert){
    var h = div({id: "boldTest"})
            .h3("bold and sequence check")
            .span("this is first")
            .b(" now bold ")
            .span("after this should be a line break")
            .br()
            .span(span({id:"nestedSpan", name: "theMokey"}, "This is a span with an id and name"))
            .br()
            .span("new line")
            .br()
            .textarea({cols: 200, rows: 5}, "this should be in a textarea");

    doBaseAssertions(["#boldTest", "#nestedSpan"], h, assert);
});

QUnit.test("render test", function(assert){
    var html = div({id: "renderTest"})
                .h2("render test")
                .p("the rendered html will be stored in var and appended from string")
                .render();

    assert.ok(html.length > 0);
    assert.ok(_.isString(html));
    $("#scratch").append(html);
});

QUnit.test("event handler test", function (assert) {
    var html = div({id: "eventHandlerTest"})
        .h2("Event Handler Test")
        .span({id:"daButton", Class: "btn" }, "event handler test")
        .render(),
        $scratch = $("#scratch");

    $scratch.append(html);

    $("#daButton", $scratch)
        .on("click", function(e){
            alert("yay");
        });

    assert.ok(html.length > 0);
    assert.ok(_.isString(html));
});

QUnit.test("activate rendered", function(assert){
   var html = div({id: "activateTest"})
       .h2("activate test")
       .span({"data-plugin": "edit"}, "click to edit")
       .render(),
       $scratch = $("#scratch");

    assert.ok(html.length > 0);
    assert.ok(_.isString(html));
    $scratch.append(html).activate();

});

