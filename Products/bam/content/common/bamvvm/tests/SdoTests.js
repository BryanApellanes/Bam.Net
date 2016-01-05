/*
	Copyright © Bryan Apellanes 2015  
*/
$(document).ready(function(){
    (function(ctors, dao, sdo){
        "use strict";

        QUnit.test("has itemscope attribute", function (assert) {
            assert.ok(!_.isUndefined($("#justItemScope").attr("itemscope")));
        });

        QUnit.test("schema thing", function (assert) {
            var item = sdo.getItem("#thing1");
            assert.equal(item.itemtype, "http://schema.org/Thing", "was a thing");
        });

        QUnit.test("person", function (assert) {
            var item = sdo.getItem("#person");
            assert.equal(item.name, "Bryan");
        });

        QUnit.test("lastindexof", function (assert) {
            var str = "http://schema.org/Person";
            var li = str.lastIndexOf("/");
            var val = str.substr(li + 1, str.length - li);
            assert.equal(val, "Person");
        });

        QUnit.test("embedded type", function (assert) {
            var noodles = sdo.getItem("#noodles");
            assert.ok(!_.isUndefined(noodles.owner), "noodles.owner");
            assert.ok(noodles.owner.is("Person"), "noodles owner is person");
            assert.equal(noodles.owner.is(), "Person", "noodles owner is person");
        });

        QUnit.test("dao observe", function (assert) {
            var obj = { first: "Bryan", last: "Apellanes" },
                observed = dao.observe(obj);

            assert.ok(_.isFunction(observed.first), "property should be a function");
            assert.ok(_.isFunction(observed.last), "property should be a function");
            assert.equal(observed.first(), "Bryan", "first = Bryan");
            assert.equal(observed.last(), "Apellanes", "last = Apellanes");
        });

        QUnit.test("dao.sdo plugged in", function (assert) {
            assert.ok(!_.isUndefined(dao.sdo));
        });

        QUnit.test("set item test", 0, function (assert) {
            var obj = { Name: "The Guy", Description: "Was tall" },
                observed = dao.observe(obj);

            observed.startTest = function () {
                alert("name: " + observed.Name() + ", desc: " + observed.Description());
            }

            sdo.setItem("#inputsForTestTable", observed);
        });
    })(dao.DaoRef.ctors, dao, sdo)
});