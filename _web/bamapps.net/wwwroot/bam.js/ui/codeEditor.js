/*
	Copyright © Bryan Apellanes 2015  
*/
/**
 *
 * Copyright 2013, Bryan Apellanes
 * Available via the MIT or new BSD license.
 *
 * Simple jQuery plugin wrapper for CodeMirror.
 * Used with dataset.js will allow declaratively
 * setting code editors on a page.
 *
 */

var codeEditor = (function($, _, b, d, w){

    $.fn.codeEditor = function(opts){
        return $(this).each(function () {
            var $the = $(this),
                codeMirror = null;

            if($the.is("textarea")){
                codeMirror = CodeMirror.fromTextArea($the[0]);
            }else{
                codeMirror = CodeMirror($the[0], opts);
            }

            $the.data("CodeMirror", codeMirror).data("codeEditor", codeMirror); // in case I forget what name I stuck it under
        });
    }

})(jQuery, _, bam, dao, window || {});