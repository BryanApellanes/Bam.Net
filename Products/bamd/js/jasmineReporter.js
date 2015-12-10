
/**
 * @constructor
 */
function jasmineReporter() { }

jasmineReporter.prototype = {
    reportRunnerStarting: function(runner) {

        $(this).trigger("reportRunnerStarting", runner);
        //this.tellPhantom({"type":"init", "payload": runner.specs().length});
    },
    reportSpecResults: function(spec) {
        // ... collect the spec results

        // spec.didFail
        // spec.suite.description
        // spec.description
        $(this).trigger("reportSpecResults", spec);
    },
    reportSuiteResults: function(suite){
        $(this).trigger("resportSuiteResults", suite);
    },
    reportRunnerResults: function(runner) {
        $(this).trigger("reportRunnerResults", runner);
    }
};