/*
	Copyright © Bryan Apellanes 2015  
*/
$(document).ready(function(){
    "use strict";
    dao.setCtx("DaoRef");
    $("body").dataSet().dataSetPlugins();

    QUnit.test("databindRaw", function(assert){
        expect(2);
        var name = "databindRaw",
            desc = "the description",
            data = new dao.DaoRef.ctors.TestTable(name),
            wrapped = dao.wrap(data),
            container = $("#databindRaw");

        wrapped.set("Description", desc);

        dao.dataBind("#databindRaw", wrapped.raw());

        assert.equal($("[itemprop=Name]", container).val(), name);
        assert.equal($("[itemprop=Description]", container).val(), desc);
    });

    QUnit.test("construct test", function(assert){
        var name = "construct test",
            desc = "this is the description",
            data = dao.construct("TestTable", {Name: name, Description: desc});

        assert.equal(data.get("Name"), name);
        assert.equal(data.get("Description"), desc);
    });

    QUnit.test("databindObservable", function(assert){
        var name = "databindObservable",
            desc = "databindObservable description",
            container = $("#databindObservable"),
            data = dao.construct("TestTable", {Name: name, Description: desc}),
            obs = dao.observe(data.raw()),
            changeCount = 0;

            obs.change("Name", function(){
                changeCount++;
            });

            obs.checkValues = function(){
                var success = true,
                    currentName = $("[itemprop=Name]", container).val(),
                    currentDesc = $("[itemprop=Description]", container).val(),
                    detail = "ChangeCount: " + changeCount + "\r\nName = " + currentName + "\r\nDescription = " + currentDesc;

                success = currentName == obs.Name() && currentDesc == obs.Description();

                alert(success ? "passed:" + detail: "failed:" + detail);
            };

        assert.equal(name, obs.Name());
        dao.dataBind("#databindObservable", obs);
    })
});
