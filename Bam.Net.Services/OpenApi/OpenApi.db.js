var database = {
    nameSpace: "Bam.Net.Services.OpenApi",
    schemaName: "OpenApi",
    tables: [
        {
            name: "ObjectDescriptor",
            cols: [
                { Name: "String", Null: false },
                { Description: "String", Null: true }
            ]
        },
        {
            name: "FixedField",
            fks: [
                { ObjectDescriptorId: "ObjectDescriptor" }
            ],
            cols: [
                { FieldName: "String", Null: false },
                { Type: "String", Null: false },
                { AppliesTo: "String", Null: true},
                { Description: "String", Null: false}
            ]
        },
        {
            name: "PatternedField",
            fks: [
                { ObjectDescriptorId: "ObjectDescriptor" }
            ],
            cols: [
                { FieldPattern: "String", Null: false },
                { Type: "String", Null: false },
                { AppliesTo: "String", Null: true },
                { Description: "String", Null: false }
            ]
        }
    ]
}