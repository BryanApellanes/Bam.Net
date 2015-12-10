/*
	Copyright © Bryan Apellanes 2015  
*/
var database = {
    nameSpace: "Bam.Net.DaoRef",
    schemaName: "DaoRef",
    xrefs: [
        ["Left", "Right"]
    ],
    tables: [
        {
            name: "Left",
            cols: [
                { Name: "String", Null: false }
            ]
        },
        {
            name: "Right",
            cols: [
                { Name: "String", Null: false }
            ]
        },
        {
            name: "TestTable",
            cols: [
                { Name: "String", Null: false },
                { Description: "String" }
            ]
        },
        {
            name: "TestFkTable",
            fks: [
                { TestTableId: "TestTable" }
            ],
            cols: [
                { Name: "String", Null: false }
            ]
        },
        {
            name: "DaoReferenceObject",
            cols: [
                { IntProperty: "Int" },
                { DecimalProperty: "Decimal" },
                { LongProperty: "Long" },
                { DateTimeProperty: "DateTime" },
                { BoolProperty: "Boolean" },
                { ByteArrayProperty: "ByteArray" },
                { StringProperty: "String" }
            ]
        },
        {
            name: "DaoReferenceObjectWithForeignKey",
            fks:[
                { DaoReferenceObjectId: "DaoReferenceObject" }
            ],
            cols: [
                { Name: "String", Null: false}
            ]
        }
    ]
}
