var database = {
    nameSpace: "Bam.Net.Analytics",
    schemaName: "Analytics",
    xrefs: [
        ["Url", "Tag"],
        ["Image", "Tag"]
    ],
    tables: [
        // -- classification tables
        {
            name: "Category",
            cols: [
                { Value: "String", Null: false },
                { DocumentCount: "Long", Null: false }
            ]
        },
        {
            name: "Feature",
            fks: [
                { CategoryId: "Category" }
            ],
            cols: [
                { Value: "String", Null: false },
                { FeatureToCategoryCount: "Long", Null: false }
            ]
        },
        // -- end classification tables
        // -- crawlers tables
        {
            name: "Crawler",
            cols: [
                { Name: "String", Null: false},
                { RootUrl: "String", Null: false}
            ]
        },
        {
            name: "Protocol",
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "Domain",
            fks: [],
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "Port",
            cols: [
                { Value: "Int", Null: false }
            ]
        },
        {
            name: "Protocol",
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "Path",
            cols: [
                { Value: "String" }
            ]
        },
        {
            name: "QueryString",
            cols: [
                { Value: "String" }
            ]
        },
        {
            name: "Fragment",
            cols: [
                { Value: "String" }
            ]
        },
        {
            name: "Url",
            fks: [
                { ProtocolId: "Protocol" },
                { DomainId: "Domain" },
                { PortId: "Port" },
                { PathId: "Path" },
                { QueryStringId: "QueryString" },
                { FragmentId: "Fragment" }
            ]
        },
        {
            name: "Tag",
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "Image",
            cols: [
                { Date: "DateTime", Null: false }
            ],
            fks: [
                { UrlId: "Url" },
                { CrawlerId: "Crawler" }
            ]
        },
        // --  end crawlers tables 
        // -- metrics tables
        {
            name: "Timer",
            cols: [
                { Value: "String", Null: false }
            ]
        },
        {
            name: "MethodTimer",
            fks: [
                { TimerId: "Timer" }
            ],
            cols: [
                { MethodName: "String", Null: false }
            ]
        },
        {
            name: "LoadTimer",
            fks: [
                { TimerId: "Timer" }
            ],
            cols: [
                { UrlId: "Int" } // maps to crawler.url
            ]
        },
        {
            name: "CustomTimer",
            fks: [
                { TimerId: "Timer" }
            ],
            cols: [
                { Name: "String", Null: false },
                { Description: "String" }
            ]
        },
        {
            name: "Counter",
            cols: [
                { Value: "Int", Null: false }
            ]
        },
        {
            name: "MethodCounter",
            fks: [
                { CounterId: "Counter" }
            ],
            cols: [
                { MethodName: "String", Null: false }
            ]
        },
        {
            name: "LoadCounter",
            fks: [
                { CounterId: "Counter" }
            ],
            cols: [
                { UrlUuid: "String", Null: false } // maps to crawlers.url
            ]
        },
        {
            name: "UserIdentifier",
            cols: [
                { Value: "String", Null: false },
                { Name: "String", Null: false }
            ]
        },
        {
            name: "ClickCounter",
            fks: [
                { CounterId: "Counter" },
                { UserIdentifierId: "UserIdentifier" }
            ],
            cols: [
                { UrlId: "Int", Null: false } // maps to crawlers.url
            ]
        },
        {
            name: "LoginCounter",
            fks: [
                { CounterId: "Counter" },
                { UserIdentifierId: "UserIdentifier" }
            ]
        }
        // -- end metrics tables
    ]
}
