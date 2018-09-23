var database = {
    nameSpace: "Bam.Net.Translation",
    schemaName: "Translation",
    xrefs: [
      
    ],
    tables: [
        {
            name: "Language",
            cols: [
                { EnglishName: "String", Null: false },
                { FrenchName: "String", Null: false },
                { GermanName: "String", Null: false },
                { ISO639_2: "String", Null: false },
                { ISO639_1: "String", Null: false}
            ]
        },
        {
            name: "Text",
            fks: [
                { LanguageId: "Language" }
            ],
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "LanguageDetection",
            fks: [
                { LanguageId: "Language" },
                { TextId: "Text" }
            ],
            cols: [
                { Detector: "String", Null: false },
                { ResponseData: "String", Null: true }
            ]
        },
        {
            name: "Translation",
            fks: [
                { TextId: "Text" },
                { LanguageId: "Language" }
            ],
            cols: [
                { Translator: "String", Null: false },
                { Value: "String", Null: false }
            ]
        },
        {
            name: "OtherName",
            fks:[
                { LanguageId: "Language" }
            ],
            cols: [
                { LanguageName: "String", Null: false },
                { Value: "String", Null: false }
            ]
        }
    ]
};
