/*
	Copyright © Bryan Apellanes 2015  
*/
(function ($) {
    var orientations = {
        topLeft: function (moving, reference, offset, collision) {
            $(moving).position({
                of: reference,
                my: "right top",
                at: "left bottom",
                offset: offset,
                collision: collision
            });
        },
        topCenter: function (moving, reference, offset, collision) {
            $(moving).position({
                of: reference,
                my: "left top",
                at: "left bottom",
                offset: offset,
                collision: collision
            });
        },
        topRight: function (moving, reference, offset, collision) {
            $(moving).position({
                of: reference,
                my: "left top",
                at: "right top",
                offset: offset,
                collision: collision
            });
        },
        rightCenter: function (moving, reference, offset, collision) {
            $(moving).position({
                of: reference,
                my: "left",
                at: "right",
                offset: offset,
                collision: collision
            });
        },
        bottomRight: function (moving, reference, offset, collision) {
            $(moving).position({
                of: reference,
                my: "left bottom",
                at: "right top",
                offset: offset,
                collision: collision
            });
        },
        bottomCenter: function (moving, reference, offset, collision) {
            $(moving).position({
                of: reference,
                my: "left top",
                at: "left bottom",
                offset: offset,
                collision: collision
            });
        },
        bottomLeft: function (moving, reference, offset, collision) {
            $(moving).position({
                of: reference,
                my: "right bottom",
                at: "left top",
                offset: offset,
                collision: collision
            });
        },
        leftCenter: function (moving, reference, offset, collision) {
            $(moving).position({
                of: reference,
                my: "right",
                at: "left",
                offset: offset,
                collision: collision
            });
        },
        centerScreen: function (moving, reference, overlap) {
            var height = $(moving).height(),
                width = $(moving).width(),
                top = ((document.body.clientHeight / 2) - (height / 2)) + $(document.body).scrollTop(),
                left = (document.body.clientWidth / 2) - (width / 2);

            top = top <= 0 ? 0 : top;
            left = left <= 0 ? 0 : left;
            $(moving).css("position", "absolute").css("top", top).css("left", left).css("z-index", 9999);
        },
        center: function (moving, reference, overlap) {

        }
    };

    _.mixin({
        orient: function (opts) {
            var config = $.extend({ moving: null, reference: null, orientation: "topCenter", offset: 0, collision: null }, opts),
                $moving = $(config.moving);

            orientations[config.orientation](config.moving, config.reference, config.offset, config.collision);
        }
    });
})(jQuery)
