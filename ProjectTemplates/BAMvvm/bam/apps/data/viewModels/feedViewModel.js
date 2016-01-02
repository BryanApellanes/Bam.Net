/**
 * @constructor
 */
function testItem(){
    var date = new Date();
    this.month = date.getMonth();
    this.date = date.getDate();
    this.year = date.getFullYear();
    this.hour = date.getHours();
    this.minute = date.getMinutes();
    this.second = date.getSeconds();
    this.millisecond = date.getMilliseconds();
    this.stickerizerImageSource = "https://graph.facebook.com/520054713/picture";
    this.stickerizerName = "Test User";
    this.stickerizeeImageSource = "https://graph.facebook.com/1565661999/picture";
    this.stickerizeeName = "Test child";
    this.stickerSource = "http://hatagi.co/pic/683";
    this.stickerizable = "being awesome";
}

function feedViewModel(el) {
    this.view = {
        items: [],
        init: function () {
            var the = this;
            return $.Deferred(function(){
                var prom = this;
                _.times(3, function(i){
                    the.items.push(new testItem());
                });
                prom.resolve();
            }).promise();
        }
    }
}


//    {#items}
//    <div>
//      <div data-plugin="friendlyDate" 
//    data-mo="{month}" 
//    data-dd="{day}" 
//    data-yyyy="{year}" 
//    data-hh="{hour}" 
//    data-mm="{minute}"
//    data-ss="{second}"
//    data-ms="{millisecond}"
//    >    
//</div>
//<div>
// <img src="{stickerizerImageSource}" /> 
// {stickerizerName} 
//    gave 
//    <img src="{stikcerizeeImageSource}" />
//    {stickerizeeName} a
//    <img src="{stickerSource}" />
//    for {stickerizable}
//</div>
//</div>
//{/items}