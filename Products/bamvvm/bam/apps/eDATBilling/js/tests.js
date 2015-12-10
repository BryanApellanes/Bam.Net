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

function doBaseAssertions(id, h, assert) {
    h.render(function (result) {
        var tmp = document.createElement("div");
        $(tmp).html(result);
        assert.ok($(id, tmp).length == 1);
        $("#scratch").append(tmp);
    })
}
QUnit.test("abbr test", function(assert){
    var h = div({id: "abbrTest"}).abbr("this is an abbr");
    doBaseAssertions("#abbrTest", h, assert);
})

QUnit.test("multiple fluent calls test", function(assert){
    var h = div({id: "multipleFluentCallsTest"})
            .h2("Multiple Calls Test")
            .p("This is a paragraph with no id")
            .p({id: "paragraphFluentTest"}, "This is a paragraph with an id");

    doBaseAssertions("#multipleFluentCallsTest", h, assert);
});

QUnit.test("fluent nesting test", function(assert){
    var toNest = div({id: "nestedDiv"}).p("nested paragraph");
    var h = div({id: "fluentNestingTest"})
        .h2("Fluent Nesting Test")
        .div("another div")
        .div(toNest);

    doBaseAssertions("#fluentNestingTest", h, assert);
});

//QUnit.test("html.js test", 0, function(assert){
//    div({id: "mainDiv"}).h1("Hello").p("This is a paragraph").child("div", {id: "childDiv"}).render(function(h){
//        alert(h);
//    });
//});

QUnit.test("test setup", function (assert) {
    assert.ok(true);
});

QUnit.asyncTest("write template test", 1, function (assert) {
    _.act("dust", "for", { json: JSON.stringify({ className: "Flying Monkey", title: "Mr." }) }, { dataType: "html" })
        .done(function (r) {
            $("#flyingMonkeyHtml").html(r);
            assert.ok(_.isString(r));
        })
        .always(start);
});

var testData = [
    "monkey",
    "gorilla",
    "bananas",
    "blah",
    "bryan"
];

QUnit.test("type ahead test", 0, function(assert){
       $("#thArraySrc").typeahead({
           source: testData
       });
});

function getTypeAheadTestData(query, cb){
   return testData;
}

function getTypeAheadTestDataAsync(query, cb){
    _.act("test", "gettestdata", {query: query, count: 8})
        .done(function(r){
            var results = [];
            _.each(r, function(v,k){
                results.push(v.Name);
            });
            cb(results);
        })
}

function typeAheadHighlighter(item){
    return _.format("<div>monkey: &nbsp;" + item + "</div>");
}

function nav(el) {
    this.view = {
        links: [
            { id: "navHome", href: "/", text: "Home" },
            { id: "navAbout", href: "/home/about", text: "About" }
        ]
    };
}