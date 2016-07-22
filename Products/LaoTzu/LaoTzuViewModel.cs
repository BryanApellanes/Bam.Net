/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using System.IO;
using Bam.Net;
using System.Reflection;
using System.Data.SqlClient;
using Bam.Net.Data.Schema;
using System.Windows.Forms;
using Bam.Net.Data.Model;
using Bam.Net.Data.Model.Windows;
using Bam.Net.Data.MsSql;
using System.Threading;
using Bam.Net.Data.Dynamic;
using Bam.Net.Data;

namespace laotzu
{
    public class LaoTzuViewModel: IHasRequiredProperties
    {
        private static string DefaultNamespace = "GeneratedByLaoTzu";
        private static string DefaultSettingsFileName = "Settings.laotzu.json";

        private static string LaoTzuFileFilter = "LaoTzu settings files|*.laotzu.json";
        private static string LaoTzuFileExtension = "laotzu.json";

        private static string MappedSchemaFileFilter = "LaoTzu schema files|*.lzs.json";
        private static string MappedSchemaExtension = "lzs.json";

        public LaoTzuViewModel() { }

        public LaoTzuViewModel(FormModelBinder binder, LaoTzuForm form)
        {
            DefaultConfiguration.SetProperties(this, false);
            if(string.IsNullOrEmpty(ExtractorFolder))
            {
                ExtractorFolder = ".";
            }
            this.Form = form;
            this.FormModelBinder = binder;
            this.MappedSchemaDefinition = new MappedSchemaDefinition(".\\LaoTzu.{0}"._Format(MappedSchemaExtension));
            this.Form.ListViewTables.ItemSelectionChanged += (o, a) =>
            {
                PopulateColumnNameMapList();
            };
        }

        public string[] RequiredProperties
        {
            get
            {
                return new string[] { "WorkspaceFolder", "ServerName", "DatabaseName" };
            }
        }

        public FormModelBinder FormModelBinder { get; private set; }
        public LaoTzuForm Form { get; private set; }
        public void BrowseWorkspaceFolderOnClick(object sender, EventArgs e)
        {
            Form.FolderBrowserWorkspace.RootFolder = Environment.SpecialFolder.MyComputer;
            Form.FolderBrowserWorkspace.SelectedPath = this.WorkspaceFolder;
            DialogResult result = Form.FolderBrowserWorkspace.ShowDialog();
            if (result == DialogResult.OK)
            {
                WorkspaceFolder = Form.FolderBrowserWorkspace.SelectedPath;
                FormModelBinder.BindTextForEachTaggedControl();
            }
        }

        public string ExtractorFolder { get; set; }

        string _schemaExtractorType;
        public string SchemaExtractorType
        {
            get
            {
                string firstFound = SchemaExtractorTypeList.FirstOrDefault();
                if (firstFound == null)
                {
                    _schemaExtractorType = "No Extractors Found";
                }
                else
                {
                    _schemaExtractorType = firstFound;
                }
                return _schemaExtractorType;
            }
            set
            {
                _schemaExtractorType = value;
            }
        }

        List<string> _extractorTypes;
        object _extractorLock = new object();

        public List<string> SchemaExtractorTypeList
        {
            get
            {
                return _extractorLock.DoubleCheckLock(ref _extractorTypes, () =>
                {
                    List<string> results = new List<string>();
                    ExtractorDir.GetFiles("*.dll").Each(fileInfo =>
                    {
                        try
                        {
                            results.AddRange(Assembly.LoadFrom(fileInfo.FullName)
                                .GetTypes()
                                .Where(t => t.ImplementsInterface<ISchemaExtractor>() && !t.IsAbstract)
                                .Select(t=>t.Name));
                        }catch{}
                    });
                    return results;
                });
            }
        }

        [ModelSetting]
        public string WorkspaceFolder { get; set; }
        [ModelSetting]
        public string ServerName { get; set; }
        [ModelSetting]
        public string DatabaseName { get; set; }
        [ModelSetting]
        public string UserName { get; set; }

        [ModelSetting]
        public CheckState IntegratedSecurity { get; set; }

        [ModelSetting]
        public string Namespace { get; set; }

        [ModelSetting]
        public string ConnectionString { get; set; }

        public void TestConnectionOnClick(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder conn = GetConnectionStringBuilder();
            using (SqlConnection c = new SqlConnection(conn.ConnectionString))
            {
                try
                {
                    c.Open();
                    MessageBox.Show("Success", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Failed: {0}"._Format(ex.Message), "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        public void AboutToolStripMenuItemOnClick(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();            
            about.ShowDialog();
        }

        public MappedSchemaDefinition MappedSchemaDefinition { get; set; }

        public void DatabaseNameOnTextChanged(object sender, EventArgs e)
        {
            this.DatabaseName = Form.TextBoxDatabaseName.Text;
        }

        public void UserNameOnTextChanged(object sender, EventArgs e)
        {
            this.UserName = Form.TextBoxUserName.Text;
        }

        public void NamespaceOnTextChanged(object sender, EventArgs e)
        {
            this.Namespace = Form.TextBoxNamespace.Text;
        }

        public void ConnectionStringOnTextChanged(object sender, EventArgs e)
        {
            this.ConnectionString = Form.TextBoxConnectionString.Text;
        }

        public void IntegratedSecurityOnCheckedChanged(object sender, EventArgs e)
        {
            IntegratedSecurity = ((CheckBox)sender).CheckState;
            if(IntegratedSecurity == CheckState.Checked)
            {
                FormModelBinder.Disable(Form.TextBoxUserName);
                FormModelBinder.Disable(Form.TextBoxPassword);
            }
            else if (IntegratedSecurity == CheckState.Unchecked)
            {
                FormModelBinder.Enable(Form.TextBoxUserName);
                FormModelBinder.Enable(Form.TextBoxPassword);
            }
        }

        public void SaveSettingsOnClick(object sender, EventArgs e)
        {
            Form.OpenFileDialog.InitialDirectory = this.WorkspaceFolder;
            Form.OpenFileDialog.FileName = DefaultSettingsFileName;
            Form.OpenFileDialog.Filter = LaoTzuFileFilter;
            Form.OpenFileDialog.DefaultExt = LaoTzuFileExtension;
            DialogResult result = Form.SaveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                object settings = this.CreateDynamicType<ModelSetting>().Construct();
                settings.CopyProperties(this);
                settings.ToJsonFile(Form.SaveFileDialog.FileName);
            }
        }

        public void LoadSettingsOnClick(object sender, EventArgs e)
        {
            Form.OpenFileDialog.InitialDirectory = this.WorkspaceFolder;
            Form.OpenFileDialog.Filter = LaoTzuFileFilter;
            Form.OpenFileDialog.DefaultExt = LaoTzuFileExtension;
            DialogResult result = Form.OpenFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FileInfo file = new FileInfo(Form.OpenFileDialog.FileName);
                LaoTzuViewModel loaded = file.FromJsonFile<LaoTzuViewModel>();
                this.GetType().GetPropertiesWithAttributeOfType<ModelSetting>().Each(pi =>
                {
                    FormModelBinder.SetByTag(pi.Name, pi.GetValue(loaded).ToString());
                });

                SetSettingsStatus("Settings file: {0}"._Format(file.FullName));
            }
        }

        public void ExtractSchemaOnClick(object sender, EventArgs e)
        {
            try
            {
                SchemaExtractor extractor = GetExtractor(); //new MsSqlSmoSchemaExtractor(new MsSqlDatabase(this.ServerName, this.DatabaseName, credentials));
                extractor.NameMap = MappedSchemaDefinition.SchemaNameMap;
                extractor.ProcessingTable += (o, args) =>
                {
                    FormModelBinder.AppendText(Form.TextBoxOutput, "Reading meta data for table {0}\r\n"._Format(((SchemaExtractorEventArgs)args).Table));
                    Form.TextBoxOutput.Refresh();
                    Thread.Sleep(3);
                };
                extractor.ProcessingColumn += (o, args) =>
                {
                    FormModelBinder.AppendText(Form.TextBoxOutput, "  Reading meta data for column {0}\r\n"._Format(((SchemaExtractorEventArgs)args).Column));
                    Form.TextBoxOutput.Refresh();
                    Thread.Sleep(3);
                };
                FormModelBinder.SetText(Form.TextBoxOutput, "");
                DisableInputs();
                SchemaDefinition schema = extractor.Extract();
                schema.Save(Path.Combine(WorkspaceFolder, "{0}.schema.json"._Format(schema.Name)));
                MappedSchemaDefinition.SchemaDefinition = schema;
                MappedSchemaDefinition.SchemaNameMap = extractor.NameMap;
                FormModelBinder.AppendText(Form.TextBoxOutput, "... Extraction Done ...");
                FormModelBinder.AppendText(Form.TextBoxOutput, "Populating schema tab ...");
                Task.Run(() =>
                {
                    PopulateTableNameMapList();
                    FormModelBinder.AppendText(Form.TextBoxOutput, "... Schema tab populated ...");
                });
                EnableInputs();
                Form.TabControlMain.SelectedTab = Form.TabPageSchemaInfo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SaveSchemaOnClick(object sender, EventArgs e)
        {
            Form.SaveFileDialog.InitialDirectory = this.WorkspaceFolder;
            Form.SaveFileDialog.Filter = MappedSchemaFileFilter;
            Form.SaveFileDialog.DefaultExt = MappedSchemaExtension;
            DialogResult result = Form.SaveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                MappedSchemaDefinition.FilePath = Form.SaveFileDialog.FileName;
                MappedSchemaDefinition.Save();

                SetSchemaStatus("Schema saved: {0}"._Format(MappedSchemaDefinition.FilePath));
            }
        }

        public void LoadSchemaOnClick(object sender, EventArgs e)
        {
            Form.OpenFileDialog.InitialDirectory = this.WorkspaceFolder;
            Form.OpenFileDialog.Filter = MappedSchemaFileFilter;
            Form.OpenFileDialog.DefaultExt = MappedSchemaExtension;
            DialogResult result = Form.OpenFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                TryOrShowMessage(() =>
                {
                    FileInfo schemaFile = new FileInfo(Form.OpenFileDialog.FileName);
                    MappedSchemaDefinition schema = MappedSchemaDefinition.Load(schemaFile);
                    MappedSchemaDefinition = schema;
                    PopulateTableNameMapList();
                    Form.TabControlMain.SelectedTab = Form.TabPageSchemaInfo;

                    SetSchemaStatus("Loaded schema: {0}"._Format(schemaFile.FullName));
                });
            }
        }

        public void ListViewTablesOnAfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Label))
            {
                string className = e.Label;
                ListViewItem item = Form.ListViewTables.Items[e.Item];
                object tag = item.Tag;
                TableNameToClassName mapping = tag as TableNameToClassName;
                if (mapping != null)
                {
                    mapping.ClassName = className;
                    item.Tag = mapping;
                    MappedSchemaDefinition.SchemaNameMap.Set(mapping);
                    MappedSchemaDefinition.Save();
                }
            }
        }

        public void ListViewColumnsOnAfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Label))
            {
                string propertyName = e.Label;
                ListViewItem item = Form.ListViewColumns.Items[e.Item];
                object tag = item.Tag;
                ColumnNameToPropertyName mapping = tag as ColumnNameToPropertyName;
                if (mapping != null)
                {
                    mapping.PropertyName = propertyName;
                    item.Tag = mapping;
                    MappedSchemaDefinition.SchemaNameMap.Set(mapping);
                    MappedSchemaDefinition.Save();
                }
            }
        }

        public void GenerateCSharpOnClick(object sender, EventArgs e)
        {
            DaoGenerator generator = new DaoGenerator();
            generator.Namespace = Namespace ?? DefaultNamespace;
            generator.BeforeClassStreamResolved += (s, t) => FormModelBinder.SetByTag("SchemaStatus", "Writing code for {0}"._Format(t.Name));
            DirectoryInfo srcDir = GetSourceDir(true);
            
            if (MappedSchemaDefinition.SchemaDefinition.Tables.Length == 0)
            {
                MessageBox.Show("No tables were found in the current schema.\r\n\r\nExtract or load a schema first.", "No Tables Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                generator.Generate(MappedSchemaDefinition.SchemaDefinition, srcDir.FullName);
            }
            string text = "Code generated to: {0}"._Format(srcDir.FullName);
            SetStatus(text);
        }

        public void GenerateAssemblyOnClick(object sender, EventArgs e)
        {
            DisableInputs();
            string text = "Generating ...";
            SetStatus(text);
            DaoAssemblyGenerator generator = GetDaoAssemblyGenerator();

            DirectoryInfo srcDir = GetSourceDir();
            string dest = string.Empty;
            DialogResult result = MessageBox.Show("Generate C# first?", "Generate C#?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                generator.GenerateSource(srcDir.FullName);
            }
            if (result == DialogResult.Yes || result == DialogResult.No)
            {
                GenerateAssembly(generator, srcDir, out dest);            
            }

            text = "Assemlby File: {0}"._Format(dest);
            SetStatus(text);
            EnableInputs();
        }

        public void ExitOnClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected DirectoryInfo ExtractorDir { get { return new DirectoryInfo(ExtractorFolder); } }

        private void GenerateAssembly(DaoAssemblyGenerator generator, DirectoryInfo srcDir, out string dest)
        {
            GeneratedAssemblyInfo assembly = generator.Compile(srcDir.FullName, "{0}.dll"._Format(Namespace));
            FileInfo assemblyFile = assembly.GetAssembly().GetFileInfo();
            dest = Path.Combine(this.WorkspaceFolder, "bin", assemblyFile.Name);
            DirectoryInfo destDir = new DirectoryInfo(Path.Combine(this.WorkspaceFolder, "bin"));
            if (!destDir.Exists)
            {
                Directory.CreateDirectory(destDir.FullName);
            }
            assemblyFile.CopyTo(dest);
        }

        private void SetStatus(string text)
        {
            SetSchemaStatus(text);
            SetSettingsStatus(text);
        }

        private DirectoryInfo GetSourceDir(bool backup = false)
        {
            DirectoryInfo srcDir = new DirectoryInfo(Path.Combine(this.WorkspaceFolder, "src", MappedSchemaDefinition.SchemaDefinition.Name));
            if (srcDir.Exists && backup)
            {
                srcDir.MoveTo("{0}_"._Format(srcDir.FullName).RandomLetters(4));
                srcDir = new DirectoryInfo(Path.Combine(this.WorkspaceFolder, "src", MappedSchemaDefinition.SchemaDefinition.Name));
            }

            return srcDir;
        }

        private DaoAssemblyGenerator GetDaoAssemblyGenerator()
        {
            DaoAssemblyGenerator generator = new DaoAssemblyGenerator(this.MappedSchemaDefinition.SchemaDefinition, this.MappedSchemaDefinition.SchemaNameMap);
            generator.Namespace = Namespace ?? DefaultNamespace;

            generator.OnTableStarted += (ns, table) =>
            {
                FormModelBinder.SetByTag("SchemaStatus", "Generating: {0}.{1}"._Format(ns, table.Name));
            };
            generator.OnGenerateStarted += (gen, sd) =>
            {
                FormModelBinder.SetByTag("SchemaStatus", "Generation started...");
            };
            generator.OnGenerateComplete += (gen, sd) =>
            {
                FormModelBinder.SetByTag("SchemaStatus", "Genertion complete");
            };
            return generator;
        }

        private void TryOrShowMessage(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private SqlConnectionStringBuilder GetConnectionStringBuilder()
        {
            
            SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder();
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                conn.ConnectionString = ConnectionString;
            }
            else
            {
                conn.Add("Data Source", this.ServerName);
                conn.Add("Initial Catalog", this.DatabaseName);
                if (IntegratedSecurity == CheckState.Unchecked)
                {
                    conn.Add("User Id", Form.TextBoxUserName.Text);
                    conn.Add("Password", Form.TextBoxPassword.Text);
                }
                else
                {
                    conn.Add("Integrated Security", "SSPI");
                }
            }
            return conn;
        }

        private void PopulateTableNameMapList()
        {
            TryOrShowMessage(() =>
            {
                FormModelBinder.ForListView(Form.ListViewTables, (lv) =>
                {
                    lv.Items.Clear();
                    SchemaNameMap schemaNameMap = MappedSchemaDefinition.SchemaNameMap;
                    schemaNameMap.TableNamesToClassNames.Each(map =>
                    {
                        ListViewItem tableItem = new ListViewItem(new string[] { map.ClassName, map.TableName });
                        tableItem.Tag = new TableNameToClassName { TableName = map.TableName, ClassName = map.ClassName };
                        lv.Items.Add(tableItem);
                    });
                });
            });
        }

        private void PopulateColumnNameMapList()
        {
            TryOrShowMessage(() =>
            {
                List<ListViewItem> selectedTables = new List<ListViewItem>();
                for (int i = 0; i < Form.ListViewTables.SelectedItems.Count; i++)
                {
                    selectedTables.Add(Form.ListViewTables.SelectedItems[i]);
                }
                ListViewItem tableItem = selectedTables.FirstOrDefault();
                if (tableItem != null)
                {
                    object tag = tableItem.Tag;
                    string tableName = tag != null ? ((TableNameToClassName)tableItem.Tag).TableName : tableItem.SubItems[0].Text;                    
                    Form.ListViewColumns.Items.Clear();
                    SchemaNameMap schemaNameMap = MappedSchemaDefinition.SchemaNameMap;
                    ColumnNameToPropertyName[] columnNamesToProperties = schemaNameMap.ColumnNamesToPropertyNames.Where(c => c.TableName.Equals(tableName)).ToArray();
                    foreach (ColumnNameToPropertyName map in columnNamesToProperties)
                    {
                        ListViewItem columnItem = new ListViewItem(new string[] { map.PropertyName, map.ColumnName });
                        columnItem.Tag = new ColumnNameToPropertyName { TableName = tableName, ColumnName = map.ColumnName, PropertyName = map.PropertyName };
                        Form.ListViewColumns.Items.Add(columnItem);
                    }
                }
            });
        }
        private void EnableInputs()
        {
            FormModelBinder.Enable<Button>();
            FormModelBinder.Enable<TextBox>();
            FormModelBinder.Enable<CheckBox>();
        }

        private void DisableInputs()
        {
            FormModelBinder.Disable<Button>();
            FormModelBinder.Disable<TextBox>();
            FormModelBinder.Disable<CheckBox>();
        }

        private void SetSchemaStatus(string text)
        {
            Form.SchemaStatus.Text = text;
        }

        private void SetSettingsStatus(string text)
        {
            Form.SettingsStatus.Text = text;
        }

        private SchemaExtractor GetExtractor()
        {
            return new MsSqlSchemaExtractor(GetMsSqlDatabase());
        }

        private MsSqlDatabase GetMsSqlDatabase()
        {
            MsSqlCredentials credentials = IntegratedSecurity == CheckState.Checked ? null : new MsSqlCredentials { UserName = Form.TextBoxUserName.Text, Password = Form.TextBoxPassword.Text };
            MsSqlDatabase result = new MsSqlDatabase(this.ServerName, this.DatabaseName, credentials);
            result.ConnectionString = GetConnectionStringBuilder().ConnectionString;
            return result;
        }
    }
}
