using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Schema;
using System.Data;
using Bam.Net.Logging;

namespace Bam.Net.Data.Repositories
{
    public class DatabaseRepository : Repository
    {
        TypeInheritanceSchemaGenerator _typeSchemaGenerator;
        public DatabaseRepository(Database database)
        {
            _typeSchemaGenerator = new TypeInheritanceSchemaGenerator();
        }
        
        public Database Database
        {
            get; set;
        }
        TypeSchema _typeSchema;
        public TypeSchema TypeSchema
        {
            get
            {
                return SchemaDefinitionCreateResult.TypeSchema;
            }
        }

        SchemaDefinition _schema;
        public SchemaDefinition SchemaDefinition
        {
            get
            {
                return SchemaDefinitionCreateResult.SchemaDefinition;
            }
        }

        SchemaDefinitionCreateResult _schemaDefinitionCreateResult;
        protected SchemaDefinitionCreateResult SchemaDefinitionCreateResult
        {
            get
            {
                if(_schemaDefinitionCreateResult == null)
                {
                    Args.ThrowIf<InvalidOperationException>(!StorableTypes.Any(), "No Types were specified, call AddType for each type to store");
                    _schemaDefinitionCreateResult = _typeSchemaGenerator.CreateSchemaDefinition(StorableTypes);
                }
                return _schemaDefinitionCreateResult;
            }
                
        }
        bool _ensured;
        public void EnsureSchema()
        {
            if (!_ensured)
            {
                try
                {
                    _ensured = true;
                    StringBuilder sql = Database.WriteSchemaScript(SchemaDefinitionCreateResult);
                    Database.ExecuteSql(sql.ToString(), CommandType.Text);
                }
                catch (Exception ex)
                {
                    Logger.AddEntry("Error writing schema: {0}", LogEventType.Warning, ex, ex.Message);
                }
            }
        }

        public override object Create(object toCreate)
        {
            throw new NotImplementedException();
        }

        public override T Create<T>(T toCreate)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(object toDelete)
        {
            throw new NotImplementedException();
        }

        public override bool Delete<T>(T toDelete)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> Query(dynamic query)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable Query(Type type, Dictionary<string, object> queryParameters)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> Query(Type type, Func<object, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable Query(Type type, QueryFilter query)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> Query(string propertyName, object propertyValue)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> Query<T>(Dictionary<string, object> queryParameters)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> Query<T>(QueryFilter query)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> Query<T>(dynamic query)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> Query<T>(Func<T, bool> query)
        {
            throw new NotImplementedException();
        }

        public override object Retrieve(Type objectType, string uuid)
        {
            throw new NotImplementedException();
        }

        public override object Retrieve(Type objectType, long id)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(string uuid)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(long id)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> RetrieveAll(Type type)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override object Update(object toUpdate)
        {
            throw new NotImplementedException();
        }

        public override T Update<T>(T toUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
