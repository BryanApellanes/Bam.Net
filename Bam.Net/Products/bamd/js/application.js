var application = (function ($, _, d, b, w) {
    "use strict";

    // -- ctors
    /**
     * @constructor
     * @param from
     * @param to
     * @param fn
     */
    function transitionHandler(name, from, to, fn) {
        var the = this,
            transition = fn;

        this.name = name;
        this.from = from;
        this.to = to;

        if (_.isString(name) && _.isString(from) && _.isFunction(to)) {
            this.from = name;
            this.to = from;
            this.name = this.from + "to" + this.to;
            transition = to;
        }

        var _start = function () {
            $(the).trigger("start");
        };

        this.play = function (data) {
            _start();
            transition(the, data);
            _end();
        };

        var _end = function () {
            $(the).trigger("end", the);
        }
    }

    /**
     * @constructor
     * @param n
     * @param app
     */
    function page(n, app) {
        var the = this;

        this.name = n;
        this.currentState = "initial";
        this.previousState = the.currentState;
        this.states = [];
        this.stateTransitions = {};
        this.stateTransitionFilters = {};
        this.uiState = null;
        this.goodByeEffect = app.goodByeEffect;
        this.helloEffect = app.helloEffect;
        this.stateGoodByeEffects = {};
        this.stateHelloEffects = {};
        this.stateGoodByeEffect = app.goodByeEffect;
        this.stateHelloEffect = app.helloEffect;
        this.contentSelector = app.contentSelector;
        this.title = null; // set on line 102
        this.content = null; // gets set in load line 100
        this.loaded = false;
        this.isActivated = false; // set by b.activate, determines if plugins have been activated and activation handlers called
        this.appName = app.name;
        this.linkTags = [];
        this.app = app;

        /**
         * data set by transitionTo (which is called by b.setState) to
         * allow data passing from state to state
         * @type {{}}
         */
        this.stateData = {};

        this.load = function () {
            return $.ajax({
                url: b.getAppRoot() + "bam/apps/" + the.appName + "/pages/" + the.name + ".html?nocache=" + b.randomString(4),
                dataType: "html",
                success: function (html) {
                    var p = document.createElement("iframe");
                    $(p).append(html);
                    the.title = $("title", p).text().trim();
                    $("title", p).remove();
                    $("link", p).each(function (i, v) {
                        the.linkTags.push(v);
                    });
                    the.content = $(p);//$(the.contentSelector, p);
                    the.loaded = true;
                }
            }).promise();
        };

        this.hello = function (d) {
            var _hello = function (_d) {
                document.title = the.title;

                if (the.uiState != null && the.isActivated) {
                    $(the.contentSelector).replaceWith(the.uiState).show(the.helloEffect);
                    the.activate(_d);
                } else {
                    $(the.contentSelector).empty();
                    $(the.contentSelector).hide().append(the.content.html()).show({
                        effect: the.helloEffect,
                        complete: function () {
                            the.loadStates(_d);
                        }
                    });
                }
            };

            if (the.loaded) {
                _hello(d);
            } else {
                the.load()
                    .done(function () {
                        _hello(d);
                    });
            }
        };

        /**
         * Play the hide effect and call the specified complete handler.
         * This method is called by the default transitionHandler and
         * should be called by custom implementations (of the transitionHandler)
         * if a hide effect is not provided.
         * @param complete function
         */
        this.goodBye = function (complete) {
            if (_.isUndefined(complete)) {
                complete = function () { };
            }
            if (the.isActivated) {
                var uiClone = $(the.contentSelector).clone(),
                    $parent = $(the.contentSelector).parent();

                the.uiState = $(the.contentSelector).detachAndReplaceWith(uiClone);
                //$(the.contentSelector).html(uiClone.html());
                //$parent.append(uiClone); // this causes the containers to move around need to fix this
                _.each(the.linkTags, function (v) {
                    $(v).detach();
                })
            }

            $(the.contentSelector).show().hide({
                effect: the.goodByeEffect,
                complete: complete
            });
        };

        /**
         * Load all states in the current page and prepare transitions between
         * each
         */
        this.loadStates = function (d) {
            var app = the.app;
            the.states = ["initial"];
            $("[data-state]", $(the.contentSelector)).each(function (i, v) {
                var state = $(v).hide().attr("data-state");
                if (!_.contains(the.states, state)) {
                    the.states.push(state);
                }
            });

            // create a state transition between all states including state to self
            _.each(the.states, function (s, i) { // state, index
                the.setStateTransition(s, s, app.defaultPageStateTransitionHandler);
                _.each(_.rest(the.states, i + 1), function (ns) { // next state
                    the.setStateTransition(s, ns, app.defaultPageStateTransitionHandler);
                    the.setStateTransition(ns, s, app.defaultPageStateTransitionHandler);
                })
            });

            var effect = the.getStateHelloEffect("initial");
            $("[data-state=initial]", app.container()).show(effect);
            the.activate(d);
        };

        this.activate = function (d) {
            activate(the, d);
        };

        /**
         * Set the name of the effect to use when the specified state
         * goes goodBye (is hidden/transitions away).
         * @param state
         * @param e
         */
        this.setStateGoodByeEffect = function (state, e) {
            the.stateGoodByeEffects[state] = e;
        };

        this.getStateGoodByeEffect = function (state) {
            return the.stateGoodByeEffects[state] || the.stateGoodByeEffect;
        };

        /**
         * Set the name of the effect to use when the specified state
         * says hello (is shown/transitions to).
         * @param state
         * @param e
         */
        this.setStateHelloEffect = function (state, e) {
            the.stateHelloEffects[state] = e;
        };

        this.getStateHelloEffect = function (state) {
            return the.stateHelloEffects[state] || the.stateHelloEffect;
        };

        this.setStateTransition = function (f, t, impl) {
            the.stateTransitions.from = the.stateTransitions.from || {};
            the.stateTransitions.from[f] = the.stateTransitions.from[f] || {};
            the.stateTransitions.from[f].to = the.stateTransitions.from[f].to || {};
            the.stateTransitions.from[f].to[t] = the.stateTransitions.from[f].to[t] || {};
            var th = new transitionHandler(f + "To" + t, f, t, impl);
            th.appName = the.appName;
            the.stateTransitions.from[f].to[t] = th;
        };

        /**
         * Set a filter to be run when transitioning from the specified
         * state f to the specified state t.  Can be used to stop the transition
         * or direct to a different state by analyzing the current page state. If
         * the function signature used is (string, function) the filter will be
         * added for "any" state to the specified state.
         * @param f
         * @param t
         * @param filter
         */
        this.setStateTransitionFilter = function (f, t, filter) {
            if (_.isUndefined(filter) && _.isFunction(t) && _.isString(f)) {
                // f = "to"
                // t = filter function
                _.each(the.states, function (st, i) {
                    the.setStateTransitionFilter(st, f, t);
                });
            } else {
                the.stateTransitionFilters.from = the.stateTransitionFilters.from || {};
                the.stateTransitionFilters.from[f] = the.stateTransitionFilters.from[f] || {};
                the.stateTransitionFilters.from[f].to = the.stateTransitionFilters.from[f].to || {};
                the.stateTransitionFilters.from[f].to[t] = the.stateTransitionFilters.from[f].to[t] || {};
                the.stateTransitionFilters.from[f].to[t] = filter;
            }
        };

        this.transitionTo = function (ts, data) {
            if (_.isUndefined(data)) {
                data = the.stateData;
            }
            if (the.stateTransitions.from &&
                the.stateTransitions.from[the.currentState] &&
                the.stateTransitions.from[the.currentState].to &&
                the.stateTransitions.from[the.currentState].to[ts]) {
                try {
                    if (_.isFunction(the.stateTransitionFilters.from[the.currentState].to[ts])) {
                        var result = the.stateTransitionFilters.from[the.currentState].to[ts](the.stateTransitions.from[the.currentState].to[ts], data);
                        if (!_.isUndefined(result)) {
                            if (result == false) {
                                return;
                            } else if (_.isString(result) && !_.isUndefined(the.stateTransitions.from[the.currentState].to[result])) {
                                ts = result;
                            }
                        }
                    }
                } catch (e) {
                    // play the transition
                }
                the.stateTransitions.from[the.currentState].to[ts].play(data);
                the.stateData = data;
                the.setState(ts);
            }
        };

        this.setState = function (s) {
            the.previousState = the.currentState;
            the.currentState = s;
        }
    }

    /**
     * @constructor
     */
    function history(appName) {
        var previous = -1,
            current = -1,
            next = -1,
            pageStack = [],
            the = this;

        this.appName = appName;

        this.init = function () {
            $(w).on("popstate", function (e) {
                var p = b.app(the.appName).pages[e.originalEvent.state];
                if (!_.isNull(p) && !_.isUndefined(p)) {
                    p.navigatingHistory = true; // prevents the addition of a new entry into the history stack
                    b.app(the.appName).transitionToPage(e.originalEvent.state, p.stateData);
                }
            });
            return this;
        };

        this.add = function (page) {
            if (page.navigatingHistory) {
                delete page.navigatingHistory;
            } else {
                previous = current;
                ++current;
                next = current + 1;
                pageStack.push(page);

                // enables the browser back button to navigate back and forth in the app
                // works with popstate listener in the init method above
                w.history.pushState(page.name, page.name, "#" + page.name);
            }
        };

        this.back = function () {
            if (the.canBack()) {
                current = previous;
                previous = previous - 1;
                next = current + 1;
                var page = pageStack[current];
                page.navigatingHistory = true;
                page.app.transitionToPage(page.name, page.stateData);
                setNavButtonState();
            }
        };

        this.forward = function () {
            if (the.canForward()) {
                previous = current;
                current = next;
                next = current + 1;
                var page = pageStack[current];
                page.navigatingHistory = true;
                page.app.transitionToPage(page.name, page.stateData);
                setNavButtonState();
            }
        };

        this.canBack = function () {
            return previous >= 0;
        };

        this.canForward = function () {
            return next > 0 && next < pageStack.length;
        };
    }
    // -- end ctors

    function setNavButtonState(appName) {
        var $back = $("[data-navigate=back][data-app=" + appName + "]"),
            $forward = $("[data-navigate=forward][data-app=" + appName + "]");
        if (!b.app(appName).history.canForward()) {
            $forward.addClass("disabled");
        } else {
            $forward.removeClass("disabled");
        }

        if (!b.app(appName).history.canBack()) {
            $back.addClass("disabled");
        } else {
            $back.removeClass("disabled");
        }
    }

    function run(startPageName) {
        if (_.isUndefined(startPageName)) {
            startPageName = "index";
        }
        return _loadPages(this.name, startPageName);
    }

    /**
     * Render the specified view (contained in /bam/apps/{appName}/dust/)
     * @param viewName
     * @param data
     * @param sOrEl either an element or a selector or a function
     * @returns jQuery promise, when resolved returns the rendered html.  If opts is
     *          an element or selector the element will be filled with the rendered html
     *          using $(opts).html(<html>);  If opts is a function the rendered html will
     *          be passed to the done handler.
     */
    function view(viewName, data, sOrEl) {
        var app = this,
            def = $.Deferred(function () {
                var prom = this;
                b.view(viewName, data, sOrEl)
                    .done(function (r) {
                        prom.resolve(r);
                    })
                    .fail(function (r) {
                        prom.reject(r);
                    })
            });

        return def.promise();
    }

    function viewData(el, viewName) {
        return $(el).data(_.format("{0}.{1}", b.app.name, viewName));
    }

    function _loadPages(subAppName, startPageName) {
        // load html from ~/pages/ using Pages
        return _.act("bam", "getpages", { bamAppName: subAppName }).done(function (result) {
            if (result.Success) {
                var vals = result.Data,
                    app = b.app(subAppName);
                _.each(vals, function (cp, i) {
                    // create the page
                    app.pages[cp] = new page(cp, app);
                    app.createPageTransition(cp + "To" + cp, cp, cp, app.defaultPageTransitionHandler);
                    _.each(_.rest(vals, i + 1), function (np) {
                        var currentToNextName = cp + "To" + np,
                            nextToCurrentName = np + "To" + cp;
                        // create a transition to all the other pages
                        app.createPageTransition(currentToNextName, cp, np, app.defaultPageTransitionHandler);
                        // and back again
                        app.createPageTransition(nextToCurrentName, np, cp, app.defaultPageTransitionHandler);
                    });
                });

                app.pageTransition(startPageName, startPageName)
                    .preLoadPages();
            } else {
                $(b.app(subAppName).contentSelector).text(result.Message);
            }
        })
    }

    function refresh(el){ // this gets attached to the app object "this.name" will be the app name
        var item = $(el).parentsUntil("[itemscope]").parent();
        renderViews(item, this.name);
    }

    function renderViews(container, appName) {
        container = container || document;

        function render(attr, bApp) {
            var selector = _.format("[{0}]", attr);
            $(selector, container).each(function (i, v) {

                var attrValue = $(v).attr(attr),
                    viewName = bApp ? _.format("{0}.{1}", appName, attrValue) : attrValue,
                    viewModelName = $(v).attr("data-view-model"),
                    app = b.app(appName),
                    viewModel = app.viewModels[viewModelName],
                    ds = $(v).attr("data-source") || viewModelName,
                    viewData = viewModel != null && viewModel.view != null ? viewModel.view : null,
                    viewModelCtor = _.getFunction(viewModelName);

                if (_.isNull(viewData)) {
                    viewData = _.isFunction(models) ? models().getModel(ds) : undefined;
                    if (_.isFunction(viewData)) {
                        viewData = viewData(viewName);
                    }
                }

                if ((_.isNull(viewData) || _.isUndefined(viewData)) && _.isFunction(viewModelCtor)) {
                    viewData = new viewModelCtor(v, app);
                    $(v).data("viewModel", viewData);
                    if (viewData.view) {
                        viewData = viewData.view;
                    }
                }

                if (!_.isUndefined(viewData) && !_.isNull(viewData) && _.isFunction(viewData.init)) {
                    var prom = viewData.init();
                    if (!_.isObject(prom) || !_.isFunction(prom.then)) {
                        throw new Error("init must return a promise");
                    }
                    prom.then(function () {
                        b.app(appName).view(viewName, viewData || {}, v).then(function(){
                            b.app(appName).attachModels();
                        });
                    })
                } else {
                    b.app(appName).view(viewName, viewData || {}, v).then(function(){
                        b.app(appName).attachModels();
                    });
                }
            });
        }

        render("data-app-view", true);
        render("data-view", false);
    }

    function activateNavigation(appName) {
        $("[data-navigate-to]", b.app(appName).container()).each(function (i, v) {
            var tp = $(v).attr("data-navigate-to"),
                navOn = $(v).attr("data-navigate-on") || "click",
                data = $.dataSetOptions(v);
            $(v).off(navOn).on(navOn, function () {
                b.app(appName).navigateTo(tp, data);
            })
        });
    }

    function activateBackButtons(appName) {
        var $back = $("[data-navigate=back][data-app-name=" + appName + "]");
        $back.each(function (i, v) {
            $(v).off("click").on("click", function () {
                b.app(appName).history.back();
                setNavButtonState(appName);
            })
        });
    }

    function activateForwardButtons(appName) {
        var $forward = $("[data-navigate=forward][data-app-name=" + appName + "]");
        $forward.each(function (i, v) {
            $(v).off("click").on("click", function () {
                b.app(appName).history.forward();
                setNavButtonState(appName);
            })
        });
    }

    function activateStateEvents(appName) {
        var app = b.app(appName);
        $("[data-set-state]", app.container()).each(function (i, v) {
            var s = $(v).attr("data-set-state"),
                on = $(v).attr("data-set-state-on") || "click",
                data = $.dataSetOptions(v);
            $(v).off(on).on(on, function () {
                b.app(appName).goToState(s, data);
            })
        });
    }

    function attachModels(appName) {
        $("[itemscope],[data-view-model]").each(function (i, scopeElement) {
            var viewModelName = $(scopeElement).attr("data-view-model") || null;
            if (!_.isNull(viewModelName)) {
                var app = b.app(appName),
                    viewModel = app.viewModels[viewModelName],
                    viewModelCtor = _.getFunction(viewModelName);

                if (_.isUndefined(viewModel) && _.isFunction(viewModelCtor)) {
                    viewModel = $(scopeElement).data("viewModel"); // check if it has been constructed by the render phase
                    if (!_.isObject(viewModel)) {
                        viewModel = new viewModelCtor(scopeElement, app);
                        $(scopeElement).data("viewModel", viewModel);
                    }
                }

                if (!_.isUndefined(viewModel)) {
                    if (viewModel.model) {
                        viewModel = viewModel.model;
                    }

                    if (_.isFunction(viewModel.init)) {
                        viewModel.init();
                    }

                    if (_.isFunction(viewModel.activate)) {
                        viewModel.activate(scopeElement);
                    }

                    if (!_.isUndefined($(scopeElement).attr("itemscope"))) {
                        _.setItem(scopeElement, viewModel);
                    }
                }
            }
        });
    }

    function activate(page, d) {
        var app = page.app;

        b.promise(function(){
                renderViews(document, page.appName);
            }).then(function(){
                app.container().activate();

                activateNavigation(page.appName);

                activateBackButtons(page.appName);

                activateForwardButtons(page.appName);

                activateStateEvents(page.appName);

                _.each(page.linkTags, function (v) { // populated by .load
                    $(v).remove();
                    $("head").append(v);
                });

                if (_.isFunction(app.pageActivationHandlers[page.name])) {
                    app.pageActivationHandlers[page.name](page, d);
                }
                _.each(app.anyPageActivationHandlers, function (fn) {
                    fn(page, d);
                });

                attachModels(page.appName);
                page.isActivated = true;
            });
    }

    var apps = {};

    var app = function (appName, renderInSelector) {
        if (_.isUndefined(apps[appName])) {
            apps[appName] = {
                /** conf **/
                pages: {},
                currentPage: "start",
                previousPage: "start",
                pageTransitions: {},
                pageTransitionFilters: {},
                pageActivationHandlers: {},
                anyPageActivationHandlers: [],
                appData: {},
                viewModels: {},
                goodByeEffect: "fade",
                helloEffect: "fade",
                attachModels: function(){
                    attachModels(appName);
                },
                /**
                 * Set the data filter on the specified transition
                 * @param from
                 * @param to
                 * @param filter
                 */
                setPageTransitionFilter: function (from, to, filter) {
                    this.pageTransitionFilters.from = this.pageTransitionFilters.from || {};
                    this.pageTransitionFilters.from[from] = this.pageTransitionFilters.from[from] || {};
                    this.pageTransitionFilters.from[from].to = this.pageTransitionFilters.from[from].to || {};
                    this.pageTransitionFilters.from[from].to[to] = filter;

                    return this;
                },
                setPageTransition: function (f, t, th) {
                    this.pageTransitions.from = this.pageTransitions.from || {};
                    this.pageTransitions.from[f] = this.pageTransitions.from[f] || {};
                    this.pageTransitions.from[f].to = this.pageTransitions.from[f].to || {};
                    this.pageTransitions.from[f].to[t] = th;
                },
                createPageTransition: function (n, f, t, impl) {
                    var th = new transitionHandler(n, f, t, impl);
                    th.appName = appName;
                    this.setPageTransition(f, t, th);
                    return th;
                },
                transitionToPage: function (to, data) {
                    this.pageTransition(this.currentPage, to, data);
                },
                pageTransition: function (from, to, data) {
                    if (_.isUndefined(to)) {
                        to = from;
                    }

                    try {
                        if (_.isFunction(this.pageTransitionFilters.from[from].to[to])) {
                            var result = this.pageTransitionFilters.from[from].to[to](this.pageTransitions.from[from].to[to], data);
                            if (!_.isUndefined(result)) {
                                if (result == false) {
                                    return this;
                                }
                            }
                        }
                    } catch (e) {
                        // play the transition;
                    }

                    this.pageTransitions.from[from].to[to].play(data);
                    this.previousPage = from;
                    this.currentPage = to;
                    var p = this.pages[to],
                        the = this;
                    p.app.history.add(p);
                    setNavButtonState(p.appName);

                    return this;
                },
                preLoadPages: function () {
                    var pages = this.pages;
                    _.each(pages, function (page, i) {
                        if (!page.loaded && _.isFunction(page.load)) {
                            page.load();
                        }
                    })
                },
                goToState: function (ts, data) {
                    if (ts == "" || _.isUndefined(ts)) {
                        ts = "initial";
                    }
                    this.pages[this.currentPage].transitionTo(ts, data);
                    return this;
                },
                navigateTo: function (pageName, data) {
                    this.transitionToPage(pageName, data);
                    return this;
                },
                pageActivated: function (pageName, handler) {
                    if (_.isFunction(pageName) && _.isUndefined(handler)) {
                        this.anyPageActivationHandlers.push(pageName); // its a function
                    } else {
                        this.pageActivationHandlers[pageName] = handler;
                    }
                    return this;
                },
                anyPageActivated: function (handler) {
                    this.anyPageActivationHandlers.push(handler);
                    return this;
                },
                setViewModel: function (name, viewModel) {
                    this.viewModels[name] = viewModel;
                    return this;
                },
                defaultPageTransitionHandler: function (tx, data) {
                    var the = this,
                        app = b.app(tx.appName);
                    app.pages[tx.from].goodBye(function () {
                        app.pages[tx.to].hello(data);
                    });
                },
                defaultPageStateTransitionHandler: function (tx, data) {
                    var app = b.app(tx.appName),
                        p = app.pages[app.currentPage],
                        $content = $(app.contentSelector);
                    $("[data-state]", $content).hide(p.getStateGoodByeEffect(tx.from));
                    $("[data-state=" + tx.to + "]", $content).show(p.getStateHelloEffect(tx.to));
                },
                /** end conf **/
                name: appName,
                isApp: true,
                run: run,
                refresh: refresh,
                view: view,
                viewData: viewData,
                renderViews: renderViews,
                contentSelector: renderInSelector || "[data-app=" + appName + "]",
                container: function () {
                    return $(app(appName).contentSelector);
                },
                history: new history(appName).init(),
                writeTemplate: function (obj) {
                    return _.act("dust", "writeTemplate", { appName: b.app.name, json: JSON.stringify(obj) });
                },
                setGoodByeEffect: function (ef, sp) { // effect, (bool)setPages
                    if (!_.isUndefined(ef)) {
                        this.goodByeEffect = ef;
                        if (sp) {
                            _.each(this.pages, function (v) {
                                v.stateGoodByeEffect = ef;
                            })
                        }
                    }
                    return this.goodByeEffect;
                },
                setHelloEffect: function (ef, sp) {
                    if (!_.isUndefined(ef)) {
                        this.helloEffect = ef;
                        if (sp) {
                            _.each(this.pages, function (v) {
                                v.stateHelloEffect = ef;
                            })
                        }
                    }
                    return this.helloEffect;
                }
            };
        }

        return apps[appName];
    };

    b.app = app;

    b.activateApps = function () {
        $("[data-app]").each(function (i, o) {
            var appName = $(o).attr("data-app"),
                startPage = $(o).attr("data-start") || "start";

            b.app(appName).run(startPage);
        });
    };

    $(function () {
        if (_ !== undefined && _.mixin !== undefined) {
            _.mixin(b);
        }

        b.activateApps();
    });
    return app;
})(jQuery, _, dao, bam, window || {});