using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Schema;
using System.Data;
using Bam.Net.Logging;
using System.Data.Common;
using System.Collections.Concurrent;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// An extension to the DaoRepository that supports saving
    /// types that extend other types in a schema that reflects
    /// the inheritance chain
    /// </summary>
    public class DatabaseRepository : DaoRepository
    {
        public DatabaseRepository(ITypeTableNameProvider tableNameProvider = null, Func<SchemaDefinition, TypeSchema, string> schemaTempPathProvider = null)
            :base(tableNameProvider, schemaTempPathProvider)
        {
            BlockOnChildWrites = true;
            BackgroundThreadQueue = new BackgroundThreadQueue<SqlStringBuilder> { Process = Execute };
        }

        public DatabaseRepository(Database database, ILogger logger = null, ITypeTableNameProvider tableNameProvider = null, Func<SchemaDefinition, TypeSchema, string> schemaTempPathProvider = null) : this(tableNameProvider, schemaTempPathProvider)
        {
            Database = database;
            TypeSchemaGenerator = new TypeInheritanceSchemaGenerator(tableNameProvider, schemaTempPathProvider);
            TypeDaoGenerator = new TypeDaoGenerator(TypeSchemaGenerator);
            Logger = logger ?? Log.Default;
            TypeDaoGenerator.Subscribe(Logger);
            TypeSchemaGenerator.Subscribe(Logger);
        }
        
        TypeSchema _typeSchema;
        public TypeSchema TypeSchema
        {
            get
            {
                return SchemaDefinitionCreateResult.TypeSchema;
            }
        }

        TypeSchemaPropertyManager _typeSchemaPropertyManager;
        object _typeSchemaPropertyManagerLock = new object();
        protected TypeSchemaPropertyManager TypeSchemaPropertyManager
        {
            get
            {
                return _typeSchemaPropertyManagerLock.DoubleCheckLock(ref _typeSchemaPropertyManager, () => new TypeSchemaPropertyManager(TypeSchema));
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
                    _schemaDefinitionCreateResult = TypeSchemaGenerator.CreateSchemaDefinition(StorableTypes);
                }
                return _schemaDefinitionCreateResult;
            }                
        }

        TypeInheritanceSqlWriter _insertWriter;
        object _insertWriterLock = new object();
        public TypeInheritanceSqlWriter SqlWriter
        {
            get
            {
                return _insertWriterLock.DoubleCheckLock(ref _insertWriter, () => new TypeInheritanceSqlWriter(Database));
            }
        }

        bool _blockOnChildWrites;
        /// <summary>
        /// If true writing of child collections
        /// will block on saving of the parent
        /// </summary>
        public bool BlockOnChildWrites
        {
            get
            {
                return _blockOnChildWrites;
            }
            set
            {
                _blockOnChildWrites = value;
                if (_blockOnChildWrites)
                {
                    ChildWriter = Execute;
                }
                else
                {
                    ChildWriter = BackgroundThreadQueue.Enqueue;
                }
            }
        }

        public bool ValidateTypes { get; set; }

        public BackgroundThreadQueue<SqlStringBuilder> BackgroundThreadQueue
        {
            get; set;
        }

        bool _ensured;

        public event EventHandler GenerateDaoAssemblySucceeded;

        public void EnsureSchema()
        {
            if (!_ensured)
            {
                try
                {
                    _ensured = true;
                    Database.CommitSchema(SchemaDefinitionCreateResult);
                    SchemaStatus = EnsureSchemaStatus.Success;
                }
                catch (Exception ex)
                {
                    Logger.AddEntry("Error writing schema: {0}", LogEventType.Warning, ex, ex.Message);
                }
            }
        }

        public override T Create<T>(T toCreate)
        {
            return (T)Create(toCreate);
        }
        public override void Initialize()
        {
            EnsureSchema();
            base.Initialize();
        }
        public override object Create(object toCreate)
        {
            Initialize();
            Type pocoType = GetBaseType(toCreate.GetType());
            if (ValidateTypes)
            {
                ValidateType(pocoType);
            }

            // TODO: implement transaction functionality in sqlstringbuilder            
            List<SqlStringBuilder> sqls = SqlWriter.GetInsertStatements(toCreate, Database); // due to supporting some database types that don't allow for (or make it easy to do) multiline scripts or multi table inserts, each insert is returned as a separate sqlstring builder
            long id = Database.QuerySingle<long>(sqls[0]);
            toCreate.Property("Id", id);
            sqls.Rest(1, sql =>
            {
                List<DbParameter> dbParams = new List<DbParameter>();
                dbParams.Add(Database.CreateParameter("Id", id));
                dbParams.AddRange(Database.GetParameters(sql));
                Database.ExecuteSql(sql, dbParams.ToArray());
            });
            // - end TODO            
            SaveChildCollections(toCreate);
            SaveXrefs(toCreate, pocoType);

            return toCreate;
        }

        public override bool Delete<T>(T toDelete)
        {
            return Delete((object)toDelete);
        }

        public override bool Delete(object toDelete)
        {
            try
            {
                List<SqlStringBuilder> sqls = SqlWriter.GetDeleteStatements(toDelete, GetBaseType(toDelete.GetType()));
                sqls.Each(sql => Execute(sql));
                return true;
            }
            catch(Exception ex)
            {
                string value = toDelete == null ? "null" : toDelete.ToString();
                Logger.AddEntry("Exception occurred on delete of {0}: {1}", ex, value, ex.Message);
                return false;
            }
        }

        public override T Update<T>(T toUpdate)
        {
            return (T)Update(toUpdate);
        }

        public override object Update(object toUpdate)
        {
            Type pocoType = GetBaseType(toUpdate.GetType());
            List<SqlStringBuilder> sqls = SqlWriter.GetUpdateStatements(toUpdate, pocoType);
            sqls.Each(sql => Execute(sql));
            SaveXrefs(toUpdate, pocoType);
            return toUpdate;
        }

        protected void SaveChildCollections(object parent)
        {
            TypeSchemaPropertyManager.ForEachChildCollection(parent, (coll) =>
            {
                List<SqlStringBuilder> sqls = new List<SqlStringBuilder>();
                foreach(object child in coll)
                {
                    TypeSchemaPropertyManager.SetParentProperties(parent, child);
                    if(child.Property<long>("Id") > 0)
                    {
                        sqls.AddRange(SqlWriter.GetUpdateStatements(child, Database));
                    }
                    else
                    {
                        SetMeta(child);
                        sqls.AddRange(SqlWriter.GetInsertStatements(child, Database));
                    }
                }
                sqls.ForEach(sql => ChildWriter(sql));
            });
        }

        protected void Execute(SqlStringBuilder sql)
        {
            Database.ExecuteSql(sql);
        }

        protected Action<SqlStringBuilder> ChildWriter { get; set; }

        private void ValidateType(Type type)
        {
            string errorFormat = "Required property {0} is missing";
            Args.ThrowIf<InvalidOperationException>(!type.HasProperty("Id"), errorFormat, "Id");
            Args.ThrowIf<InvalidOperationException>(RequireUuid && !type.HasProperty("Uuid"), errorFormat, "Uuid");
            Args.ThrowIf<InvalidOperationException>(RequireCuid && !type.HasProperty("Cuid"), errorFormat, "Cuid");
        }

        private void SaveXrefs(object instance, Type pocoType)
        {
            Dao dao = GetDaoInstanceById(pocoType, GetIdValue(instance));
            if (SetXrefDaoCollectionValues(instance, dao))
            {
                dao.ForceUpdate = true;
                SaveDaoInstance(pocoType, dao);
            }
        }
    }
}
