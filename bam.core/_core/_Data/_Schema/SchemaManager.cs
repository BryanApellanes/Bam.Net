namespace Bam.Net.Data.Schema
{
    public partial class SchemaManager // core
    {
        string _rootDir;
        public string RootDir
        {
            get
            {
                return _rootDir ?? SchemaTempPathProvider(CurrentSchema);
            }

            set
            {
                _rootDir = value;
            }
        }
    }
}
