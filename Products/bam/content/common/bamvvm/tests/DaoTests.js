/*
	Copyright © Bryan Apellanes 2015  
*/
;
$(document).ready(function () {
    "use strict";
    dao.setCtx("DaoRef");
    $("#inputsForTestTable").dataSet().dataSetPlugins();

    function writeMessage(testName, val){
        $("#scratch").append($("<div>").text(testName + ": " + val));
    }

    var toBeDeleted = [];
    QUnit.done(function () {
        while (toBeDeleted.length > 0) {
            try {
                var d = toBeDeleted.pop();
                d.delete().then(function (r, msg) {
                    if (!r) {
                        throw {message: msg};
                    }
                });
            } catch (e) {
                alert(e);
            }
        }
    });

    QUnit.test("if id specified loaded should be false and Dao should be null", function(assert){
        assert.expect(2);
        var d = new dao.wrapper("TestTable", 500);
        assert.ok(d.loaded == false, "loaded should be false");
        assert.ok(_.isNull(d.Dao), "Dao should be null");
    });

    QUnit.test("if object specified loaded should be true and Dao should be the object specified", function(assert){
        assert.expect(3);
        var o = {Name: "TheMonkey"},
            d = new dao.wrapper(o);
        assert.ok(d.loaded == true, "loaded should be true");
        assert.ok(!_.isNull(d.Dao));
        assert.equal(o, d.Dao, "objects should match");
    });

    QUnit.test("new dao.entity test", function (assert) {
        assert.expect(1);
        var name = "dao.entity test",
            d = new dao.wrapper({ Name: name, Description: "description" });
        assert.equal(d.Dao.Name, name, "name should be " + name);
    });

    QUnit.test("name should get set", function (assert) {
        assert.expect(1);
        var name = "Monkey";
        var test = new dao.DaoRef.ctors.TestTable(name);
        assert.equal(test.Dao.Name, "Monkey", "test.Dao.name equals Monkey");
    });

    QUnit.test("testing jquery promises", function (assert){
        assert.expect(3);

        var thenCalled = false,
            firstCalled = false,
            andThenCalled = false,
            start = assert.async(),
            firstFn = function(){
                firstCalled = true;
            },
            thenFn = function(){
                thenCalled = true;
            },
            andThenFn = function(){
                andThenCalled = true;
            };
        $.when(firstFn()).then(thenFn).then(andThenFn).then(function(){
            assert.ok(firstCalled, "firstCalled should be true");
            assert.ok(thenCalled, "thenCalled should be true");
            assert.ok(andThenCalled, "andThenCalled should be true");
            start();
        })
    });

    QUnit.test("should be able to save", function (assert) {
        assert.expect(7);
        var name = "SaveMyMonkey";
        var test = new dao.DaoRef.ctors.TestTable(name);
        var start = assert.async();
        assert.ok(!_.isNull(test.Dao), "Dao shouldn't be null");
        assert.ok(!_.isUndefined(test.Dao), "Dao shouldn't be undefined");
        assert.equal(test.tableName, "TestTable");
        assert.equal(test.Dao.Name, name);

        test.save()
            .done(function(wrapped){
                assert.equal(wrapped.Dao.Name, name, "name should be " + name);
                assert.equal(test.Dao.Name, wrapped.Dao.Name, "return should same as original");
                assert.ok(!_.isNull(wrapped.pk()) && _.isNumber(wrapped.pk()), "key is " + wrapped.pk());
                start();
                toBeDeleted.push(test);
            })
            .fail(function(m){
                assert.ok(false, m);
                start();
            })
    });

    QUnit.test("load should return promise", function(assert){
        assert.expect(4);
        var name = "loadShouldReturnPromise",
            start = assert.async(),
            testObj = new dao.DaoRef.ctors.TestTable(name);

        testObj.save()
            .done(function(wrapped){
                var id = wrapped.pk();
                assert.equal(wrapped.tableName, "TestTable", "TableName should match");
                assert.ok(_.isNumber(wrapped.pk()), "pk() should be a number");
                var prom = dao.load("TestTable", wrapped.pk());
                    prom.then(function(d){
                        assert.equal(d.get("Name"), name);
                        assert.equal(id, d.pk());
                        start();
                    });

                toBeDeleted.push(wrapped);
            })
            .fail(function(m){
                assert.ok(false, m);
                start();
            })
    });

    QUnit.asyncTest("should be able to save and load", function(assert){
        var name = "should be able to save and load",
            data = new dao.DaoRef.ctors.TestTable(name, "description value");

        data.save()
            .done(function(wrapped){
                assert.equal(wrapped.get("Name"), name);
                assert.ok(_.isNumber(wrapped.pk()));
                dao.load(wrapped.tableName, wrapped.pk()).then(function(iwrapped){
                    assert.equal(wrapped.get("Name"), iwrapped.get("Name"), "names should match");
                    assert.equal(wrapped.pk(), iwrapped.pk(), "pk should be the same");
                    start();
                    toBeDeleted.push(iwrapped);
                })
            })
            .fail(function(){
                assert.ok(false, "if you see this the test failed");
                start();
            })
    });

    QUnit.asyncTest("should be able to update", function(assert){
        var name = "should be able to update",
            initDesc = "First Description",
            updatedDesc = "Updated Description",
            data = new dao.DaoRef.ctors.TestTable(name, initDesc),
            failFn = function(m){
                assert.ok(false, "if you see this the test failed: " + m);
            };

        data.save()
            .done(function(wrapped){
                var id = wrapped.pk();
                assert.equal(wrapped.get("Name"), name, "name should be " + name);
                assert.equal(wrapped.get("Description"), initDesc, "description shoud be " + initDesc);
                wrapped
                    .set("Description", updatedDesc)
                    .update()
                    .done(function(iwrapped){
                        assert.equal(iwrapped.get("Description"), updatedDesc, "description should be " + updatedDesc);
                        assert.equal(iwrapped.pk(), id, "Ids should match");
                        toBeDeleted.push(iwrapped);
                        start();
                    })
                    .fail(failFn);
            })
            .fail(failFn)
    });

    QUnit.asyncTest("should be able to save then delete", function(assert){
        expect(3);
        var name = "should be able to save then delete",
            data = new dao.DaoRef.ctors.TestTable(name);

        data.save()
            .done(function(wrapped){
                assert.equal(wrapped.get("Name"), name, "name should be " + name);
                wrapped.delete().then(function(r, m){
                    assert.ok(r);
                    assert.ok(m == "");
                    start();
                })
            })
            .fail(function(m){
                assert.ok(false, m);
                start();
            })
    });

    QUnit.asyncTest("save should pass a dao.wrapper to the then callback", function(assert){
        expect(1);
        var name = "saveShouldpass wrapped dao to then callback",
            d = new dao.DaoRef.ctors.TestTable(name);

        d.save()
            .done(function(wrapped){
                assert.ok(!_.isNull(wrapped) && !_.isNull(wrapped.Dao), "result shouldn't be null");
                writeMessage(name, "pk()=" + wrapped.pk());
                toBeDeleted.push(wrapped);
                start();
            })
            .fail(function(m){
                assert.ok(false, m);
            })
    });

    QUnit.asyncTest("tableName should not be null", function(assert){
        expect(1);
        var data = new dao.DaoRef.ctors.TestTable("tableNameShouldNotBeNull");

        data.save()
            .done(function(wrapped){
                assert.ok(!_.isNull(wrapped.Dao));
                toBeDeleted.push(wrapped);
                start();
            })
            .fail(function(m){
                assert.ok(false, m);
                start();
            })
    });

    QUnit.test("qi parsing tests", function(assert){
        var q = qi("DaoRef");

        q.from("TestTable").where("Name = Monkey");
        var parsed = q.clauses.parse();

        writeMessage("qi parsing tests", "txt: " + parsed.parsed + ", values: " + parsed.values);
        assert.ok(!_.isNull(parsed));
    });

    QUnit.test("query test", function(assert){
        var q = qi("DaoRef"),
            name = "queryTest",
            data = new dao.DaoRef.ctors.TestTable(name),
            start = assert.async();

        data.save()
            .done(function (wrapped) {
                q.from(wrapped.tableName)
                    .where("Name = " + name)
                    .load(true).done(function (r) {
                        assert.ok(r.length > 0);
                        var ids = "";
                        $.each(r, function (i, v) {
                            ids += v.pk() + " ";
                            toBeDeleted.push(v);
                        });
                        writeMessage("query test", "ids: " + ids);
                        start();
                    })
            })
            .fail(function (m) {
                assert.ok(false, m);
                start();
            })
    });

    QUnit.test("Save collection test", function(assert){
        assert.expect(4);
        var name = "save collection test",
            data = dao.construct("TestTable", {Name: name}),
            start = assert.async();

        data.save()
            .done(function(d){
                toBeDeleted.push(d);
                var coll = d.TestFkTableCollection();
                coll.add({Name: "the referencing object"});
                coll.add({Name: "another referencing object"});
                coll.save(true).then(function(c){
                    assert.ok(c.loaded == true);
                    c.load().then(function(reloaded){
                        assert.ok(reloaded.loaded);
                        assert.ok(!_.isUndefined(reloaded.values));
                        assert.ok(reloaded.values.length > 0);
                        _.each(reloaded.values, function(v){
                            toBeDeleted.push(v);
                        });
                        start();
                    })
                })
            })
            .fail(function(m){
                assert.ok(false, m);
            })
    });

    QUnit.test("Collection .each after save test", function(assert){
        var name = "Use each iterator test",
            data = dao.construct("TestTable", {Name: name}),
            start = assert.async();

        data.save()
            .done(function(d){
                var coll = d.TestFkTableCollection();
                for(var i = 0; i< 10;i++){
                    coll.add({Name: "name_" + i});
                }

                var msg = "PKs: ",
                    first = true,
                    count = 0;
                coll.save(true).then(function(c){
                    c.each(function(v){
                        if(!first){
                            msg += ",";
                        }
                        msg += v.get("Name") + ": " + v.pk() + "\r\n";
                        first = false;
                        count++;
                        toBeDeleted.push(v);
                    });

                    toBeDeleted.push(d);
                    assert.equal(count, 10);
                    writeMessage(name, msg);
                    start();
                });
            })
            .fail(function(m){
                assert.ok(false, m);
                start();
            })
    })
});











