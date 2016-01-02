var database = {
    nameSpace: "Bam.Net.Logging.Data",
    schemaName: "DaoLogger",
    xrefs: [
       
    ],
    tables: [
        {
            name: "LogEvent",
            cols: [
                { Source: "String" },
                { Category: "String" },
                { EventId: "Int" },
                { User: "String" },
                { Time: "DateTime" },
                { MessageSignature: "String" },
                { MessageVariableValues: "String" },
                { Message: "String" },
                { Computer: "String" },
                { Severity: "String" }
            ]
        }
    ]
};