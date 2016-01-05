// This file was taken verbatim from 
// https://raw.github.com/thomaslee/jquery.detach-and-replace-with/master/jquery.detach-and-replace-with.js
//
// $("#foo").detachAndReplaceWith($stuff);
//
// Pretty much a copy/paste of replaceWith() from the jQuery trunk
// that uses detach() instead of remove().
//
(function ($) {
    $.fn.detachAndReplaceWith = function _jquery_detachAndReplaceWith(value) {
        if (this[0] && this[0].parentNode && this[0].parentNode.nodeType != 11) {
            //
            // Make sure that the elements are removed from the DOM before they are inserted
            // this can help fix replacing a parent with child elements
            //
            if (jQuery.isFunction(value)) {
                return this.each(function (i) {
                    var self = jQuery(this), old = self.html();
                    self.replaceWith(value.call(this, i, old));
                });
            }

            if (typeof value !== "string") {
                value = jQuery(value).detach();
            }

            return this.each(function () {
                var next = this.nextSibling,
                    parent = this.parentNode;

                //
                // Call detach() instead of remove() to ensure we don't break
                // event handlers etc.
                //
                jQuery(this).detach();

                if (next) {
                    jQuery(next).before(value);
                } else {
                    jQuery(parent).append(value);
                }
            });
        }

        return this.length ?
            this.pushStack(jQuery(jQuery.isFunction(value) ? value() : value), "detachAndReplaceWith", value) :
            this;
    };
})(jQuery);