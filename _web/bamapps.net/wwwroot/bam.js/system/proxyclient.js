let proxyclient = (function(){
    "use strict"
    let requestJson = require('request-json'),    
        _ = require('lodash'),
        request = require('request'),
        url = require('url'),
        hosts = {},
        currentProtocol = 'http://',
        currentHost = 'gloo-test.bamapps.net',
        currentPath = '/serviceproxy/proxies.js',
        proxies = {};

    function getApiRoot(){
        return currentProtocol + currentHost + '/';
    }

    function load (uri){
        let vm = require('vm');
        console.log('loading ' + uri);
        return new Promise((resolve, reject) => {
            try {
                request(uri, (err, res, body) => {                    
                    if(err){
                        console.log(err);
                        reject(err);
                    }
                    let proxyContext = {
                            proxy: function(){
                                // functionality not needed.
                            },
                            invoke: function(className, method, args, format, options){
                                return new Promise((invRes, invRej) => {                                
                                    let methodUrl = `${getApiRoot()}${className}/${method}.json`,
                                        strings = [];

                                    for (let i = 0; i < args.length; i++) {
                                        strings.push(JSON.stringify(args[i]));
                                    }
                                    var params = JSON.stringify(strings);
                                    let client = requestJson.createClient(getApiRoot());
                                    client.post(`${className}/${method}.json`, {jsonParams: params}, (err, res, body) => {                                        
                                        if (err) {
                                            invRej(err);
                                        } else {
                                            if(undefined !== options && _.isFunction(options.success)){
                                                options.success(body);
                                            }                                           
                                            invRes(body);
                                        }
                                    })
                                })
                            }
                        },
                        ctx = vm.createContext({bam: proxyContext, dao: {}, jQuery: _, window: {}});
                        
                    vm.runInContext(body, ctx);
                    let host = url.parse(uri).host;
                    hosts[host] = proxyContext;

                    for(let proxyName in proxyContext)  {
                        let proxy = _.extend({}, {url: uri}, proxyContext[proxyName]);
                        proxies[proxyName] = proxy;
                        proxies[uri] = proxy;                        
                    }
                    resolve(proxies);
                });
            } catch(e) {
                console.log('error getting uri: ' + e.toString());
                reject(e);
            }
        });
    }

    function resolveProxy(host, proxyName){
        console.log('resolveProxy');
        if(proxyName){              
            console.log('with proxyName');
            return hosts[host][proxyName];
        }else{
            console.log('with host only');
            return hosts[host];
        }
    }

    return function(options){
        let config = _.extend({
            log: console.log
        }, options);

        return {
            setHost: function(host){
                currentHost = host;
                return this;
            },
            setPath: function(path){
                currentPath = path;
                return this;
            },
            load: function(uri){
                return load(uri).catch((e)=> config.log(e));
            },
            proxies: function(uri){
                return new Promise((resolve, reject) => {
                    try{                        
                        if(!proxies[uri]){
                            load(uri).then(resolve).catch(reject);
                        }else{
                            resolve(proxies);
                        }
                    }catch(e){
                        reject(e);
                    }
                })
            },
            /**
             * Downloads all proxies from the specified host
             * @param {string} host - the host including port if not 80.  For example, localhost:9100.  Should not include protocol.
             * @param {string} name - the name of the proxy to retrieve.
             */
            proxy: function(host, name){
                config.log('proxy: ' + host + ' ' + name);
                currentHost = host;
                return new Promise((resolve, reject) => {
                    try{
                        if (!hosts[host]) {
                            config.log('loading ' + currentProtocol + host + currentPath);
                            load(currentProtocol + host + currentPath)
                                .then(() => resolve(resolveProxy(host, name)))
                                .catch(reject);
                        } else {
                            resolve(resolveProxy(host, name));
                        }
                    } catch(e) {
                        config.log('error in proxy method: ' + e.toString());
                        reject(e);
                    }
                })
            }
        }
    }
})()

module.exports = proxyclient;