var database = {
    nameSpace: "Bam.Net.Analytics.EnglishDictionary",
    schemaName: "EnglishDictionary",
    tables: [
        {
            name: "Word",
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "Definition",
            fks: [
                { WordId: "Word" }
            ],
            cols: [
                { WordType: "String", Null: true },
                { Value: "String", Null: false }
            ]
        }
    ]
}