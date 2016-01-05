/*
	Copyright © Bryan Apellanes 2015  
*/

$(document).ready(function(){
    "use strict";

    //bam.app("localhost").run("home");
    var accordionData = new templates.accordionModel();
    var collapsibleData = new templates.collapsibleModel("This is a collapsible");
    var dropdownData = new templates.dropdownModel("Show Drop Down");
    var buttongroupData = new templates.buttongroupModel();
    var datepickerData = new templates.datepickerModel();

    using.dustTemplates("localhost", function(){

        dropdownData.addItem("option one");
        dropdownData.addItem("option two");
        dropdownData.addItem("this one should alert", function(ev){
            alert("it worked yay");
        });
        dropdownData.renderIn("#renderDropDownTest");

        accordionData.addSection("section one", "this is stuff to put there");
        accordionData.addSection("section two", "this is the second set of stuff");
        accordionData.renderIn("#renderAccordionTest").then(function(h){
            accordionData.getSection(1).html(div().content("monkey").html());
        });

        collapsibleData.renderIn("#renderCollapsibleTest").then(function(h){
            collapsibleData.content("collapsible monkey: yay this is good");
        });

        buttongroupData.addItem("one", function(ev){
            alert("one");
        });
        buttongroupData.addItem("two", function(ev){
            alert("two");
        });
        buttongroupData.addItem("three", function(ev){
            alert("three");
        });
        buttongroupData.renderIn("#renderButtonGroupTest");

        datepickerData.changeDate(function(ev){
            datepickerData.hide();
            alert(ev.date);
        });
        datepickerData.renderIn("#renderDatePickerTest");
    });
});
