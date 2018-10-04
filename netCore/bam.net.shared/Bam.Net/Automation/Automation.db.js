var database = {
    nameSpace: "Bam.Net.Automation.Data",
    schemaName: "Automation",
    xrefs: [       
    ],
    tables: [
        {
            name: "JobData",
            cols: [
                { Name: "String", Null: false },
                { Description: "String", Null: true }
            ]
        },
        {
            name: "DeferredJobData",
            cols: [
                { Name: "String", Null: false },
                { JobDirectory: "String", Null: false },
                { HostName: "String", Null: false },
                { LastStepNumber: "Long", Null: false}
            ]
        },
        {
            name: "JobRunData",
            fks: [
               { JobDataId: "JobData" }
            ],
            cols: [
                { Success: "Boolean", Null: false },
                { Message: "String" }                
            ]
        }
        //,
        //{
        //    name: "GitLogData",
        //    cols: [
        //        { CommitHash: "String", Null: false },
        //        { AbbreviatedCommitHash: "String", Null: false },
        //        { TreeHash: "String" },
        //        { AbbreviatedTreeHash: "String" },
        //        { ParentHashes: "String" },
        //        { AbbreviatedParentHashes: "String" },
        //        { AuthorName: "String" },
        //        { AuthorEmail: "String" },
        //        { AuthorDate: "String" },
        //        { AuthorDateRelative: "String" },
        //        { CommitterName: "String" },
        //        { CommitterEmail: "String" },
        //        { CommitterDate: "String" },
        //        { CommitterDateRelative: "String" },
        //        { Subject: "Subject" }
        //    ]
        //}
    ]
};
