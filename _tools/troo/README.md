# Troo

troo.exe is used to generate data access objects, data transfer objects and repositories from plain CLR classes.

/generateDaoAssemblyForTypes
Generate a Dao Assembly for types in a specified namespace of a specified assembly.

/generateDaoCodeForTypes
Generate Dao source code for types in a specified namespace of a specified assembly.

/generateDtosForDaos
Generate Dto source for types in a specified namespace of a specified assembly, optionally compiling and keeping the source.

/generateSchemaRepository
In addition to generating Daos and Dtos for types in a specified namespace of a specified assembly, will 
also generate a schema specific DaoRepository for all the types found.
