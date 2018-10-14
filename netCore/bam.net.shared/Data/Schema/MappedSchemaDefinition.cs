/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Data.Schema
{
    public class MappedSchemaDefinition
    {
        public MappedSchemaDefinition() : this(".\\{0}.lzs.json"._Format(typeof(MappedSchemaDefinition).Name)) { }
        public MappedSchemaDefinition(string filePath)
        {
            this.SchemaNameMap = new SchemaNameMap();
            this.SchemaDefinition = new SchemaDefinition();
            this.FilePath = filePath;
        }

        public MappedSchemaDefinition(SchemaDefinition definition, SchemaNameMap nameMap)
            : this()
        {
            this.SchemaNameMap = nameMap;
            this.SchemaDefinition = definition;
        }

        public string FilePath { get; set; }
        public SchemaNameMap SchemaNameMap { get; set; }
        public SchemaDefinition SchemaDefinition { get; set; }

        public static MappedSchemaDefinition Load(string filePath)
        {
            return Load(new FileInfo(filePath));
        }
        public static MappedSchemaDefinition Load(FileInfo file)
        {
            MappedSchemaDefinition def = file.FromJsonFile<MappedSchemaDefinition>();
            def.FilePath = file.FullName;
            return def;
        }

        public void Save()
        {
            Save(this.FilePath);
        }

        public void Save(string filePath)
        {
            Save(new FileInfo(filePath));
        }

        public SchemaDefinition MapSchemaClassAndPropertyNames()
        {
            return MapSchemaClassAndPropertyNames(SchemaNameMap, SchemaDefinition);
        }

        public void Save(FileInfo file)
        {
            SchemaDefinition = MapSchemaClassAndPropertyNames();
            this.FilePath = file.FullName;
            this.ToJsonFile(file);
        }

        public static SchemaDefinition MapSchemaClassAndPropertyNames(SchemaNameMap nameMap, SchemaDefinition schema)
        {
            SchemaManager mgr = new SchemaManager(schema);
            mgr.AutoSave = false;
            Parallel.ForEach(nameMap.TableNamesToClassNames, (map) =>
            {
                mgr.SetTableClassName(map.TableName, map.ClassName);
            });
            Parallel.ForEach(schema.Tables, (table) =>
            {
                Parallel.ForEach(table.Columns, column => mgr.SetColumnPropertyName(table.Name, column.Name, nameMap.GetPropertyName(table.Name, column.Name)));
            });            
            return mgr.CurrentSchema;
        }
    }
}
