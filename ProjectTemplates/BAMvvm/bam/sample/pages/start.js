$(document).ready(function () {
    $("#effectTypes").on("change", function () {
        var ef = $(this).val();
        bam.app.helloEffect(ef);
        bam.app.goodByeEffect(ef);
    });
});

(function ($, _, b, d) {
    "use strict";

    

    return {

    }
})(jQuery, _, bam, dao);

