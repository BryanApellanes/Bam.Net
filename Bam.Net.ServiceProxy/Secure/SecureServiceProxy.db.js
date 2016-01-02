var database = {
    nameSpace: "Bam.Net.ServiceProxy.Secure",
    schemaName: "SecureServiceProxy",
    
    tables: [
        {
            name: "Application",
            cols: [
                { Name: "String", Null: false }
            ]
        },
        {
            name: "Configuration",
            fks: [{ ApplicationId: "Application" }],
            cols: [
                { Name: "String", Null: false }
            ]
        },
        {
            name: "ConfigSetting",
            fks: [{ ConfigurationId: "Configuration" }],
            cols: [
                { Key: "String", Null: false },
                { Value: "String", Null: false }
            ]
        },
        {
            name: "ApiKey",
            fks: [
                { ApplicationId: "Application" }
            ],
            cols: [
                { ClientId: "String", Null: false },
                { SharedSecret: "String", Null: false },
                { CreatedBy: "String", Null: false },
                { CreatedAt: "DateTime", Null: false},
                { Confirmed: "DateTime" },
                { Disabled: "Boolean", Null: false },
                { DisabledBy: "String" }
            ]
        },
        {
            name: "SecureSession",
            fks: [
                { ApplicationId: "Application" }
            ],
            cols: [
                { Identifier: "String", Null: false },
                { AsymmetricKey: "String", Null: false },
                { SymmetricKey: "String", Null: false },
                { SymmetricIV: "String", Null: false },
                { CreationDate: "DateTime", Null: false},
                { TimeOffset: "Int", Null: false},
                { LastActivity: "DateTime" },
                { IsActive: "Boolean" }
            ]
        }
    ]
};
