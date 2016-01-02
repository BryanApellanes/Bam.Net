var database = {
    nameSpace: "Bam.Net.Encryption",
    schemaName: "Encryption",
    xrefs: [
        //["List", "Item"]
    ],
    tables: [
        {
            name: "Vault",
            cols: [
                { Name: "String", Null: false }                
            ]
        },
        {
            name: "VaultItem",
            fks:[
                { VaultId: "Vault" }                
            ],
            cols: [
                { Key: "String", Null: false },
                { Value: "String", Null: false }
            ]
        },
        {
            name: "VaultKey",
            fks: [
                { VaultId: "Vault" }
            ],
            cols: [
                { RsaKey: "String", Null: false },
                { Password: "String", Null: true }
            ]
        }
    ]
};
