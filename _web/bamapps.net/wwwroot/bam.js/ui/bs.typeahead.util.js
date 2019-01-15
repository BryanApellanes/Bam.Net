/*
	Copyright © Bryan Apellanes 2015  
*/
(function($, _, b){
    b.typeaheadItem = function(o){
        o.toLowerCase = function(){
            return this.toString().toLowerCase();
        };
        return o;
    }

})(jQuery, _, bam);