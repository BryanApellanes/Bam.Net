var database = {
    nameSpace: "BAMvvm.Data",
    contextName: "BAMvvm",
    tables: [
        {
            name: "Example",
            fks: [],
            Name: "String"            
        },
        {
            name: "Item",        
            fks: ["Example"],
            Name: "String"
        }
    ]
}