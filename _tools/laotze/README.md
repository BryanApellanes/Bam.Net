# Data Access Objects (Dao) From LaoTze
Generating Data Access Objects using LaoTze.exe is done by creating a *.db.js 
file and processing that file with the command line tool LaoTze.exe.  A *.db.js 
file is a special JavaScript file that contains a single JavaScript literal object 
named “database” that defines a database schema.  An example database schema object
is shown below.

```javascript
var database = {
	nameSpace: “The.Namespace.That.Generated.Objects.Will.Be.Placed.In”,
	schemaName: “UsedAsTheConnectionStringNameInTheConfig”,
	xrefs: [ // An array of arrays; 
		// each entry defines a many to many relationship between the table 					
		// names specified
		[“LeftTable”, “RightTable”]
	],
	tables: [
		{
			name: “TheNameOfTheTable”,
			fks: [ 	// An array of foreign key definitions where the key is 
				// the column name and the value is the name of the table 
				// that the foreign key references
				{ ColumnName1: “ReferencedTable1” },
				{ ColumnName2: “ReferencedTable2” }
			],
			cols: [ 	// An array of column definitions
				{ ColumnName: “DataType”, Null: false || true } // 
			]
		},
		{
			name: “TableOne”,
			cols: [ 	
				{ Name: “String”, Null: false },
				{ Description: "String", Null: true }
			]
		},
		{
			name: "TableTwo",
			fks: [
				{ TableOneId: "TableOne" }
			],
			cols: [
				{ Name: "String", Null: false },
				{ DescriptionTwo: "String", Null: true }
			]
		},
		{
			name: "LeftTable",
			cols: [
				{ LeftName: "String"}
			]
		},
		{
			name: "RightTable",
			cols: [
				{ RightName: "String"}
			]
		}
		{		
			// … another table like above and so on
		}		
	]
}
```

To generate C# code from a *.db.js file use the following command:

`laotze.exe /root:[PATH-TO-DIRECTORY-CONTAINING-DBJS] /keep`

### What's next?
- [Performing Dao CRUD Operations (Create, Retrieve, Update, Delete)](../../DaoRef/) using generated code
