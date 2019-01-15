/*
	Copyright © Bryan Apellanes 2015  
*/
var secureChannel = (function($, _, b, sc){
    "use strict";
    var pki = forge.pki,
        random = forge.random,
        aes = forge.aes,
        util = forge.util;

    function Instant(){
        var now = new Date();
        this.Month = now.getUTCMonth() + 1;
        this.Day = now.getUTCDate();
        this.Year = now.getUTCFullYear();
        this.Hour = now.getUTCHours();
        this.Minute = now.getUTCMinutes();
        this.Second = now.getUTCSeconds();
        this.Millisecond = now.getUTCMilliseconds();

        this.toString = function(){
            return this.Month + "/" + this.Day + "/" + this.Year + ";" + this.Hour + "." + this.Minute + "." + this.Second + "." + this.Millisecond;
        }
    }

    b.secureInvoke = function(className, method, args, format, options){
        if(!$.isArray(args)){
            var a = [];
            a.push(args);
            args = a;
        }

        function getPostData(session){
            var params = [];
            _.each(args, function(arg){
                params.push(JSON.stringify(arg));
            });

            params = JSON.stringify(params);
            var jsonParams = JSON.stringify({jsonParams: params});

            var secureParams = [JSON.stringify(className), JSON.stringify(method), JSON.stringify(jsonParams)];
            secureParams = JSON.stringify({jsonParams: secureParams});
            return {
                plain: secureParams,
                cipher: session.encrypt(secureParams)
            }
        }

        function createValidationToken(session, plainPostData){
            var nonce = new Instant().toString(),
                hash = _.sha256(nonce + ":" + plainPostData),
                hashCipher = session.rsaEncrypt(hash),
                nonceCipher = session.rsaEncrypt(nonce);

            return {
                HashCipher: hashCipher,
                NonceCipher: nonceCipher
            }
        }

        var def = $.Deferred(function(){
            var prom = this;
            secureChannel.startSession()
                .then(function(session){
                    var root = b.proxyRoot(b[className].proxyName),
                        url = root + "SecureChannel/Invoke.json?nocache=" + b.randomString(4) + "&",
                        postData = getPostData(session),
                        validationToken = createValidationToken(session, postData.plain),
                        config = {
                            url: url,
                            dataType: "json",
                            data: postData.cipher,
                            global: false,
                            crossDomain: false,
                            type: "POST",
                            headers: {
                                "X-Bam-Sps-Session": _.getCookie("SPSSESS"),
                                "X-Bam-Validation-Token": validationToken.HashCipher,
                                "X-Bam-Timestamp": validationToken.NonceCipher,
                                "X-Bam-Padding": "true"
                            },
                            contentType: "text/plain; charset=utf-8"
                        };

                    $.extend(config, options);

                    $.ajax(config)
                        .done(function(r){
                            if(r.Success){
                                var json = session.decrypt(r.Data);
                                prom.resolve(JSON.parse(json));
                            }else{
                                prom.reject(r.Message);
                            }
                        })
                        .fail(function(r){
                            prom.reject(r);
                        });
            });
        });

        return def.promise();
    };

    function _resolveSession(prom , opts){
        var session = secureChannel.session;
        _.extend(session, opts);
        prom.resolve(session);
    }

    function _rejectSession(prom, opts){
        var session = secureChannel.session;
        _.extend(session, opts);
        prom.reject(session);
    }

    function _createAesKey(){
        var key = random.getBytesSync(16),
            base64Key = util.encode64(key),
            iv = random.getBytesSync(16),
            base64IV = util.encode64(iv);

        return {
            key: key,
            base64Key: base64Key,
            iv: iv,
            base64IV: base64IV
        }
    }

    var sessionStarter = null;
    function startSession(){

        if(sessionStarter === null){
            var def = $.Deferred(function(){
                var prom = this; // the deferred object

                sc.initSession(new Instant())
                    .done(function(r){
                        if(r.Success){
                            var createdKey = _createAesKey(),
                                publicKey = pki.publicKeyFromPem(r.Data.PublicKey),
                                key = createdKey.base64Key,
                                keyHash = _.sha256(key),
                                keyCipher = publicKey.encrypt(key),
                                keyCipherB64 = util.encode64(keyCipher),
                                keyHashCipher = publicKey.encrypt(keyHash),
                                keyHashCipherB64 = util.encode64(keyHashCipher),
                                iv = createdKey.base64IV,
                                ivHash = _.sha256(iv),
                                ivCipher = publicKey.encrypt(iv),
                                ivCipher64 = util.encode64(ivCipher),
                                ivHashCipher = publicKey.encrypt(ivHash),
                                ivHashCipher64 = util.encode64(ivHashCipher),
                                clientId = r.Data.ClientIdentifier;

                            sc.setSessionKey({
                                PasswordCipher: keyCipherB64,
                                PasswordHashCipher: keyHashCipherB64,
                                IVCipher: ivCipher64,
                                IVHashCipher: ivHashCipher64,
                                UsePkcsPadding: true
                            })
                            .done(function(r){
                                if(r.Success){
                                    var session ={
                                        publicKey: publicKey,
                                        symmetricKey: createdKey.key,
                                        symmetricIV: createdKey.iv,
                                        symmetricKey64: createdKey.base64Key,
                                        symmetricIV64: createdKey.base64IV,
                                        started: true,
                                        clientId: clientId
                                    };
                                    _resolveSession(prom, session);
                                }else{
                                    _rejectSession(prom, {message: r.Message});
                                }
                            })
                            .fail(function(r){
                                _rejectSession(prom, {message: r.Message});
                            })
                        }else{
                            _rejectSession(prom, {message: r.Message});
                        }
                    })
                    .fail(function(r){
                        _rejectSession(prom, {message: r.Message});
                    });

            });

            sessionStarter = def.promise();
        }

        return sessionStarter;
    }

    return _.extend({
        startSession: startSession,
        session: {
            message: "",
            started: false,
            initializer: null,
            clientId: "",
            publicKey: {}, // set by startSession; should be a forge public rsa key
            symmetricKey: {}, // set by startSession
            symmetricIV: {}, // set by startSession
            createAesKey: _createAesKey,
            aes: {
                encrypt: function(string, key, iv){
                    var encryptor = aes.createEncryptionCipher(key, "CBC"),
                        data = string + _.randomString(2); // random salt gets desalinated by server

                    encryptor.start(iv);
                    encryptor.update(util.createBuffer(data));
                    encryptor.finish();
                    return util.encode64(encryptor.output.data);
                },
                decrypt: function(b64Cipher, key, iv){
                    var decryptor = aes.createDecryptionCipher(key, 'CBC'),
                        encryptedBytes = util.decode64(b64Cipher);

                    decryptor.start(iv);
                    decryptor.update(util.createBuffer(encryptedBytes));
                    decryptor.finish();

                    return decryptor.output.data.substring(0, decryptor.output.data.length - 2); // truncate 2 char salt
                }
            },
            rsaEncrypt: function(data){
                return util.encode64(this.publicKey.encrypt(data));
            },
            encrypt: function(data){
                return this.aes.encrypt(data, this.symmetricKey, this.symmetricIV);
            },
            decrypt: function(cipher){
                return this.aes.decrypt(cipher, this.symmetricKey, this.symmetricIV);
            }
        }
    }, secureChannelServer);
})(jQuery, _, bam, secureChannelServer);