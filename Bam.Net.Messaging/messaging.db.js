var database = {
    nameSpace: "Bam.Net.Messaging.Data",
    schemaName: "Messaging",
    xrefs: [
    ],
    tables: [
        {
            name: "EmailMessage",
            fks: [
                { DirectMessageId: "DirectMessage" }
            ],
            cols: [
                { Sent: "Boolean" },
                { TemplateName: "String", Null: false },
                { TemplateJsonData: "String", Null: false}
            ]
        },
        {
            name: "DirectMessage",
            fks: [
                { MessageId: "Message" }
            ],
            cols: [
                { To: "String", Null: false },
                { ToEmail: "String", Null: false }
            ]
        },
        {
            name: "Message",
            cols: [
                { CreatedDate: "DateTime", Null: false },
                { From: "String", Null: false },
                { FromEmail: "String", Null: false },
                { Subject: "String" },
                { Body: "String", Null: false }
            ]
        }
    ]
}
