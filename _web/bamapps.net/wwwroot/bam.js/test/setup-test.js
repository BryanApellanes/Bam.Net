var expect = require("expect");

describe("A basic test", function(){
    it("should run", function(){
        console.log("it ran!");
    })
});

describe("Async test", function(){
    it("should also run", function(done){
        return new Promise((resolve, reject) => {
            console.log("async yay!");
            expect(true).toBe(true);
            resolve();
            done();
        })
    })
    it("is just me", function(){
        console.log("just me ran");
    })
});