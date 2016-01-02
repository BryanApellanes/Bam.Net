var imageStream = (function () {
    var iq = qi("Analytics"),
        currentSlideSet = [],
        nextSlideSet = [],
        highId = 2147483647;

    function createSlides(images) {
        var slides = [];
        _.each(images, function (v, i) {
            var slide = document.createElement("div");
            bam.app.view("streamImage", v, slide);

            slides.push(slide);
        });
        return slides;
    }

    function showError(msg){
        $("#errors").text(msg).show(bam.helloEffect());
    }

    function getNextSet(count, complete){
        _.act("Images", "GetSome", {count: count, fromId: highId})
            .done(function(d){
                if(d.Success){
                    highId = d.Data[d.Data.length - 1].Id;
                    complete(createSlides(d.Data));
                }else{
                    showError(r);
                }
            })
            .fail(function(r){
                showError(r);
            })
    }

    function setCurrent(){
        var $ss = $("#imageSlides");

        $ss.data("slideShow").setSlides(currentSlideSet);
        $("img", $ss).show();
        bam.activatePlugins();
        $(".loading").hide();
    }

    function start(){
        getNextSet(5, function(slides){
            currentSlideSet = slides;
            setCurrent();
        })
    }
    function afterSlide(ss){
        // after slide
        //      if slideindex == 2 set the next set of 5 slides
        //      if slideindex == 4 (the end) set current slide set to next slide set and get new next
        if(ss.currentSlideIndex == 2){
            getNextSet(4, function(slides){
                nextSlideSet = slides;
            })
        }else if(ss.currentSlideIndex == 4){
            nextSlideSet.push(currentSlideSet[currentSlideSet.length - 1]);
            currentSlideSet = nextSlideSet;
            setCurrent();
            getNextSet(4, function(slides){
                nextSlideSet = slides;
            })
        }
    }

    function score(sc, ev) {
        var itId = $(ev.currentTarget).parent().attr("data-image-id");
        _.act("Images", "RateImage", { imageId: itId, rating: sc }, function (r) {

        })
        $("#imageSlides").data("slideShow").nextSlide();
    }

    /**
     * Used by sdo.getItem to extract the Url from the img tags
     * @param el
     * @returns {*|jQuery}
     */
    function getUrl(el){
        return $(el).attr("src");
    }

    function setUrl(el, v){
        $(el).attr("src", v);
    }

    return {
        start: start,
        afterSlide: afterSlide,
        getUrl: getUrl,
        setUrl: setUrl,
        score: score
    }
})();