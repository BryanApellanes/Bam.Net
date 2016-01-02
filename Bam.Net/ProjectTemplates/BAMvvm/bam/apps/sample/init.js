
$(document).ready(function () {
    bam.app("sample", "#eDATBillingContent").setPageTransitionFilter("current", "next", function (tx, d) {
        // tx is the transitionHandler which looks like this
        // {
        //      name: <string>,
        //      from: <string>, // the name of the page the transition is from
        //      to: <string>, // the name of the page the transition is to
        //      play: function(data), // plays the transition passing in optional data
        //      also triggers start and end events before and after play
        // }
        // analyze the data d to determine if the transition will be allowed or
        // directly analyze the state of the dom.
        // return false to stop the transition from current to next page
    })
    .pageActivated("contentPage", function (page) {
        page.setStateTransitionFilter("other", "monkey", function (tx, d) {
            $("#errors").text("put state monkey");
            return "error"; // return a different state or boolean indicating whether to allow the transition
        });
        page.setStateHelloEffect("error", "shake");
    })
    .pageActivated("instructions", function (page) {
        bam.app("sample").view("sample.test", { Title: "Dust Step", Details: "These are the details" }, ".dustTarget");
    })
    .run("start");
});

//$(document).ready(function () {
//    bam.app("eDATBilling", "#eDATBillingContent").run("start");
//});

