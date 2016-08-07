using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public class TypeInheritanceWriter
    {
        public TypeInheritanceWriter()
        {
            TransactionFormat = @"BEGIN TRANSACTION
{0}
COMMIT";
        }

        public string TransactionFormat { get; set; }
        public Database Database { get; set; }
        public string GetInsertStatement(object instance)
        {
            return GetInsertStatement(instance, Database);
        }
        public SqlStringBuilder GetInsertStatement(object instance, Database db)
        {
            SqlStringBuilder result = db.GetService<SqlStringBuilder>();            
            Type type = instance.GetType();
            TypeInheritanceDescriptor inheritance = new TypeInheritanceDescriptor(type);
            Type rootType = inheritance.RootType;
            result.Insert(Dao.TableName(rootType), GetAssignValues(rootType, instance, result.ColumnNameFormatter).ToArray());
            result.Id();
            result.Go();
            inheritance.Chain.BackwardsEach(typeTable =>
            {
                if(typeTable.Type != rootType)
                {
                    List<AssignValue> assignValues = GetAssignValues(typeTable.Type, instance, result.ColumnNameFormatter).ToList();
                    assignValues.Insert(0, new AssignValue("Id", "@ID"));
                    result.Insert(Dao.TableName(typeTable.Type), assignValues.ToArray());
                }
            });
            return result;
        }

        private IEnumerable<AssignValue> GetAssignValues(Type type, object instance, Func<string, string> columnNameFormatter)
        {
            foreach (PropertyInfo pi in type.GetProperties().Where(pi => pi.DeclaringType == type))
            {
                yield return new AssignValue(pi.Name, pi.GetValue(instance), columnNameFormatter);
            };
        }
    }
}
