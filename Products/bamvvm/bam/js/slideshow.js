(function ($) {
    function setSlideShow(el, options) {
        if ($(el).data("slideshow") == null || undefined) {
            var ss = new slideShow(el, options);
            $(el).data("slideshow", ss).data("slideShow", ss);
        }
    }

    function slideShow(el, options) {
        var target = $(el),
            id = $(target).attr("id"),
            itemContainer = document.createElement("div"),
            slides = [],
            config =
            {
                itemtag: "div",
                viewableCount: 1,
                width: 500,
                height: 400,
                loop: true,
                showEffect: "fade",
                beforeSlide: function (slideNum) { },
                afterSlide: function (slideNum) { },
                nextButton: null,
                previousButton: null
            },
            the = this;

        $.extend(config, options);
        config.viewableCount = Number(config.viewableCount);
        config.itemWidth = config.width / config.viewableCount;

        $(target).width(config.width).height(config.height).css("overflow", "hidden").css("position", "relative");

        this.element = target;
        this.itemContainer = itemContainer;
        this.currentSlideIndex = 0;
        this.initialized = false;
        this.beforeSlide = config.beforeSlide;
        this.afterSlide = config.afterSlide;

        this.addSlide = function(el){
            $(el).css("float", "left").width(config.itemWidth).height(config.height);
            slides.push(el);
            $(el).detach();
            if(the.initialized){
                setSlideStrips();
                the.setSlide(the.currentSlideIndex);
            }
        };

        this.setSlides = function(sa){
            slides = [];
            the.initialized = false;
            _.each(sa, function(el){
                the.addSlide(el);
            });

            setSlideStrips();
            the.firstPosition();
            the.initialized = true;
        };

        this.getCurrentSlide = function(){
            return the.getSlide(the.currentSlideIndex);
        };

        this.getSlide = function(index){
            return $(itemContainer).children()[index];
        };

        this.slideCount = function(){
            return $(itemContainer).children().length;
        };
        /**
         * Get the number of pixels to set the container's
         * left attribute to in order to correctly focus the
         * current slide
         * @type {Function}
         */
        this.getSlideLeft = function (slideIndex) {
            if (slideIndex < 0) {
                throw { message: "SlideIndex out of range" }
            }
            if (slideIndex > $(the.itemContainer).children().length - 1) {
                throw { message: "SlideIndex out of range" };
            }

            var left = 0,
                slides = $(the.itemContainer).children();

            for (var i = 0; i < slideIndex; i++) {
                left += $(slides[i]).outerWidth(true);
            }

            return -left;
        };

        this.setSlide = function (slideIndex) {
            if(_.isUndefined(slideIndex)){
                slideIndex = the.currentSlideIndex;
            }
            // the first slide is a copy of the last for 'looping' effect
            if (config.loop == true && slideIndex == 0) {
                slideIndex = $(the.itemContainer).children().length - (config.viewableCount * 2);
            }

            // the last slide is a copy of the first for 'looping' effect
            if (config.loop == true && slideIndex == $(the.itemContainer).children().length - (config.viewableCount)) {
                slideIndex = config.viewableCount;
            }
            the.currentSlideIndex = slideIndex;
            $(the.itemContainer).css("left", the.getSlideLeft(slideIndex))
        };

        this.firstPosition = function () {
            the.currentSlideIndex = 0;
            if (config.loop == true) {
                the.currentSlideIndex = config.viewableCount;
            }
            the.setSlide(the.currentSlideIndex);
        };

        function getMoveBy(from, to) {
            if (from < 0 || from > $(the.itemContainer).children().length - 1) {
                throw "from index out of range";
            }

            if (to < 0 || to > $(the.itemContainer).children().length - 1) {
                throw "to index out of range";
            }

            var low = from > to ? to : from,
                high = from > to ? from : to,
                dir = to > from ? "-=" : "+=",
                // mainly for debugging, a 'friendly' way of determining the direction of the slide
                fr = dir == "-=" ? "up" : "down", // yes minus (-) is "up"
                moveByWidth = 0,
                children = $(the.itemContainer).children();

            for (var i = low; i < high; i++) {
                moveByWidth += $(children[i]).outerWidth();
            }

            return { width: moveByWidth, direction: dir, friendly: fr };
        }

        this.slideTo = function (slideIndex) {
            if (slideIndex > $(the.itemContainer).children().length - 1) {
                slideIndex = $(the.itemContainer).children().length - 1;
            } else if (slideIndex < 0) {
                slideIndex = 0;
            }

            //config.beforeSlide(the);
            the.beforeSlide(the);
            var moveBy = getMoveBy(the.currentSlideIndex, slideIndex);
            $(the.itemContainer).animate({ left: moveBy.direction + moveBy.width + "px" }, "swing", function () {
                the.setSlide(slideIndex);
                //config.afterSlide(the);
                the.afterSlide(the);
            });
        };

        function setSlideStrips() {
            var frontClones = [],
                backClones = [],
                stripWidth = 0,
                loopStripWidth = 0,
                strip = document.createElement("div"),
                loopStrip = document.createElement("div");

            // create the front and back clones for loop effect
            for (var i = 0; i < config.viewableCount; i++) {
                var frontClone = $(slides[i]).clone(),
                    backClone = $(slides[slides.length - (config.viewableCount - i)]).clone();

                frontClones.push(frontClone);
                backClones.push(backClone);
            }
            // -- end create front and back clones

            // -- add the backClones to the front of the strip for
            // looping
            _.each(backClones, function (v) {
                $(loopStrip).append(v);
                loopStripWidth += $(v).outerWidth();
            });
            // -- end backClones

            // -- add the actual slides to the loopStrip and the normal
            for (var i = 0; i < slides.length; i++) {
                var slide = slides[i];
                if (!_.isUndefined(slide.parentNode) && !_.isNull(slide.parentNode)) {
                    slide.parentNode.removeChild(slide);
                }
                loopStripWidth += $(slide).outerWidth();
                $(loopStrip).append($(slide).clone());
                $(strip).append($(slide).clone());
                stripWidth += $(slide).outerWidth();
            }
            // -- end actual

            // -- add the front clones to the back for looping
            _.each(frontClones, function (v) {
                $(loopStrip).append(v);
                loopStripWidth += $(v).outerWidth();
            });
            // -- end frontClones

            $(loopStrip).width(loopStripWidth);
            $(strip).width(stripWidth);

            $(the.itemContainer).remove();

            if (config.loop) {
                the.itemContainer = loopStrip;
            } else {
                the.itemContainer = strip;
            }

            $(the.itemContainer).css("position", "absolute");
            $(target).append(the.itemContainer).show(config.showEffect);
        }

        this.init = function(){ // bool
            $(target).children().each(function (i, el) {
                the.addSlide(el);
            });

            setSlideStrips();
            the.firstPosition();
            the.initialized = true;
        };

        this.setLoop = function(){
            the.loop = true;
            config.loop = true;
            setSlideStrips();
            the.setSlide(Number(the.currentSlideIndex) + Number(config.viewableCount));
        };

        this.nextSlide = function () {
            var next = Number(the.currentSlideIndex) + 1;
            the.slideTo(next);
        };

        this.previousSlide = function () {
            var prev = Number(the.currentSlideIndex) - 1;
            the.slideTo(prev);
        };

        this.init();
        $(config.nextButton).click(this.nextSlide);
        $(config.previousButton).click(this.previousSlide);
    }

    $.fn.slideshow = function (options) {
        var config =
        {
            itemtag: "div",
            width: 500,
            height: 400,
            loop: true,
            beforeSlide: function (slideNum) { },
            afterSlide: function (slideNum) { },
            nextButton: null,
            previousButton: null
        };

        return this.each(function () {
            if (options) {
                $.extend(config, options);
            }

            var the = $(this);
            setSlideShow(the, options);
        });
    };

    $.fn.slideShow = $.fn.slideshow;
})(jQuery);
