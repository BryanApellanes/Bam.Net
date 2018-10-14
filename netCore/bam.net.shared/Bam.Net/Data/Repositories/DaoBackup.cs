/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;
using System.Reflection;
using System.Collections;
using System.Data;

namespace Bam.Net.Data.Repositories
{
	/// <summary>
	/// Represents a backup of a Dao database schema and the data therein
	/// </summary>
	public partial class DaoBackup
	{
		public DaoBackup(Assembly daoAssembly, Database databaseToBackup, IRepository backupRepository)			
		{
			this.DatabaseToBackup = databaseToBackup;
			this.DatabaseToRestoreTo = new SQLiteDatabase(DefaultDataDirectoryProvider.Current.AppDataDirectory, "{0}_Restore"._Format(databaseToBackup.ConnectionName));
			this.BackupRepository = backupRepository;
			this.BackupRepository.AddTypes(Dto.GetTypesFromDaos(daoAssembly));
			this.DaoAssembly = daoAssembly;
			this.DaoTypes = daoAssembly.GetTypes().Where(t => t.HasCustomAttributeOfType<TableAttribute>()).ToArray();
			Dao.AfterCommitAny += SaveToRepository;
		}

		/// <summary>
		/// The Assembly given to the constructor which "should" contain Dao types.
		/// </summary>
		public Assembly DaoAssembly { get; private set; }

		/// <summary>
		/// All the Dao types that were found in the Assembly given to the constructor
		/// </summary>
		public IEnumerable<Type> DaoTypes { get; private set; }

		/// <summary>
		/// The database to backup
		/// </summary>
		public Database DatabaseToBackup { get; private set; }

		/// <summary>
		/// The repository where the database will be backed up to.
		/// </summary>
		public IRepository BackupRepository { get; private set; }

		/// <summary>
		/// The database that the backup will be restored to
		/// </summary>
		public Database DatabaseToRestoreTo { get; set; }

		public void Backup()
		{
			Backup(BackupRepository);
		}

		protected internal void Backup(IRepository backupRepository)
		{
			BackupRepository = backupRepository;
			DaoTypes.Each(t =>
			{
				// TODO: fix this so that it doesn't use LoadAll, if there are too many records we'll run out of memory
				IEnumerable daos = t.InvokeStatic<IEnumerable>("LoadAll", DatabaseToBackup);
				foreach(object o in daos)
				{
					SaveToBackupRepository((Dao)o);
				}
			});
		}
		
		public void Restore()
		{
			Restore(DatabaseToRestoreTo);
		}

		object restoreLock = new object();
		public HashSet<OldToNewIdMapping> Restore(Database restoreTo, ILogger logger = null)
		{
			lock (restoreLock)
			{
				HashSet<OldToNewIdMapping> result = RestoreData(restoreTo, logger);
				CorrectForeignKeys(result, DatabaseToBackup, restoreTo);

				return result;
			}
		}
		
		protected void SetTemporaryForeignKeys(Dao dao, Dictionary<string, Dao> temp)
		{
			List<PropertyInfo> foreignKeyProperties = dao.GetType().GetProperties().Where(p => p.HasCustomAttributeOfType<ForeignKeyAttribute>()).ToList();
			foreignKeyProperties.Each(prop =>
			{
				ForeignKeyAttribute fk = prop.GetCustomAttribute<ForeignKeyAttribute>(); 
				dao.Property(prop.Name, temp[fk.ReferencedTable].IdValue);
			});
		}

		protected internal void CorrectForeignKeys(HashSet<OldToNewIdMapping> oldToNewIdMappings, Database source, Database destination)
		{
			Dictionary<string, OldToNewIdMapping> byUuid = oldToNewIdMappings.ToDictionary(otn => otn.Uuid);
			
			IParameterBuilder oldParameterBuilder = source.GetService<IParameterBuilder>();
			SqlStringBuilder committer = destination.GetService<SqlStringBuilder>();
			List<Type> tableTypes = DaoAssembly.GetTypes().Where(type => type.HasCustomAttributeOfType<TableAttribute>()).ToList();
			List<PropertyInfo> foreignKeyProperties = new List<PropertyInfo>();
			foreach(Type type in tableTypes)
			{
				foreignKeyProperties.AddRange(type.GetProperties().Where(prop => prop.HasCustomAttributeOfType<ForeignKeyAttribute>()).ToList());
			}				
			
			// for every foreign key
			foreach (PropertyInfo prop in foreignKeyProperties)
			{
				ForeignKeyAttribute fk = prop.GetCustomAttributeOfType<ForeignKeyAttribute>();
				// get the old Dao instances that represents the referenced table					
				Type referencedDaoType = DaoTypes.First(t=>
				{
                    t.HasCustomAttributeOfType(out TableAttribute table);
                    return table.TableName.Equals(fk.ReferencedTable);
				});

				SqlStringBuilder oldSelector = source.GetService<SqlStringBuilder>();
				oldSelector.Select(fk.ReferencedTable);
				List<object> oldReferencedDaos = source.GetDataTable(oldSelector, CommandType.Text, oldParameterBuilder.GetParameters(oldSelector)).ToListOf(referencedDaoType);

				foreach (object oldReferenced in oldReferencedDaos)
				{
					Dao oldReferencedDao = (Dao)oldReferenced;

					// get the old Dao instances that represent the referencing table where the referencing column value = the old table id
					Type referencingDaoType = DaoTypes.First(t =>
					{
                        t.HasCustomAttributeOfType(out TableAttribute table);
                        return table.TableName.Equals(fk.Table);
					});
					SqlStringBuilder oldReferencerSelector = source.GetService<SqlStringBuilder>();
					oldReferencerSelector.Select(fk.Table).Where(new AssignValue(fk.Name, oldReferencedDao.IdValue));
					List<object> oldReferencingDaoInstances = source.GetDataTable(oldReferencerSelector, CommandType.Text, oldParameterBuilder.GetParameters(oldReferencerSelector)).ToListOf(referencingDaoType);

					if (oldReferencingDaoInstances.Count > 0)
					{
						List<string> oldReferencingDaoInstanceUuids = oldReferencingDaoInstances.Select(o => o.Property<string>("Uuid")).ToList();

						ulong oldReferencedId = oldReferencedDao.IdValue.Value;
						// get the new referenced id
						ulong whatItShouldBeNow = byUuid[oldReferencedDao.Property<string>("Uuid")].NewId;

						// update the new referencing column to match the newId in the destination where the uuids in oldDaoInstances		
						committer.Update(fk.Table, new AssignValue(fk.Name, whatItShouldBeNow)).Where(Query.Where("Uuid").In(oldReferencingDaoInstanceUuids.ToArray()));
						committer.Go();
					}
				}				
			}			
			
			destination.ExecuteSql(committer, oldParameterBuilder);
		}
		
		protected internal Dictionary<string, Dao> InsertTempForeignKeyTargets()
		{
			dynamic topContext = new { DaoTypes, Results = new Dictionary<string, Dao>() };
			foreach (Type dao in DaoTypes)
			{
				dao.GetProperties().Where(prop => prop.HasCustomAttributeOfType<ForeignKeyAttribute>()).Each(prop =>
				{
					ForeignKeyAttribute fk = prop.GetCustomAttribute<ForeignKeyAttribute>();
					foreach(Type daoType in DaoTypes)
					{
						if (daoType.Name.Equals(fk.ReferencedTable))
						{
							Dao instance = daoType.Construct<Dao>();
							foreach (PropertyInfo p in daoType.GetProperties())
							{
								if (p.PropertyType == typeof(string))
								{
									p.SetValue(instance, "Temp");
								}
								else if (p.PropertyType == typeof(byte[]) || p.PropertyType == typeof(byte?[]))
								{
									p.SetValue(instance, new byte[] { });
								}
								else if (p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?))
								{
									p.SetValue(instance, DateTime.UtcNow);
								}
							}
							instance.Save(DatabaseToRestoreTo);
							topContext.Results.Add(fk.ReferencedTable, instance);
						}
					}
				});
			}

			return topContext.Results;
		}

		protected void SaveToRepository(Database database, Dao instance)
		{
			if(database == DatabaseToBackup)
			{
				object dtoInstanc = Dto.Copy(instance);
				SaveDtoToBackupRepository(dtoInstanc);
			}
		}

		private void ForceUpdateIfExistsInTarget(string uuid, Dao dao)
		{
			SqlStringBuilder sql = DatabaseToRestoreTo.ServiceProvider.Get<SqlStringBuilder>();
			IParameterBuilder parameterBuilder = DatabaseToRestoreTo.ServiceProvider.Get<IParameterBuilder>();
			sql.Select(dao.TableName()).Where(Query.Where("Uuid") == uuid);
			DataTable table = DatabaseToRestoreTo.GetDataTable(sql.ToString(), CommandType.Text, parameterBuilder.GetParameters(sql));

			if (table.Rows.Count == 1)
			{
				dao.DataRow = table.Rows[0];
				dao.ForceUpdate = true;
			}
			else if (table.Rows.Count > 1)
			{
				Args.Throw<InvalidOperationException>("More than one instance of the specified Uuid exists in the target database: {0}", uuid);
			}
		}
		private void SaveToBackupRepository(Dao dao)
		{
			object dtoInstance = Dto.Copy(dao);
			SaveDtoToBackupRepository(dtoInstance);
		}

		private void SaveDtoToBackupRepository(object dtoInstance)
		{
			object existing = BackupRepository.Retrieve(dtoInstance.GetType(), dtoInstance.Property<string>("Uuid"));
			if (existing != null)
			{
				BackupRepository.Save(dtoInstance);
			}
			else
			{
				BackupRepository.Create(dtoInstance);
			}
		}

		private HashSet<OldToNewIdMapping> RestoreData(Database restoreTo, ILogger logger)
		{
			HashSet<OldToNewIdMapping> result = new HashSet<OldToNewIdMapping>();
			DatabaseToRestoreTo = restoreTo;
			DatabaseToRestoreTo.TryEnsureSchema(DaoTypes.First(), logger);

			Dictionary<string, Dao> tempForeignKeyTargets = InsertTempForeignKeyTargets();
			// for all the poco types load them all from the repo
			foreach (Type pocoType in BackupRepository.StorableTypes)
			{
				// copy the poco as a dao and save it into the restoreTo
				IEnumerable<object> all = BackupRepository.RetrieveAll(pocoType);
				Type daoType = DaoTypes.FirstOrDefault(t => t.Name.Equals(pocoType.Name));
				Args.ThrowIf<InvalidOperationException>(daoType == null, "The Dto of type {0} didn't have a corresponding Dao type", pocoType.Name);

				foreach (object poco in all)
				{
					string uuid = Meta.GetUuid(poco, true);
					Dao dao = (Dao)poco.CopyAs(daoType);
					dao.IdValue = null;
					dao.DataRow = null;
					dao.ForceInsert = true;
					dao.UniqueFilterProvider = (d) =>
					{
						return Query.Where("Uuid") == uuid;
					};
					ForceUpdateIfExistsInTarget(uuid, dao);
					SetTemporaryForeignKeys(dao, tempForeignKeyTargets);

					dao.Save(DatabaseToRestoreTo);
					OldToNewIdMapping idMapping = new OldToNewIdMapping
					{
						PocoType = pocoType,
						DaoType = daoType,
						OldId = (ulong)poco.Property("Id"),
						NewId = (ulong)dao.IdValue,
						Uuid = uuid
					};

					result.Add(idMapping);
				}
			}
			return result;
		}
	}
}
