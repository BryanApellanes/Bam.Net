using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// A class used to write sql statements that can be used
    /// to persist CLR types to a database.  This class
    /// assumes that the tables in the database are setup 
    /// to recieve properties of object instances in their
    /// inheritance chain.  For example if a type A extends 
    /// type B then there will be tables in the database
    /// representing each.  This structure can be achieved
    /// by using <see cref="Bam.Net.Data.Repositories.TypeSchemaScriptWriter.WriteSchemaScript(Database, IEnumerable{Type})"/>
    /// </summary>
    public class TypeInheritanceSqlWriter
    {
        public TypeInheritanceSqlWriter()
        {
            BaseTypePropertyPredicate = (pi) => !pi.Name.Equals("Id");
            InheritingTypePropertyPredicate = (pi) => true;
        }

        public TypeInheritanceSqlWriter(Database db):this()
        {
            Database = db;
        }
        
        public Database Database { get; set; }
        public List<SqlStringBuilder> GetInsertStatements(object instance)
        {
            return GetInsertStatements(instance, Database);
        }

        public List<SqlStringBuilder> GetInsertStatements(object instance, Database db)
        {
            List<SqlStringBuilder> results = new List<SqlStringBuilder>();
            db = db ?? Database;
            SqlStringBuilder rootInsert = db.GetService<SqlStringBuilder>();            
            Type type = instance.GetType();
            TypeInheritanceDescriptor inheritance = new TypeInheritanceDescriptor(type);
            Type rootType = inheritance.RootType;
            rootInsert.Insert(Dao.TableName(rootType), GetBaseTypeAssignValues(rootType, instance, rootInsert.ColumnNameFormatter).ToArray());
            rootInsert.Id();
            rootInsert.Go();
            results.Add(rootInsert);
            inheritance.Chain.BackwardsEach(typeTable =>
            {
                Type tableType = typeTable.Type;
                if (tableType != rootType)
                {
                    SqlStringBuilder inheritor = db.GetService<SqlStringBuilder>();
                    inheritor.Insert(Dao.TableName(tableType), GetInheritingTypeAssignValues(tableType, instance, inheritor.ColumnNameFormatter).ToArray());                    
                    inheritor.Go();
                    results.Add(inheritor);
                }
            });
            return results; 
        }
 
        public List<SqlStringBuilder> GetUpdateStatements(object instance, Type type)
        {
            return GetUpdateStatements(instance, type, Database);
        }
        public List<SqlStringBuilder> GetUpdateStatements(object instance, Database db)
        {
            return GetUpdateStatements(instance, instance.GetType(), db);
        }

        public List<SqlStringBuilder> GetUpdateStatements(object instance, Type type, Database db)
        {
            List<SqlStringBuilder> results = new List<SqlStringBuilder>();
            db = db ?? Database;
            SqlStringBuilder rootUpdate = db.GetService<SqlStringBuilder>();
            Func<string, string> columnNameFormatter = rootUpdate.ColumnNameFormatter;
            AssignValue uniqueness = GetUniquenessFilter(instance, columnNameFormatter);
            TypeInheritanceDescriptor inheritance = new TypeInheritanceDescriptor(type);
            Type rootType = inheritance.RootType;
            rootUpdate.Update(Dao.TableName(rootType), GetBaseTypeAssignValues(rootType, instance, columnNameFormatter).ToArray()).Where(uniqueness);
            results.Add(rootUpdate);            
            inheritance.Chain.BackwardsEach(typeTable =>
            {
                Type tableType = typeTable.Type;
                if(tableType != rootType)
                {
                    SqlStringBuilder inheritor = db.GetService<SqlStringBuilder>();
                    inheritor.Update(Dao.TableName(tableType), GetInheritingTypeAssignValues(tableType, instance, columnNameFormatter).ToArray()).Where(uniqueness);
                    inheritor.Go();
                    results.Add(inheritor);
                }
            });
            return results;
        }

        public List<SqlStringBuilder> GetDeleteStatements(object instance, Type type)
        {
            return GetDeleteStatements(instance, type, Database);
        }

        public List<SqlStringBuilder> GetDeleteStatements(object instance, Type type, Database db)
        {
            List<SqlStringBuilder> results = new List<SqlStringBuilder>();
            db = db ?? Database;
            SqlStringBuilder rootDelete = db.GetService<SqlStringBuilder>();
            Func<string, string> columnNameFormatter = rootDelete.ColumnNameFormatter;
            AssignValue uniqueness = GetUniquenessFilter(instance, columnNameFormatter);
            TypeInheritanceDescriptor inheritance = new TypeInheritanceDescriptor(type);
            Type rootType = inheritance.RootType;
            rootDelete.Delete(Dao.TableName(rootType)).Where(uniqueness);
            results.Add(rootDelete);
            inheritance.Chain.BackwardsEach(typeTable =>
            {
                Type tableType = typeTable.Type;
                if(tableType != rootType)
                {
                    SqlStringBuilder inheritor = db.GetService<SqlStringBuilder>();
                    inheritor.Delete(Dao.TableName(tableType)).Where(uniqueness);
                    inheritor.Go();
                    results.Add(inheritor);
                }
            });
            return results;
        }

        public Func<PropertyInfo, bool> BaseTypePropertyPredicate { get; set; }
        public Func<PropertyInfo, bool> InheritingTypePropertyPredicate { get; set; }
        private IEnumerable<AssignValue> GetInheritingTypeAssignValues(Type type, object instance, Func<string, string> columnNameFormatter)
        {
            foreach(AssignValue valueAssignment in GetAssignValues(type, instance, columnNameFormatter, InheritingTypePropertyPredicate))
            {
                yield return valueAssignment;
            }
            foreach (AssignValue valueAssignment in GetUuidAndCuidAssignValues(instance, columnNameFormatter))
            {
                yield return valueAssignment;
            }
        }

        private IEnumerable<AssignValue> GetBaseTypeAssignValues(Type type, object instance, Func<string, string> columnNameFormatter)
        {
            foreach(AssignValue valueAssignment in GetAssignValues(type, instance, columnNameFormatter, BaseTypePropertyPredicate))
            {
                yield return valueAssignment;
            }
            foreach(AssignValue valueAssignment in GetUuidAndCuidAssignValues(instance, columnNameFormatter))
            {
                yield return valueAssignment;
            }
        }

        private IEnumerable<AssignValue> GetUuidAndCuidAssignValues(object instance, Func<string, string> columnNameFormatter)
        {
            object uuid = instance.Property("Uuid", false);
            if (uuid != null)
            {
                yield return new AssignValue("Uuid", uuid, columnNameFormatter);
            }
            object cuid = instance.Property("Cuid", false);
            if (cuid != null)
            {
                yield return new AssignValue("Cuid", cuid, columnNameFormatter);
            }
        }

        private IEnumerable<AssignValue> GetAssignValues(Type type, object instance, Func<string, string> columnNameFormatter, Func<PropertyInfo, bool> propertyPredicate = null)
        {
            propertyPredicate = propertyPredicate ?? BaseTypePropertyPredicate;
            return instance.EachDataProperty(type, propertyPredicate, (pi, v) => new AssignValue(pi.Name, v, columnNameFormatter));
        }

        private AssignValue GetUniquenessFilter(object instance, Func<string, string> columnNameFormatter)
        {
            Args.ThrowIfNull(instance, nameof(instance));
            object identifier = Meta.GetId(instance);
            if (identifier.Equals(default(long)))
            {
                Args.Throw<InvalidOperationException>("Unable to get unique identifier for specified data: {0}", instance.ToString());
            }
            return new AssignValue("Id", identifier, columnNameFormatter);
        }
    }
}
