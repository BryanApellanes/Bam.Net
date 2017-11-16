# Performing Dao CRUD Operations (Create, Retrieve, Update, Delete)

LaoTze and LaoTzu both generate .Net (C#) code that can be used to quickly perform all 
database CRUD (Create, Retrieve, Update, Delete) operations.

```c#
// Create
TableOne one = new TableOne();
one.Name = "TableOneName";
one.Description = "TableOne Description";
one.Save();

// Retrieve
TableOne retrieved = TableOne.OneWhere(c => c.Name == "TableOneName");
// or
TableOneCollection retrieved = TableOne.Where(c => Name == "TableOneName");

// Update
retrieved.Description = "The description updated";
retrieved.Save();

// Delete
retrieved.Delete();
```