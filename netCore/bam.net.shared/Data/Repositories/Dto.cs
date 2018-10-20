/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Linq;
using System.Reflection;
using System.IO;

namespace Bam.Net.Data.Repositories
{
	/// <summary>
	/// A Data Transfer Object.  Represents the properties
	/// of Dao types without the associated methods.  
	/// </summary>
	public partial class Dto
	{
        /// <summary>
        /// Get the associated Dto types for the 
        /// Dao types in the specified daoAssembly.
        /// </summary>
        /// <param name="daoAssembly"></param>
        /// <returns></returns>
        public static Type[] GetTypesFromDaos(Assembly daoAssembly)
        {
            GeneratedAssemblyInfo assemblyInfo = GetGeneratedDtoAssemblyInfo(daoAssembly);

            return assemblyInfo.GetAssembly().GetTypes();
        }

        /// <summary>
        /// Get a generated Dto type for the specified Dao instance.
        /// The Dto type will only have properties that match the columns
        /// of the Dao.
        /// </summary>
        /// <param name="daoInstance"></param>
        /// <returns></returns>
        public static Type TypeFor(Dao daoInstance)
        {
            return TypeFor(daoInstance.GetType());
        }

        /// <summary>
        /// Get the associated Dto type for the specified
        /// daoType
        /// </summary>
        /// <param name="daoType"></param>
        /// <returns></returns>
        public static Type TypeFor(Type daoType)
        {
            return GetTypesFromDaos(daoType.Assembly).Where(t => t.Name.Equals(daoType.Name)).FirstOrDefault();
        }

        /// <summary>
        /// Copy the specified Dao instance as an equivalent Dto instance
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static object Copy(Dao instance)
        {
            return instance.CopyAs(TypeFor(instance));
        }

        static object _genLock = new object();
        /// <summary>
        /// Generates an assembly containing Dto's that represent all the 
        /// Dao's found in the sepecified daoAssembly.  A Dto or (DTO) is
        /// a Data Transfer Object and represents only the properties of 
        /// a Dao.  A Dao or (DAO) is a Data Access Object that represents
        /// both properties and methods to create, retrieve, update and delete. 
        /// </summary>
        /// <param name="daoAssembly"></param>
        /// <returns></returns>
        public static GeneratedAssemblyInfo GetGeneratedDtoAssemblyInfo(Assembly daoAssembly, string fileName = null)
        {
            lock (_genLock)
            {
                DaoToDtoGenerator generator;
                string defaultFileName = GetDefaultFileName(daoAssembly, out generator);
                fileName = fileName ?? defaultFileName;
                GeneratedAssemblyInfo assemblyInfo = GeneratedAssemblyInfo.GetGeneratedAssembly(fileName, generator);
                return assemblyInfo;
            }
        }

        public static string GetDefaultFileName(Assembly daoAssembly)
        {
            DaoToDtoGenerator ignore;
            return GetDefaultFileName(daoAssembly, out ignore);
        }

        public static string GetDefaultFileName(Assembly daoAssembly, out DaoToDtoGenerator generator)
        {
            generator = new DaoToDtoGenerator(daoAssembly);
            string fileName = generator.GetDefaultFileName();
            return fileName;
        }

        public static void WriteRenderedDto(string nameSpace, string writeSourceTo, Type daoType, Func<PropertyInfo, bool> propertyFilter)
        {
            string typeName = Dao.TableName(daoType);
            typeName = string.IsNullOrEmpty(typeName) ? daoType.Name : typeName;
            DtoModel dtoModel = new DtoModel(nameSpace, typeName, daoType.GetProperties().Where(propertyFilter).Select(pi=> new DtoPropertyModel(pi)).ToArray());
            WriteRenderedDto(writeSourceTo, dtoModel);
        }

        public static void WriteRenderedDto(string nameSpace, string writeSourceTo, Type dynamicDtoType)
        {
            DtoModel dtoModel = new DtoModel(dynamicDtoType, nameSpace);
            WriteRenderedDto(writeSourceTo, dtoModel);
        }

        private static void WriteRenderedDto(string writeSourceTo, DtoModel dtoModel)
        {
            string csFile = "{0}.cs"._Format(dtoModel.TypeName);
            FileInfo csFileInfo = new FileInfo(Path.Combine(writeSourceTo, csFile));
            if (!csFileInfo.Directory.Exists)
            {
                csFileInfo.Directory.Create();
            }
            using (StreamWriter sw = new StreamWriter(csFileInfo.FullName))
            {
                sw.Write(dtoModel.Render());
            }
        }
    }
}
