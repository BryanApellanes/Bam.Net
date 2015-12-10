var database = {
    nameSpace: "Bam.Net.Instructions",
    schemaName: "Instructions",
    xrefs: [        
    ],
    tables:[
        {
            name: "InstructionSet",
            cols: [
                { Name: "String", Null: false },
                { Description: "String" },
                { Author: "String" }
            ]
        },
        {
            name: "Section",
            fks: [
                { InstructionSetId: "InstructionSet" }
            ],
            cols: [
                { Title: "String", Null: false },
                { Description: "String" }
            ]
        },
        {
            name: "Step",
            fks: [
                { SectionId: "Section" }
            ],
            cols: [
                { Number: "Int", Null: false },
                { Description: "String" },
                { Detail: "String", Null: false }
            ]
        }
    ]
}