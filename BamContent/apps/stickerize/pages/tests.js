/*
	Copyright © Bryan Apellanes 2015  
*/
var tests =(function(q, $, b, app, s, scc, sc){
    "use strict";
    var pki = forge.pki,
        random = forge.random,
        sha1 = forge.md.sha1,
        aes = forge.aes,
        util = forge.util,
        iv = sha1.create().update("").digest().toHex().substring(0, 16);


    function scratch(msg){
        div({style: "border 1px solid black;"})
            .text(msg)
            .render(function(result){
                $("#scratchSpace").append(result);
            });
    }

    function runAllTests(){
        q.test("Verify QUnit", function(assert){
            assert.ok(true, "Just to make sure QUnit is working");
        });

        q.test("Should be able to set a drop down", function(assert){

            b.view("dropdown", {
                name: "drop down name",
                items:[
                    {
                        id: "dropDownTestOne",
                        text: "One"
                    },
                    {
                        id: "dropDownTestTwo",
                        text: "Two"
                    }
                ]
            }, "#dropDownTarget");

            var empty = "";

            assert.ok($("#dopDownTarget").html() != empty);
        });

        q.test("Should be able to set a drop down with model", function(assert){
            var model = new templates.dropdownModel("Drop Down Model test");
            model.addItem("Some Text", function(){
                alert("clicked");
            });
            model.renderIn("#dropDownTarget2");

            var empty = "";

            assert.ok($("#dropDownTarget2").html() != empty);
        });

        q.test("Test sha1", function(assert){
            var forgeSha1 = sha1.create().update("").digest().toHex(),
                _sha1 = _.sha1("");

            assert.equal(forgeSha1, _sha1);
        });

        q.asyncTest("Secure Echo", function(assert){
            q.expect(1);
            var testValue = "test string";

            scc.startSession()
                .done(function(session){
                    b.secureInvoke("Echo", "Send", [testValue]).then(function(r){
                        assert.equal(r, testValue);
                        q.start();
                    })
                })
                .fail(function(msg){
                    assert.ok(false, msg);
                    q.start();
                })
        });

        q.asyncTest("should set session key", function(assert){
            q.expect(2);
            scc.startSession()
                .done(function(session){
                    assert.ok(session);
                    assert.ok(session.started);
                    scratch("session id: " + session.clientId);
                    q.start();
                })
                .fail(function(msg){
                    assert.ok(false, msg); // failed
                    q.start();
                })
        });

        q.test("util.encode64 should give same result as btoa", function(assert){
            var data = random.getBytes(6);
            var btoaed = btoa(data);
            var utiled = util.encode64(data);

            assert.equal(btoaed, utiled);
        });

        function createKey(){
            var key = random.getBytesSync(16),
                base64Key = util.encode64(key);
            return {
                key: key,
                base64Key: base64Key,
                iv: iv
            }
        }

        q.test("Encrypt with getKey", function(assert){
            assert.ok(true);

            function encrypt(data, key, iv){
                var encrypted = aes.createEncryptionCipher(key, "CBC");

                encrypted.start(iv);
                encrypted.update(util.createBuffer(data));
                encrypted.finish();
                return util.encode64(encrypted.output.data);
            }

            var gotKey = createKey();

            var info  = "Encrypt with getKey";
            var b64Encrypted = encrypt(info, gotKey.key, gotKey.iv);
            var encryptedBytesFrB64 = util.decode64(b64Encrypted);

            var cipher = aes.createDecryptionCipher(gotKey.key, 'CBC');
            cipher.start(iv);
            cipher.update(util.createBuffer(encryptedBytesFrB64));
            cipher.finish();

            scratch("Encrypt with getKey decrypted: " + cipher.output.data );
        });

        q.test("Encrypt and decrypt with getKey", function(assert){
            function encrypt(data, key, iv){
                var encrypted = aes.createEncryptionCipher(key, "CBC");

                encrypted.start(iv);
                encrypted.update(util.createBuffer(data));
                encrypted.finish();
                return util.encode64(encrypted.output.data);
            }

            function decrypt(b64Cipher, key, iv){
                var decryptor = aes.createDecryptionCipher(key, 'CBC'),
                    encryptedBytes = util.decode64(b64Cipher);

                decryptor.start(iv);
                decryptor.update(util.createBuffer(encryptedBytes));
                decryptor.finish();

                return decryptor.output.data;
            }

            var gotKey = createKey();

            var info  = "Encrypt and decrypt with getKey";
            var b64Encrypted = encrypt(info, gotKey.key, gotKey.iv);

            var decrypted = decrypt(b64Encrypted, gotKey.key, gotKey.iv);

            scratch(info + ": " + decrypted);

            assert.equal(info, decrypted);
        });


        q.asyncTest("Encrypt and decrypt using session.aes", function(assert){
            q.expect(1);

            scc.startSession().then(function(session){
                var gotKey = session.createAesKey();
                var info = "Encrypt and decrypt using session.aes";

                var b64Encrypted = session.aes.encrypt(info, gotKey.key, gotKey.iv);
                var decrypted = session.aes.decrypt(b64Encrypted, gotKey.key, gotKey.iv);
                scratch(info + ": " + decrypted);
                assert.equal(info, decrypted, info);
                q.start();
            })
        });

        q.asyncTest("Encrypt and decrypt using session", function(assert){
            q.expect(1);

            scc.startSession().then(function(session){
                var info = "Encrypt and decrypt using session";

                var b64Encrypted = session.encrypt(info);
                var decrypted = session.decrypt(b64Encrypted);
                scratch(info + ": " + decrypted);
                assert.equal(info, decrypted);
                q.start();
            })
        });

        q.asyncTest("Test session key", function(assert){
            q.expect(1);

            scc.startSession().then(function(session){
                var info = "Test session key";

                var cipher = session.encrypt(info);
                sc.testDecrypt(cipher, session.symmetricKey64, session.symmetricIV64)
                    .done(function(r){
                        assert.equal(r.substring(0, r.length - 2), info);
                        q.start();
                    })
                    .fail(function(r){
                        assert.ok(false, r);
                        q.start();
                    })
            });
        });

        q.asyncTest("Server session decrypt test", function(assert){

            scc.startSession().then(function(session){
                var info = "server session decrypt";

                var cipher = session.encrypt(info);
                sc.testSessionKey(cipher)
                    .done(function(r){
                        assert.equal(r, info);
                        q.start();
                    })
                    .fail(function(r){
                        assert.ok(false, r);
                        q.start();
                    })
            });
        });

        q.test("look at key", function(assert){
            var key = createKey();
            scratch("look at key (key): " + key.key);
            scratch("look at key (base64Key): " + key.base64Key);
            scratch("look at key (iv): " + key.iv);

            assert.ok(key);
        });

        q.test("bare forge encryption", function(assert){
            // generate a random key and IV
            var key = forge.random.getBytesSync(16);
            var iv = forge.random.getBytesSync(16);

            var someBytes = "monkey";

            var cipher = forge.aes.createEncryptionCipher(key, 'CBC');
            cipher.start(iv);
            cipher.update(forge.util.createBuffer(someBytes));
            cipher.finish();
            var encrypted = cipher.output;

            scratch("bare encrypted: " + encrypted.toHex());

            cipher = forge.aes.createDecryptionCipher(key, 'CBC');
            cipher.start(iv);
            cipher.update(util.createBuffer(encrypted.data));
            cipher.finish();

            scratch("bare decrypted: " + cipher.output.data);
            assert.ok(cipher.output.data == someBytes);
        });

        q.test("bare forge encryption from b64", function(assert){
            // generate a random key and IV
            var key = forge.random.getBytesSync(16);
            var iv = forge.random.getBytesSync(16);

            var someBytes = "bare forge encryption to and from b64";

            var cipher = forge.aes.createEncryptionCipher(key, 'CBC');
            cipher.start(iv);
            cipher.update(forge.util.createBuffer(someBytes));
            cipher.finish();
            var encrypted = cipher.output;

            scratch("bare encrypted: " + encrypted.toHex());

            var b64Encrypted = util.encode64(encrypted.data);

            scratch("b64Encrypted: " + b64Encrypted);
            var encryptedBytesFrB64 = util.decode64(b64Encrypted);

            cipher = forge.aes.createDecryptionCipher(key, 'CBC');
            cipher.start(iv);
            cipher.update(util.createBuffer(encryptedBytesFrB64));
            cipher.finish();

            scratch("bare decrypted: " + cipher.output.data);
            assert.ok(cipher.output.data == someBytes);
        });

        q.test("bare forge encryption using getKey", function(assert){
            // generate a random key and IV
            var gotKey = createKey();
            var key = gotKey.key;//forge.random.getBytesSync(16);
            var iv = gotKey.iv;//forge.random.getBytesSync(16);

            var someBytes = "bare forge encryption using getKey";

            var cipher = forge.aes.createEncryptionCipher(key, 'CBC');
            cipher.start(iv);
            cipher.update(util.createBuffer(someBytes));
            cipher.finish();
            var encrypted = cipher.output;

            scratch("bare encrypted (toHex): " + encrypted.toHex());

            var b64Encrypted = util.encode64(encrypted.data);

            scratch("b64Encrypted: " + b64Encrypted);
            var encryptedBytesFrB64 = util.decode64(b64Encrypted);

            cipher = forge.aes.createDecryptionCipher(key, 'CBC');
            cipher.start(iv);
            cipher.update(util.createBuffer(encryptedBytesFrB64));
            cipher.finish();

            scratch("bare decrypted: " + cipher.output.data);
            assert.ok(cipher.output.data == someBytes);
        });


        q.test("Test getKey", function(assert){
            var k = createKey(),
                decoded = util.decode64(k.base64Key);

            scratch("key: " + k.key);
            assert.equal(decoded, k.key);
        });

    }

    app.pageActivated("tests", function(page, data){
        runAllTests();
    });

    return {
        run: runAllTests
    }
})(QUnit, jQuery, bam, bam.app("stickerize"), stickerize || {}, secureChannel, secureChannelServer);