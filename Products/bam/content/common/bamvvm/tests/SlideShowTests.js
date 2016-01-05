/*
	Copyright © Bryan Apellanes 2015  
*/
$(function () {
    //QUnit.test("")

    bam.activatePlugins();

});

function addSlideTest(){
    var e = document.createElement("div");
    $(e).html($("<h2 />").text("Added :)"));
    $("#interactiveTest").data("slideshow").addSlide(e);
}

function replaceSlides() {
    var slides = [];
    for (var i = 0; i < 5; i++) {
        slides.push($(document.createElement("div")).html(_.format("<h2>Slide {0}</h2>", i)));
    }
    $("#interactiveTest").data("slideshow").setSlides(slides);
}

function setLoopTest() {
    $("#interactiveTest").data("slideshow").setLoop(true);
}

function addSlideTestLoop(){
    addSlideTest();
    $("#interactiveTest").data("slideshow").setLoop(true);
}
function gotoSlide2(){
    $("#interactiveTest").data("slideshow").slideTo(1);
}

function beforeSlideFn(ss){
    $("#beforeMessages").text("slide index before slide: " + ss.currentSlideIndex);
}

function afterSlideFn(ss){
    $("#afterMessages").text("slide index after slide: " + ss.currentSlideIndex);
}