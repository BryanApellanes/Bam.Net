/*
	Copyright © Bryan Apellanes 2015  
*/
$(function () {
    bam.setAppRoot("http://uspapptstweb02:8099/");

    QUnit.asyncTest("jsProxy echo test", 2, function (assert) {
        assert.ok(!_.isUndefined(srvrEcho));
        var val = "This is the text that should come back plus random text: " + _.randomString(8);
        srvrEcho.testStringParameter(val)
            .done(function (r) {
                assert.equal(r, val);
            })
            .always(start);
    });

    QUnit.asyncTest("jsProxy echo html test", 2,function (assert) {
        assert.ok(!_.isUndefined(srvrEcho));
        var val = "This is the text that should come back and some random text: " + _.randomString(8);
        srvrEcho.testStringParameter(val, { format: "html" })
            .done(function (r) {
                $("#proxyHtmlTarget").html(r);
                assert.ok(true, "should be true");
            })
            .always(start);
    });

    QUnit.asyncTest("jsProxy obj parameter test", 2, function(assert){
        assert.ok(!_.isUndefined(srvrEcho));
        var val = "This is the text that should back: jsProxy obj parameter test: " + _.randomString(8);
        srvrEcho.testObjectParameter({StringProperty: val, IntProperty: 455, BoolProperty: true}, "more text")
            .done(function(r){
                $("#testObjectParameterTarget").html(r);
                assert.ok(true, "should be true"); // making sure 2 assertions are run
            })
            .always(start);
    });

    QUnit.asyncTest("obj out", 5, function(assert){
        assert.ok(!_.isUndefined(srvrEcho));
        var val = "This is the text that should back: jsProxy obj parameter test: " + _.randomString(8);
        srvrEcho.testObjectOut(val, true, 466)
            .done(function(r){
                assert.equal(r.StringProperty, val);
                assert.equal(r.BoolProperty, true);
                assert.ok(_.isBoolean(r.BoolProperty));
                assert.equal(r.IntProperty, 466);
            })
            .always(start);
    });
});