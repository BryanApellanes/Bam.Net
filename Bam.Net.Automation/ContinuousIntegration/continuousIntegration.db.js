var database = {
    nameSpace: "Bam.Net.Automation.ContinuousIntegration.Data",
    schemaName: "ContinuousIntegration",
    xrefs: [       
    ],
    tables: [
        {
            name: "BuildJob",
            cols: [
                { Name: "String", Null: false },
                { UserName: "String", Null: false },
                { HostName: "String", Null: false },
                { OutputPath: "String", Null: false }
            ]
        },
        {
            name: "BuildResult",
            fks: [
               { BuildJobId: "BuildJob" }
            ],
            cols: [
                { Success: "Boolean", Null: false },
                { Message: "String" }                
            ]
        }
    ]
};
