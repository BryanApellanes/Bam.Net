using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Dynamic;

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
            rootInsert.Insert(Dao.TableName(rootType), GetAssignValues(rootType, instance, rootInsert.ColumnNameFormatter, BaseTypePropertyPredicate).ToArray());
            rootInsert.Id();
            rootInsert.Go();
            results.Add(rootInsert);
            inheritance.Chain.BackwardsEach(typeTable =>
            {
                Type tableType = typeTable.Type;
                if (tableType != rootType)
                {
                    SqlStringBuilder inheritor = db.GetService<SqlStringBuilder>();
                    inheritor.FormatInsert<InsertInheritanceFormat>(Dao.TableName(tableType), GetAssignValues(tableType, instance, inheritor.ColumnNameFormatter, InheritingTypePropertyPredicate).ToArray());
                    inheritor.Go();
                    results.Add(inheritor);
                }
            });
            return results; 
        }
        public List<SqlStringBuilder> GetUpdateStatements(object instance)
        {
            return GetUpdateStatements(instance, instance.GetType());
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
            long id = Meta.GetId(instance);
            SqlStringBuilder rootUpdate = db.GetService<SqlStringBuilder>();
            Func<string, string> columNameFormatter = rootUpdate.ColumnNameFormatter;
            AssignValue idAssignment = new AssignValue("Id", id, rootUpdate.ColumnNameFormatter);
            TypeInheritanceDescriptor inheritance = new TypeInheritanceDescriptor(type);
            Type rootType = inheritance.RootType;
            rootUpdate.Update(Dao.TableName(rootType), GetAssignValues(rootType, instance, columNameFormatter, BaseTypePropertyPredicate).ToArray()).Where(idAssignment);
            results.Add(rootUpdate);            
            inheritance.Chain.BackwardsEach(typeTable =>
            {
                Type tableType = typeTable.Type;
                if(tableType != rootType)
                {
                    SqlStringBuilder inheritor = db.GetService<SqlStringBuilder>();
                    inheritor.Update(Dao.TableName(tableType), GetAssignValues(tableType, instance, columNameFormatter, InheritingTypePropertyPredicate).ToArray()).Where(idAssignment);
                    inheritor.Go();
                    results.Add(inheritor);
                }
            });
            return results;
        }

        protected Func<PropertyInfo, bool> BaseTypePropertyPredicate { get; set; }
        protected Func<PropertyInfo, bool> InheritingTypePropertyPredicate { get; set; }
        private IEnumerable<AssignValue> GetAssignValues(Type type, object instance, Func<string, string> columnNameFormatter, Func<PropertyInfo, bool> propertyPredicate = null)
        {
            propertyPredicate = propertyPredicate ?? BaseTypePropertyPredicate;
            return instance.EachDataProperty(type, propertyPredicate, (pi, v) => new AssignValue(pi.Name, v, columnNameFormatter));
        }        
    }
}
