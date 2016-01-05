/*
	Copyright © Bryan Apellanes 2015  
*/
$(document).ready(function () {

    bam.activatePlugins();

    QUnit.test("should have required field", function(assert){
        var testObj = $("#requiredTest").data("validatable"),
            $field = $("#RequiredField");
        assert.ok(!_.isUndefined(testObj));
        $field.val("");

        var validation = testObj.validate();
        assert.ok(validation.success == false, "validation should have failed");
        $("#requiredTestMsgs").text(validation.messages.RequiredField);

        $field.val("a value");
        validation = testObj.validate();
        assert.ok(validation.success == true, "validation should have succeeded");

    });

    QUnit.test("should have custom message", function(assert){
        var testObj = $("#requiredWithMessageTest").data("validatable"),
            $field = $("#RequiredWithMessageField");

        assert.ok(!_.isUndefined(testObj));
        $field.val("");

        var validation = testObj.validate();
        assert.ok(validation.success == false, "validation should have failed");
        assert.equal(validation.messages.RequiredWithMessageField,
            "Custom Msg: The Value field requires a value");

        $field.val("bananas");
        validation = testObj.validate();
        assert.ok(validation.success == true, "validation should have succeeded");
    });

    QUnit.test("optional email should only fail if not email", function(assert){
        var testObj = $("#optionalEmailTest").data('validatable'),
            $field = $("#OptionalEmail");

        assert.ok(!_.isUndefined(testObj));
        $field.val("");

        var validation = testObj.validate();
        assert.ok(validation.success, "validation should have succeeded");
        $field.val("invalid email address");
        validation = testObj.validate();
        assert.ok(validation.success == false, "validation should have failed");

        $field.val("good@email.com");
        validation = testObj.validate();
        assert.ok(validation.success, "validation should have succeeded");
    });
});

