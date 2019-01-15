/*
	Copyright © Bryan Apellanes 2015  
*/
(function ($, _) {
    "use strict";

    function percentarc(canvas, options) {
        var config = {
                text: "",
                value: 25,
                valueFont: '30px Calibri',
                textFont: '15px Calibri',
                radius: 75,
                x: canvas.width / 2,
                y: canvas.height / 2,
                lineWidth: 15,
                backgroundStrokeStyle: "#dedbe7",
                strokeStyle: 'black',
                fillStyle: 'black',
                percentageOffset: 0,
                textOffset: 12,
                configure: function (conf) {
                    // set additional attributes on the context
                    // like lineWidth and strokeStyle before rendering
                }
            },
            context = canvas.getContext('2d'),
            startAngle = 1.5 * Math.PI;

        if (!_.isUndefined(options)) {
            $.extend(config, options);
        }        
        
        context.clearRect(0, 0, canvas.width, canvas.height);
        context.lineWidth = config.lineWidth;
        context.fillStyle = config.fillStyle;

        config.configure(config);

        // draws the background circle
        context.beginPath();
        context.strokeStyle = config.backgroundStrokeStyle;
        context.arc(config.x, config.y, config.radius, 0, 2 * Math.PI, false);
        context.stroke();
        // end -- draws the background circle

        context.strokeStyle = config.strokeStyle;
        context.fillStyle = config.fillStyle;

        var percentage = config.value,
            degrees = (percentage / 100) * 360,
            radians = degrees * (Math.PI / 180),
            endAngle = radians + startAngle;
        
        context.beginPath();
        context.arc(config.x, config.y, config.radius, startAngle, endAngle, false);
        context.stroke();
       
        context.textAlign = 'center';
        context.font = config.valueFont;
        context.fillText(config.value + "%", config.x, config.y + config.percentageOffset);
        context.font = config.textFont;
        context.fillText(config.text, config.x, config.y + config.textOffset);
    }

    $.fn.percentarc = function (opts) {
        return $(this).each(function () {
            var the = $(this)[0];
            percentarc(the, opts);
        });
    }
})(jQuery, _)