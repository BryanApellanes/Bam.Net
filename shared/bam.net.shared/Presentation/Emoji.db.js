var database = {
    nameSpace: "Bam.Net.Presentation.Unicode",
    schemaName: "Emojis",
    xrefs: [],
    tables: [
        {
            name: "Category",
            cols: [
                { Name: "String", Null: false }
            ]
        },
        {
            name: "Emoji",
            fks: [
                { CategoryId: "Category" }
            ],
            cols: [
                { Number: "Int", Null: false },
                { Character: "String", Null: false },
                { Apple: "String", Null: false },
                { Google: "String", Null: false },
                { Twitter: "String", Null: false },
                { One: "String", Null: false },
                { Facebook: "String", Null: false },
                { Samsung: "String", Null: false },
                { Windows: "String", Null: false },
                { GMail: "String", Null: false },
                { SoftBank: "String", Null: false },
                { DoCoMo: "String", Null: false },
                { KDDI: "String", Null: false },
                { ShortName: "String", Null: false },
            ]
        },
        {
            name: "Code",
            fks: [
                { EmojiId: "Emoji" }
            ],
            cols: [
                { Value: "String", Null: false }
            ]
        }
    ]
}