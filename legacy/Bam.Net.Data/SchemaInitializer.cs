/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bam.Net.Logging;

namespace Bam.Net.Data
{
    /// <summary>
    /// This class takes responsibility for initializing databases and their 
    /// related schemas.
    /// </summary>
    public class SchemaInitializer
    {
        public SchemaInitializer() { }

        public SchemaInitializer(string schemaContextAssemblyQaulifiedName, string registrarCallerAssemblyQaulifiedName)
        {
            this.SchemaContext = schemaContextAssemblyQaulifiedName;
            this.RegistrarCaller = registrarCallerAssemblyQaulifiedName;
        }
        public SchemaInitializer(Type schemaContextType, Type registrarCallerType)
            : this(schemaContextType.AssemblyQualifiedName, registrarCallerType.AssemblyQualifiedName)
        {
        }

        /// <summary>
        /// The FullName of the database context to initialize if AssemblyPath is specified.
        /// Otherwise, the assembly qualified type name.
        /// </summary>
        public string SchemaContext { get; set; }

        /// <summary>
        /// The AssemblyQualifiedName of an IRegistrarCaller implementation
        /// used to register the underlying database type (SQLite, SqlClient, etc.)
        /// </summary>
        public string RegistrarCaller { get; set; }

        /// <summary>
        /// If specified, should be the path to the assembly containing the 
        /// SchemaContext to be initialized.
        /// </summary>
        public string SchemaAssemblyPath { get; set; }

        protected internal string SchemaName { get; set; }

        public bool Initialize(ILogger logger, out Exception ex)
        {
            bool success = false;
            ex = null;
            try
            {
                Assembly assembly;
                Type context;
                if (!string.IsNullOrEmpty(SchemaAssemblyPath))
                {
                    assembly = Assembly.LoadFrom(SchemaAssemblyPath);
                    context = assembly.GetType(SchemaContext); 
                    if (context == null)
                    {
                        context = assembly.GetTypes().Where(t => t.AssemblyQualifiedName.Equals(SchemaContext)).FirstOrDefault();
                    }
                }
                else
                {
                    context = Type.GetType(SchemaContext);
                }
                
                Args.ThrowIf<ArgumentException>(context == null, "The specified SchemaContext ({0}) was not found", SchemaContext);

                PropertyInfo prop = context.GetProperty("ConnectionName");
                Args.ThrowIf<ArgumentException>(prop == null, "{0}.ConnectionName property was not found, make sure you're using the latest Bam.Net.Data.Schema.dll", context.Name);                

                SchemaName = (string)prop.GetValue(null);

                RegistrarCallerFactory registrarFactory = new RegistrarCallerFactory();
                IRegistrarCaller registrarCaller = registrarFactory.CreateRegistrarCaller(RegistrarCaller);
                Args.ThrowIf<ArgumentException>(registrarCaller == null, "Unable to instanciate IRegistrarCaller of type ({0})", RegistrarCaller);

                registrarCaller.Register(SchemaName);

                Exception ensureSchemaException;
                if (!Db.TryEnsureSchema(SchemaName, out ensureSchemaException))
                {
                    string properties = this.PropertiesToString("\r\n\t");
                    logger.AddEntry("A non fatal error occurred initializing schema ({0}) from ({1}): {2}\r\n{3}", 
                        LogEventType.Warning, 
                        SchemaName, 
                        SchemaContext,
                        ensureSchemaException.Message,
                        properties
                    );
                }

                success = true;
            }
            catch (Exception e)
            {
                ex = e;
                success = false;
            }

            return success;
        }
    }
}
