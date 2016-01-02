var database = {
    nameSpace: "Bam.Net.Data.Tests",
    schemaName: "Shop",
    xrefs: [
        ["List", "Item"]
    ],
    tables: [
        {
            name: "Cart",
            cols: [
            ]
        },
        {
            name: "CartItem",
            fks:[
                { CartId: "Cart" },
                { ItemId: "Item" }
            ],
            cols: [
                { Quantity: "Int", Null: false }
            ]
        },
        {
            name: "List",
            cols: [
                { Name: "String", Null: false }
            ]
        },
        {
            name: "Item",
            fks: [
            ],
            cols: [
                { Name: "String", Null: false }
            ]
        }
    ]
};
