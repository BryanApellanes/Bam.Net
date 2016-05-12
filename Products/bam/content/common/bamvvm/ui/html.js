/*
	Copyright © Bryan Apellanes 2015  
*/

function createElement(tagName){
    if(_.isFunction(document.createElement)){
        return document.createElement(tagName);
    }else{
        return {};
    }
}

/**
 * @constructor
 * @param name
 * @param attrsOrText
 */
function tag(name, attrsOrText){
    var element = createElement(name);
    if(!_.isUndefined(attrsOrText) && attrsOrText.isTop){
        attrsOrText.render(function(h){
            $(element).append(h);
        });
    }else if (_.isObject(attrsOrText) && !attrsOrText.isTag){
        _.each(attrsOrText, function(v, k){
            $(element).attr(k.replace('_', '-'), v);
        });
    }else if(_.isString(attrsOrText)){
        $(element).text(attrsOrText);
    }

    this.element = $(element);
    var the = this;

    this.render = function(parent){
        $(parent).append(the.element);
    };

    this.attr = function(k,v){
        this.element.attr(k, v);
    };
    this.html = function(h){
        this.element.html(h);
    };
    this.text = function(txt){
        this.element.text(txt);
    };
    this.css = function(attr, val){
        this.element.css(attr, val);
    };
    this.append = function(el){
        this.element.append(el.element || el);
    };
    this.appendTo = function(opts){
        this.element.appendTo(opts);
    };
    this.child = function(el){
        the.element.append(el.element || el);
    };
    this.addClass = function(c){
        the.element.addClass(c);
    };
    this.isTag = true;
}

function div(attributes){
    var element = new tag("div", attributes);
    return $.extend({}, html(element));
}

function span(attributes){
    var element = new tag("span", attributes);
    return $.extend({}, html(element));
}

var html = (function($, _){

    function getTag(tagName, attrsOrHtmlObj, text) {
        var t = new tag(tagName, attrsOrHtmlObj.currentTag || attrsOrHtmlObj);
        if(tagName.isTag){
            t = tagName.element;
        }else if(tagName.currentTag && tagName.currentTag.element){
            t = tagName.currentTag.element;
        }

        if (_.isString(text)) {
            t.text(text);
        }
        return t;
    }

    return function(tagName, objectAttributes, text){
        var container = createElement("div"),
            firstTag;
        if(_.isString(tagName)){
            firstTag = new tag(tagName, objectAttributes);
        }else if(tagName.isTag){
            firstTag = tagName;
        }else{
                throw new Error("tagName must be a valid html tagName");
        }

        firstTag.text(text);
        _.each(objectAttributes, function(v, k){
            firstTag.attr(k, v);
        });

        return {
            isTop: true,
            root: $(container),
            siblings: [firstTag],
            currentTag: firstTag,
            addRoot: function(tagName, attributes){
                var t = new tag(tagName, attributes);
                this.siblings.push(t.element || t);
                this.currentTag = t;
                return this;
            },
            /**
             * Specifies a hyperlink
             * @param attributes
             * @param text
             * @returns {*}
             */
            a: function(attributes, text){
                var t = getTag("a", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies an abbreviation
             * @param attributes
             * @param text
             * @returns {*}
             */
            abbr: function(attributes, text){
                var t = getTag("abbr", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies an address element
             * @param attributes
             * @param text
             * @returns {*}
             */
            address: function(attributes, text){
                var t = getTag("address", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies an area inside an image map
             * @param attributes
             * @param text
             * @returns {*}
             */
            area: function(attributes, text){
                var t = getTag("area", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies an article
             * @param attributes
             * @param text
             * @returns {*}
             */
            article: function(attributes, text){
                var t = getTag("article", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies content aside from the page content
             * @param attributes
             * @param text
             * @returns {*}
             */
            aside: function(attributes, text){
                var t = getTag("aside", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies sound content
             * @param attributes
             * @param text
             * @returns {*}
             */
            audio: function(attributes, text){
                var t = getTag("audio", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies bold text
             * @param attributes
             * @param text
             * @returns {*}
             */
            b: function(attributes, text){
                var t = getTag("b", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a base URL for all the links in a page
             * @param attributes
             * @param text
             * @returns {*}
             */
            base: function(attributes, text){
                var t = getTag("base", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * For bi-directional text formatting
             * @param attributes
             * @param text
             * @returns {*}
             */
            bdi: function(attributes, text){
                var t = getTag("bdi", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies the direction of text display
             * @param attributes
             * @param text
             * @returns {*}
             */
            bdo: function(attributes, text){
                var t = getTag("bdo", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a long quotation
             * @param attributes
             * @param text
             * @returns {*}
             */
            blockquote: function(attributes, text){
                var t = getTag("blockquote", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies the body element
             * @param attributes
             * @param text
             * @returns {*}
             */
            body: function(attributes, text){
                var t = getTag("body", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Inserts a single line break
             * @param attributes
             * @param text
             * @returns {*}
             */
            br: function(attributes, text){
                var t = getTag("br", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a push button
             * @param attributes
             * @param text
             * @returns {*}
             */
            button: function(attributes, text){
                var t = getTag("button", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Define graphics
             * @param attributes
             * @param text
             * @returns {*}
             */
            canvas: function(attributes, text){
                var t = getTag("canvas", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a table caption
             * @param attributes
             * @param text
             * @returns {*}
             */
            caption: function(attributes, text){
                var t = getTag("caption", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a citation
             * @param attributes
             * @param text
             * @returns {*}
             */
            cite: function(attributes, text){
                var t = getTag("cite", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies computer code text
             * @param attributes
             * @param text
             * @returns {*}
             */
            code: function(attributes, text){
                var t = getTag("code", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies attributes for table columns�
             * @param attributes
             * @param text
             * @returns {*}
             */
            col: function(attributes, text){
                var t = getTag("col", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies groups of table columns
             * @param attributes
             * @param text
             * @returns {*}
             */
            colgroup: function(attributes, text){
                var t = getTag("colgroup", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a command
             * @param attributes
             * @param text
             * @returns {*}
             */
            command: function(attributes, text){
                var t = getTag("command", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Allows for machine-readable data to be provided
             * @param attributes
             * @param text
             * @returns {*}
             */
            data: function(attributes, text){
                var t = getTag("data", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * "Allows for an interactive representation of tree
             * @param attributes
             * @param text
             * @returns {*}
             */
            datagrid: function(attributes, text){
                var t = getTag("datagrid", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * "Specifies an ""autocomplete"" dropdown list"
             * @param attributes
             * @param text
             * @returns {*}
             */
            datalist: function(attributes, text){
                var t = getTag("datalist", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a definition description
             * @param attributes
             * @param text
             * @returns {*}
             */
            dd: function(attributes, text){
                var t = getTag("dd", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies deleted text
             * @param attributes
             * @param text
             * @returns {*}
             */
            del: function(attributes, text){
                var t = getTag("del", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies details of an element
             * @param attributes
             * @param text
             * @returns {*}
             */
            details: function(attributes, text){
                var t = getTag("details", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Defines�a definition term
             * @param attributes
             * @param text
             * @returns {*}
             */
            dfn: function(attributes, text){
                var t = getTag("dfn", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a section in a document
             * @param attributes
             * @param text
             * @returns {*}
             */
            div: function(attributes, text){
                var t = getTag("div", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a definition list
             * @param attributes
             * @param text
             * @returns {*}
             */
            dl: function(attributes, text){
                var t = getTag("dl", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a definition term
             * @param attributes
             * @param text
             * @returns {*}
             */
            dt: function(attributes, text){
                var t = getTag("dt", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies emphasized text�
             * @param attributes
             * @param text
             * @returns {*}
             */
            em: function(attributes, text){
                var t = getTag("em", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies external application or interactive content
             * @param attributes
             * @param text
             * @returns {*}
             */
            embed: function(attributes, text){
                var t = getTag("embed", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a target for events sent by a server
             * @param attributes
             * @param text
             * @returns {*}
             */
            eventsource: function(attributes, text){
                var t = getTag("eventsource", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a fieldset
             * @param attributes
             * @param text
             * @returns {*}
             */
            fieldset: function(attributes, text){
                var t = getTag("fieldset", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies caption for the figure element.
             * @param attributes
             * @param text
             * @returns {*}
             */
            figcaption: function(attributes, text){
                var t = getTag("figcaption", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * "Specifies a group of media content
             * @param attributes
             * @param text
             * @returns {*}
             */
            figure: function(attributes, text){
                var t = getTag("figure", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a footer for a section or page
             * @param attributes
             * @param text
             * @returns {*}
             */
            footer: function(attributes, text){
                var t = getTag("footer", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a form�
             * @param attributes
             * @param text
             * @returns {*}
             */
            form: function(attributes, text){
                var t = getTag("form", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a heading level 1
             * @param attributes
             * @param text
             * @returns {*}
             */
            h1: function(attributes, text){
                var t = getTag("h1", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a heading level 2
             * @param attributes
             * @param text
             * @returns {*}
             */
            h2: function(attributes, text){
                var t = getTag("h2", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a heading level 3
             * @param attributes
             * @param text
             * @returns {*}
             */
            h3: function(attributes, text){
                var t = getTag("h3", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a heading level 4
             * @param attributes
             * @param text
             * @returns {*}
             */
            h4: function(attributes, text){
                var t = getTag("h4", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a heading level 5
             * @param attributes
             * @param text
             * @returns {*}
             */
            h5: function(attributes, text){
                var t = getTag("h5", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a heading level 6
             * @param attributes
             * @param text
             * @returns {*}
             */
            h6: function(attributes, text){
                var t = getTag("h6", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies information about the document
             * @param attributes
             * @param text
             * @returns {*}
             */
            head: function(attributes, text){
                var t = getTag("head", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * "Specifies a group of introductory or navigational aids
             * @param attributes
             * @param text
             * @returns {*}
             */
            header: function(attributes, text){
                var t = getTag("header", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a header for a section or page
             * @param attributes
             * @param text
             * @returns {*}
             */
            hgroup: function(attributes, text){
                var t = getTag("hgroup", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a horizontal rule
             * @param attributes
             * @param text
             * @returns {*}
             */
            hr: function(attributes, text){
                var t = getTag("hr", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies italic text
             * @param attributes
             * @param text
             * @returns {*}
             */
            i: function(attributes, text){
                var t = getTag("i", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies an inline sub window (frame)
             * @param attributes
             * @param text
             * @returns {*}
             */
            iframe: function(attributes, text){
                var t = getTag("iframe", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies an image
             * @param attributes
             * @param text
             * @returns {*}
             */
            img: function(attributes, text){
                var t = getTag("img", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies an input field
             * @param attributes
             * @param text
             * @returns {*}
             */
            input: function(attributes, text){
                var t = getTag("input", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies inserted text
             * @param attributes
             * @param text
             * @returns {*}
             */
            ins: function(attributes, text){
                var t = getTag("ins", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies keyboard text
             * @param attributes
             * @param text
             * @returns {*}
             */
            kbd: function(attributes, text){
                var t = getTag("kbd", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Generates a key pair
             * @param attributes
             * @param text
             * @returns {*}
             */
            keygen: function(attributes, text){
                var t = getTag("keygen", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a label�for a form control
             * @param attributes
             * @param text
             * @returns {*}
             */
            label: function(attributes, text){
                var t = getTag("label", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a title in a fieldset
             * @param attributes
             * @param text
             * @returns {*}
             */
            legend: function(attributes, text){
                var t = getTag("legend", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a list item
             * @param attributes
             * @param text
             * @returns {*}
             */
            li: function(attributes, text){
                var t = getTag("li", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a resource reference
             * @param attributes
             * @param text
             * @returns {*}
             */
            link: function(attributes, text){
                var t = getTag("link", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies marked text
             * @param attributes
             * @param text
             * @returns {*}
             */
            mark: function(attributes, text){
                var t = getTag("mark", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies an image map�
             * @param attributes
             * @param text
             * @returns {*}
             */
            map: function(attributes, text){
                var t = getTag("map", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a menu list
             * @param attributes
             * @param text
             * @returns {*}
             */
            menu: function(attributes, text){
                var t = getTag("menu", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies meta information
             * @param attributes
             * @param text
             * @returns {*}
             */
            meta: function(attributes, text){
                var t = getTag("meta", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies measurement within a predefined range
             * @param attributes
             * @param text
             * @returns {*}
             */
            meter: function(attributes, text){
                var t = getTag("meter", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies navigation links
             * @param attributes
             * @param text
             * @returns {*}
             */
            nav: function(attributes, text){
                var t = getTag("nav", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a noscript section
             * @param attributes
             * @param text
             * @returns {*}
             */
            noscript: function(attributes, text){
                var t = getTag("noscript", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies an embedded object
             * @param attributes
             * @param text
             * @returns {*}
             */
            object: function(attributes, text){
                var t = getTag("object", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies an ordered list
             * @param attributes
             * @param text
             * @returns {*}
             */
            ol: function(attributes, text){
                var t = getTag("ol", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies an option group
             * @param attributes
             * @param text
             * @returns {*}
             */
            optgroup: function(attributes, text){
                var t = getTag("optgroup", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies an option in a drop-down list
             * @param attributes
             * @param text
             * @returns {*}
             */
            option: function(attributes, text){
                var t = getTag("option", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies some types of output
             * @param attributes
             * @param text
             * @returns {*}
             */
            output: function(attributes, text){
                var t = getTag("output", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a paragraph
             * @param attributes
             * @param text
             * @returns {*}
             */
            p: function(attributes, text){
                var t = getTag("p", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a parameter for an object
             * @param attributes
             * @param text
             * @returns {*}
             */
            param: function(attributes, text){
                var t = getTag("param", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies preformatted text
             * @param attributes
             * @param text
             * @returns {*}
             */
            pre: function(attributes, text){
                var t = getTag("pre", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies progress of a task of any kind
             * @param attributes
             * @param text
             * @returns {*}
             */
            progress: function(attributes, text){
                var t = getTag("progress", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a short quotation
             * @param attributes
             * @param text
             * @returns {*}
             */
            q: function(attributes, text){
                var t = getTag("q", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a ruby annotation (used in East Asian typography)
             * @param attributes
             * @param text
             * @returns {*}
             */
            ruby: function(attributes, text){
                var t = getTag("ruby", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Used for the benefit of browsers that don't support ruby annotations
             * @param attributes
             * @param text
             * @returns {*}
             */
            rp: function(attributes, text){
                var t = getTag("rp", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies the ruby text component of a ruby annotation.
             * @param attributes
             * @param text
             * @returns {*}
             */
            rt: function(attributes, text){
                var t = getTag("rt", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Indicates text that's no longer accurate or relevant.
             * @param attributes
             * @param text
             * @returns {*}
             */
            s: function(attributes, text){
                var t = getTag("s", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies sample computer code
             * @param attributes
             * @param text
             * @returns {*}
             */
            samp: function(attributes, text){
                var t = getTag("samp", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a script
             * @param attributes
             * @param text
             * @returns {*}
             */
            script: function(attributes, text){
                var t = getTag("script", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a section
             * @param attributes
             * @param text
             * @returns {*}
             */
            section: function(attributes, text){
                var t = getTag("section", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a selectable list
             * @param attributes
             * @param text
             * @returns {*}
             */
            select: function(attributes, text){
                var t = getTag("select", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies small text
             * @param attributes
             * @param text
             * @returns {*}
             */
            small: function(attributes, text){
                var t = getTag("small", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies media resources
             * @param attributes
             * @param text
             * @returns {*}
             */
            source: function(attributes, text){
                var t = getTag("source", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a section in a document
             * @param attributes
             * @param text
             * @returns {*}
             */
            span: function(attributes, text){
                var t = getTag("span", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies strong text
             * @param attributes
             * @param text
             * @returns {*}
             */
            strong: function(attributes, text){
                var t = getTag("strong", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a style definition
             * @param attributes
             * @param text
             * @returns {*}
             */
            style: function(attributes, text){
                var t = getTag("style", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies subscripted text
             * @param attributes
             * @param text
             * @returns {*}
             */
            sub: function(attributes, text){
                var t = getTag("sub", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a summary / caption for the <details> element
             * @param attributes
             * @param text
             * @returns {*}
             */
            summary: function(attributes, text){
                var t = getTag("summary", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies superscripted text
             * @param attributes
             * @param text
             * @returns {*}
             */
            sup: function(attributes, text){
                var t = getTag("sup", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a table
             * @param attributes
             * @param text
             * @returns {*}
             */
            table: function(attributes, text){
                var t = getTag("table", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a table body
             * @param attributes
             * @param text
             * @returns {*}
             */
            tbody: function(attributes, text){
                var t = getTag("tbody", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a table cell
             * @param attributes
             * @param text
             * @returns {*}
             */
            td: function(attributes, text){
                var t = getTag("td", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a text area
             * @param attributes
             * @param text
             * @returns {*}
             */
            textarea: function(attributes, text){
                var t = getTag("textarea", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a table footer
             * @param attributes
             * @param text
             * @returns {*}
             */
            tfoot: function(attributes, text){
                var t = getTag("tfoot", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a table header
             * @param attributes
             * @param text
             * @returns {*}
             */
            th: function(attributes, text){
                var t = getTag("th", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a table header
             * @param attributes
             * @param text
             * @returns {*}
             */
            thead: function(attributes, text){
                var t = getTag("thead", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a date/time
             * @param attributes
             * @param text
             * @returns {*}
             */
            time: function(attributes, text){
                var t = getTag("time", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies the document title
             * @param attributes
             * @param text
             * @returns {*}
             */
            title: function(attributes, text){
                var t = getTag("title", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a table row
             * @param attributes
             * @param text
             * @returns {*}
             */
            tr: function(attributes, text){
                var t = getTag("tr", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a text track for media such as video and audio
             * @param attributes
             * @param text
             * @returns {*}
             */
            track: function(attributes, text){
                var t = getTag("track", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies text with a non-textual annotation.
             * @param attributes
             * @param text
             * @returns {*}
             */
            u: function(attributes, text){
                var t = getTag("u", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies an unordered list
             * @param attributes
             * @param text
             * @returns {*}
             */
            ul: function(attributes, text){
                var t = getTag("ul", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a video
             * @param attributes
             * @param text
             * @returns {*}
             */
            video: function(attributes, text){
                var t = getTag("video", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            /**
             * Specifies a line break opportunity for very long words and strings of text with no spaces.
             * @param attributes
             * @param text
             * @returns {*}
             */
            wbr: function(attributes, text){
                var t = getTag("wbr", attributes, text);
                this.currentTag.append(t.element || t);
                return this;
            },
            text: function(txt){
                this.currentTag.text(txt);
                return this;
            },
            content: function(txt){
                this.currentTag.html(txt);
                return this;
            },
            render: function(cb){
                var txt = "";
                _.each(this.siblings, function(v, i){
                    $(container).append(v.element);
                });
                cb($(container).html());
                return this;
            },
            html: function(){
                var result = "";
                this.render(function(h){
                    result = h;
                });

                return result;
            },
            renderIn: function(el){
                $(el).append(this.html());
            }
        }
    }
})(jQuery, _);
