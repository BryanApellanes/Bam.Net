/*
	Copyright © Bryan Apellanes 2015  
*/
namespace laotzu
{
    partial class LaoTzuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        protected internal System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        protected internal void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaoTzuForm));
            this.TabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.tabControlConnectionInfo = new System.Windows.Forms.TabControl();
            this.tabPageConnectionInfo = new System.Windows.Forms.TabPage();
            this.TextBoxDatabaseName = new System.Windows.Forms.TextBox();
            this.CheckBoxIntegratedSecurity = new System.Windows.Forms.CheckBox();
            this.TextBoxUserName = new System.Windows.Forms.TextBox();
            this.labelDatabaseName = new System.Windows.Forms.Label();
            this.buttonTestConnection = new System.Windows.Forms.Button();
            this.labelUserName = new System.Windows.Forms.Label();
            this.TextBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.labelServerName = new System.Windows.Forms.Label();
            this.tabPageConnectionString = new System.Windows.Forms.TabPage();
            this.TextBoxConnectionString = new System.Windows.Forms.TextBox();
            this.buttonTestConnectionString = new System.Windows.Forms.Button();
            this.TextBoxOutput = new System.Windows.Forms.TextBox();
            this.statusStripSettingsTab = new System.Windows.Forms.StatusStrip();
            this.SettingsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonGenerateAssemblySettingsTab = new System.Windows.Forms.Button();
            this.buttonExtractSchema = new System.Windows.Forms.Button();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.textBoxWorkspaceFolder = new System.Windows.Forms.TextBox();
            this.ButtonBrowseForWorkspace = new System.Windows.Forms.Button();
            this.labelWorkspace = new System.Windows.Forms.Label();
            this.TabPageSchemaInfo = new System.Windows.Forms.TabPage();
            this.labelNamespace = new System.Windows.Forms.Label();
            this.TextBoxNamespace = new System.Windows.Forms.TextBox();
            this.buttonGenerateCSharp = new System.Windows.Forms.Button();
            this.buttonGenerateAssemblySchemaTab = new System.Windows.Forms.Button();
            this.statusStripSchemaTab = new System.Windows.Forms.StatusStrip();
            this.SchemaStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.ListViewTables = new System.Windows.Forms.ListView();
            this.classNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListViewColumns = new System.Windows.Forms.ListView();
            this.propertyNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FolderBrowserWorkspace = new System.Windows.Forms.FolderBrowserDialog();
            this.MenuStripMain = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveSchemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadSchemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.comboBoxDatabaseType = new System.Windows.Forms.ComboBox();
            this.TabControlMain.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.tabControlConnectionInfo.SuspendLayout();
            this.tabPageConnectionInfo.SuspendLayout();
            this.tabPageConnectionString.SuspendLayout();
            this.statusStripSettingsTab.SuspendLayout();
            this.groupBoxOptions.SuspendLayout();
            this.TabPageSchemaInfo.SuspendLayout();
            this.statusStripSchemaTab.SuspendLayout();
            this.MenuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControlMain
            // 
            this.TabControlMain.Controls.Add(this.tabPageSettings);
            this.TabControlMain.Controls.Add(this.TabPageSchemaInfo);
            this.TabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControlMain.Location = new System.Drawing.Point(0, 24);
            this.TabControlMain.Name = "TabControlMain";
            this.TabControlMain.SelectedIndex = 0;
            this.TabControlMain.Size = new System.Drawing.Size(624, 348);
            this.TabControlMain.TabIndex = 6;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.tabControlConnectionInfo);
            this.tabPageSettings.Controls.Add(this.TextBoxOutput);
            this.tabPageSettings.Controls.Add(this.statusStripSettingsTab);
            this.tabPageSettings.Controls.Add(this.buttonGenerateAssemblySettingsTab);
            this.tabPageSettings.Controls.Add(this.buttonExtractSchema);
            this.tabPageSettings.Controls.Add(this.groupBoxOptions);
            this.tabPageSettings.Controls.Add(this.comboBoxDatabaseType);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(616, 322);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // tabControlConnectionInfo
            // 
            this.tabControlConnectionInfo.Controls.Add(this.tabPageConnectionInfo);
            this.tabControlConnectionInfo.Controls.Add(this.tabPageConnectionString);
            this.tabControlConnectionInfo.Location = new System.Drawing.Point(8, 69);
            this.tabControlConnectionInfo.Name = "tabControlConnectionInfo";
            this.tabControlConnectionInfo.SelectedIndex = 0;
            this.tabControlConnectionInfo.Size = new System.Drawing.Size(340, 218);
            this.tabControlConnectionInfo.TabIndex = 22;
            // 
            // tabPageConnectionInfo
            // 
            this.tabPageConnectionInfo.Controls.Add(this.TextBoxDatabaseName);
            this.tabPageConnectionInfo.Controls.Add(this.CheckBoxIntegratedSecurity);
            this.tabPageConnectionInfo.Controls.Add(this.TextBoxUserName);
            this.tabPageConnectionInfo.Controls.Add(this.labelDatabaseName);
            this.tabPageConnectionInfo.Controls.Add(this.buttonTestConnection);
            this.tabPageConnectionInfo.Controls.Add(this.labelUserName);
            this.tabPageConnectionInfo.Controls.Add(this.TextBoxPassword);
            this.tabPageConnectionInfo.Controls.Add(this.labelPassword);
            this.tabPageConnectionInfo.Controls.Add(this.textBoxServerName);
            this.tabPageConnectionInfo.Controls.Add(this.labelServerName);
            this.tabPageConnectionInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPageConnectionInfo.Name = "tabPageConnectionInfo";
            this.tabPageConnectionInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConnectionInfo.Size = new System.Drawing.Size(332, 192);
            this.tabPageConnectionInfo.TabIndex = 0;
            this.tabPageConnectionInfo.Text = "Connection Info";
            this.tabPageConnectionInfo.UseVisualStyleBackColor = true;
            // 
            // TextBoxDatabaseName
            // 
            this.TextBoxDatabaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxDatabaseName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxDatabaseName.Location = new System.Drawing.Point(94, 115);
            this.TextBoxDatabaseName.Name = "TextBoxDatabaseName";
            this.TextBoxDatabaseName.Size = new System.Drawing.Size(233, 22);
            this.TextBoxDatabaseName.TabIndex = 29;
            this.TextBoxDatabaseName.Tag = "DatabaseName";
            // 
            // CheckBoxIntegratedSecurity
            // 
            this.CheckBoxIntegratedSecurity.AutoSize = true;
            this.CheckBoxIntegratedSecurity.Location = new System.Drawing.Point(94, 157);
            this.CheckBoxIntegratedSecurity.Name = "CheckBoxIntegratedSecurity";
            this.CheckBoxIntegratedSecurity.Size = new System.Drawing.Size(115, 17);
            this.CheckBoxIntegratedSecurity.TabIndex = 30;
            this.CheckBoxIntegratedSecurity.Tag = "IntegratedSecurity";
            this.CheckBoxIntegratedSecurity.Text = "Integrated Security";
            this.CheckBoxIntegratedSecurity.UseVisualStyleBackColor = true;
            // 
            // TextBoxUserName
            // 
            this.TextBoxUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxUserName.Location = new System.Drawing.Point(94, 51);
            this.TextBoxUserName.Name = "TextBoxUserName";
            this.TextBoxUserName.Size = new System.Drawing.Size(233, 22);
            this.TextBoxUserName.TabIndex = 27;
            this.TextBoxUserName.Tag = "UserName";
            // 
            // labelDatabaseName
            // 
            this.labelDatabaseName.AutoSize = true;
            this.labelDatabaseName.Location = new System.Drawing.Point(5, 118);
            this.labelDatabaseName.Name = "labelDatabaseName";
            this.labelDatabaseName.Size = new System.Drawing.Size(84, 13);
            this.labelDatabaseName.TabIndex = 35;
            this.labelDatabaseName.Text = "Database Name";
            // 
            // buttonTestConnection
            // 
            this.buttonTestConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTestConnection.Location = new System.Drawing.Point(215, 153);
            this.buttonTestConnection.Name = "buttonTestConnection";
            this.buttonTestConnection.Size = new System.Drawing.Size(112, 23);
            this.buttonTestConnection.TabIndex = 34;
            this.buttonTestConnection.Tag = "TestConnection";
            this.buttonTestConnection.Text = "Test Connection";
            this.buttonTestConnection.UseVisualStyleBackColor = true;
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Location = new System.Drawing.Point(28, 54);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(60, 13);
            this.labelUserName.TabIndex = 32;
            this.labelUserName.Text = "User Name";
            // 
            // TextBoxPassword
            // 
            this.TextBoxPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxPassword.Font = new System.Drawing.Font("Wingdings", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.TextBoxPassword.Location = new System.Drawing.Point(94, 83);
            this.TextBoxPassword.Name = "TextBoxPassword";
            this.TextBoxPassword.PasswordChar = 'N';
            this.TextBoxPassword.Size = new System.Drawing.Size(233, 22);
            this.TextBoxPassword.TabIndex = 28;
            this.TextBoxPassword.Tag = "Password";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(35, 86);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(53, 13);
            this.labelPassword.TabIndex = 33;
            this.labelPassword.Text = "Password";
            // 
            // textBoxServerName
            // 
            this.textBoxServerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxServerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxServerName.Location = new System.Drawing.Point(94, 17);
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.Size = new System.Drawing.Size(233, 22);
            this.textBoxServerName.TabIndex = 26;
            this.textBoxServerName.Tag = "ServerName";
            // 
            // labelServerName
            // 
            this.labelServerName.AutoSize = true;
            this.labelServerName.Location = new System.Drawing.Point(19, 21);
            this.labelServerName.Name = "labelServerName";
            this.labelServerName.Size = new System.Drawing.Size(69, 13);
            this.labelServerName.TabIndex = 31;
            this.labelServerName.Text = "Server Name";
            // 
            // tabPageConnectionString
            // 
            this.tabPageConnectionString.Controls.Add(this.TextBoxConnectionString);
            this.tabPageConnectionString.Controls.Add(this.buttonTestConnectionString);
            this.tabPageConnectionString.Location = new System.Drawing.Point(4, 22);
            this.tabPageConnectionString.Name = "tabPageConnectionString";
            this.tabPageConnectionString.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConnectionString.Size = new System.Drawing.Size(332, 192);
            this.tabPageConnectionString.TabIndex = 1;
            this.tabPageConnectionString.Text = "Connection String";
            this.tabPageConnectionString.UseVisualStyleBackColor = true;
            // 
            // TextBoxConnectionString
            // 
            this.TextBoxConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxConnectionString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxConnectionString.Location = new System.Drawing.Point(6, 20);
            this.TextBoxConnectionString.Name = "TextBoxConnectionString";
            this.TextBoxConnectionString.Size = new System.Drawing.Size(318, 22);
            this.TextBoxConnectionString.TabIndex = 36;
            this.TextBoxConnectionString.Tag = "ConnectionString";
            // 
            // buttonTestConnectionString
            // 
            this.buttonTestConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTestConnectionString.Location = new System.Drawing.Point(215, 153);
            this.buttonTestConnectionString.Name = "buttonTestConnectionString";
            this.buttonTestConnectionString.Size = new System.Drawing.Size(112, 23);
            this.buttonTestConnectionString.TabIndex = 35;
            this.buttonTestConnectionString.Tag = "TestConnection";
            this.buttonTestConnectionString.Text = "Test Connection";
            this.buttonTestConnectionString.UseVisualStyleBackColor = true;
            // 
            // TextBoxOutput
            // 
            this.TextBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxOutput.Location = new System.Drawing.Point(354, 6);
            this.TextBoxOutput.Multiline = true;
            this.TextBoxOutput.Name = "TextBoxOutput";
            this.TextBoxOutput.ReadOnly = true;
            this.TextBoxOutput.Size = new System.Drawing.Size(247, 244);
            this.TextBoxOutput.TabIndex = 21;
            // 
            // statusStripSettingsTab
            // 
            this.statusStripSettingsTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingsStatus});
            this.statusStripSettingsTab.Location = new System.Drawing.Point(3, 297);
            this.statusStripSettingsTab.Name = "statusStripSettingsTab";
            this.statusStripSettingsTab.Size = new System.Drawing.Size(610, 22);
            this.statusStripSettingsTab.TabIndex = 20;
            // 
            // SettingsStatus
            // 
            this.SettingsStatus.Name = "SettingsStatus";
            this.SettingsStatus.Size = new System.Drawing.Size(0, 17);
            this.SettingsStatus.Tag = "SettingsStatus";
            // 
            // buttonGenerateAssemblySettingsTab
            // 
            this.buttonGenerateAssemblySettingsTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerateAssemblySettingsTab.Location = new System.Drawing.Point(480, 264);
            this.buttonGenerateAssemblySettingsTab.Name = "buttonGenerateAssemblySettingsTab";
            this.buttonGenerateAssemblySettingsTab.Size = new System.Drawing.Size(121, 23);
            this.buttonGenerateAssemblySettingsTab.TabIndex = 19;
            this.buttonGenerateAssemblySettingsTab.Tag = "GenerateAssembly";
            this.buttonGenerateAssemblySettingsTab.Text = "Generate Assembly";
            this.buttonGenerateAssemblySettingsTab.UseVisualStyleBackColor = true;
            // 
            // buttonExtractSchema
            // 
            this.buttonExtractSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExtractSchema.Location = new System.Drawing.Point(353, 264);
            this.buttonExtractSchema.Name = "buttonExtractSchema";
            this.buttonExtractSchema.Size = new System.Drawing.Size(121, 23);
            this.buttonExtractSchema.TabIndex = 18;
            this.buttonExtractSchema.Tag = "ExtractSchema";
            this.buttonExtractSchema.Text = "Extract Schema";
            this.buttonExtractSchema.UseVisualStyleBackColor = true;
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.textBoxWorkspaceFolder);
            this.groupBoxOptions.Controls.Add(this.ButtonBrowseForWorkspace);
            this.groupBoxOptions.Controls.Add(this.labelWorkspace);
            this.groupBoxOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBoxOptions.Location = new System.Drawing.Point(8, 6);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(337, 57);
            this.groupBoxOptions.TabIndex = 17;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // textBoxWorkspaceFolder
            // 
            this.textBoxWorkspaceFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWorkspaceFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxWorkspaceFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxWorkspaceFolder.Location = new System.Drawing.Point(116, 14);
            this.textBoxWorkspaceFolder.Name = "textBoxWorkspaceFolder";
            this.textBoxWorkspaceFolder.Size = new System.Drawing.Size(165, 22);
            this.textBoxWorkspaceFolder.TabIndex = 1;
            this.textBoxWorkspaceFolder.Tag = "WorkspaceFolder";
            // 
            // ButtonBrowseForWorkspace
            // 
            this.ButtonBrowseForWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonBrowseForWorkspace.Location = new System.Drawing.Point(287, 10);
            this.ButtonBrowseForWorkspace.Name = "ButtonBrowseForWorkspace";
            this.ButtonBrowseForWorkspace.Size = new System.Drawing.Size(41, 28);
            this.ButtonBrowseForWorkspace.TabIndex = 16;
            this.ButtonBrowseForWorkspace.Tag = "BrowseWorkspaceFolder";
            this.ButtonBrowseForWorkspace.Text = "...";
            this.ButtonBrowseForWorkspace.UseVisualStyleBackColor = true;
            // 
            // labelWorkspace
            // 
            this.labelWorkspace.AutoSize = true;
            this.labelWorkspace.Location = new System.Drawing.Point(16, 18);
            this.labelWorkspace.Name = "labelWorkspace";
            this.labelWorkspace.Size = new System.Drawing.Size(94, 13);
            this.labelWorkspace.TabIndex = 15;
            this.labelWorkspace.Text = "Workspace Folder";
            // 
            // TabPageSchemaInfo
            // 
            this.TabPageSchemaInfo.Controls.Add(this.labelNamespace);
            this.TabPageSchemaInfo.Controls.Add(this.TextBoxNamespace);
            this.TabPageSchemaInfo.Controls.Add(this.buttonGenerateCSharp);
            this.TabPageSchemaInfo.Controls.Add(this.buttonGenerateAssemblySchemaTab);
            this.TabPageSchemaInfo.Controls.Add(this.statusStripSchemaTab);
            this.TabPageSchemaInfo.Controls.Add(this.ListViewTables);
            this.TabPageSchemaInfo.Controls.Add(this.ListViewColumns);
            this.TabPageSchemaInfo.Location = new System.Drawing.Point(4, 22);
            this.TabPageSchemaInfo.Name = "TabPageSchemaInfo";
            this.TabPageSchemaInfo.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageSchemaInfo.Size = new System.Drawing.Size(616, 322);
            this.TabPageSchemaInfo.TabIndex = 0;
            this.TabPageSchemaInfo.Text = "Schema";
            this.TabPageSchemaInfo.UseVisualStyleBackColor = true;
            // 
            // labelNamespace
            // 
            this.labelNamespace.AutoSize = true;
            this.labelNamespace.Location = new System.Drawing.Point(9, 269);
            this.labelNamespace.Name = "labelNamespace";
            this.labelNamespace.Size = new System.Drawing.Size(64, 13);
            this.labelNamespace.TabIndex = 23;
            this.labelNamespace.Text = "Namespace";
            // 
            // TextBoxNamespace
            // 
            this.TextBoxNamespace.Location = new System.Drawing.Point(79, 266);
            this.TextBoxNamespace.Name = "TextBoxNamespace";
            this.TextBoxNamespace.Size = new System.Drawing.Size(268, 20);
            this.TextBoxNamespace.TabIndex = 22;
            this.TextBoxNamespace.Tag = "Namespace";
            // 
            // buttonGenerateCSharp
            // 
            this.buttonGenerateCSharp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerateCSharp.Location = new System.Drawing.Point(353, 264);
            this.buttonGenerateCSharp.Name = "buttonGenerateCSharp";
            this.buttonGenerateCSharp.Size = new System.Drawing.Size(121, 23);
            this.buttonGenerateCSharp.TabIndex = 21;
            this.buttonGenerateCSharp.Tag = "GenerateCSharp";
            this.buttonGenerateCSharp.Text = "Generate C#";
            this.buttonGenerateCSharp.UseVisualStyleBackColor = true;
            // 
            // buttonGenerateAssemblySchemaTab
            // 
            this.buttonGenerateAssemblySchemaTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerateAssemblySchemaTab.Location = new System.Drawing.Point(480, 264);
            this.buttonGenerateAssemblySchemaTab.Name = "buttonGenerateAssemblySchemaTab";
            this.buttonGenerateAssemblySchemaTab.Size = new System.Drawing.Size(121, 23);
            this.buttonGenerateAssemblySchemaTab.TabIndex = 20;
            this.buttonGenerateAssemblySchemaTab.Tag = "GenerateAssembly";
            this.buttonGenerateAssemblySchemaTab.Text = "Generate Assembly";
            this.buttonGenerateAssemblySchemaTab.UseVisualStyleBackColor = true;
            // 
            // statusStripSchemaTab
            // 
            this.statusStripSchemaTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SchemaStatus});
            this.statusStripSchemaTab.Location = new System.Drawing.Point(3, 297);
            this.statusStripSchemaTab.Name = "statusStripSchemaTab";
            this.statusStripSchemaTab.Size = new System.Drawing.Size(610, 22);
            this.statusStripSchemaTab.TabIndex = 11;
            this.statusStripSchemaTab.Tag = "";
            // 
            // SchemaStatus
            // 
            this.SchemaStatus.Name = "SchemaStatus";
            this.SchemaStatus.Size = new System.Drawing.Size(0, 17);
            this.SchemaStatus.Tag = "SchemaStatus";
            // 
            // ListViewTables
            // 
            this.ListViewTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListViewTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.classNameColumnHeader,
            this.tableNameColumnHeader});
            this.ListViewTables.FullRowSelect = true;
            this.ListViewTables.GridLines = true;
            this.ListViewTables.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListViewTables.LabelEdit = true;
            this.ListViewTables.Location = new System.Drawing.Point(6, 6);
            this.ListViewTables.MultiSelect = false;
            this.ListViewTables.Name = "ListViewTables";
            this.ListViewTables.ShowGroups = false;
            this.ListViewTables.Size = new System.Drawing.Size(257, 247);
            this.ListViewTables.TabIndex = 7;
            this.ListViewTables.Tag = "ListViewTables";
            this.ListViewTables.UseCompatibleStateImageBehavior = false;
            this.ListViewTables.View = System.Windows.Forms.View.Details;
            // 
            // classNameColumnHeader
            // 
            this.classNameColumnHeader.Text = "Class Name";
            this.classNameColumnHeader.Width = 137;
            // 
            // tableNameColumnHeader
            // 
            this.tableNameColumnHeader.Text = "Table Name";
            this.tableNameColumnHeader.Width = 112;
            // 
            // ListViewColumns
            // 
            this.ListViewColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListViewColumns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListViewColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.propertyNameHeader,
            this.columnNameHeader});
            this.ListViewColumns.FullRowSelect = true;
            this.ListViewColumns.GridLines = true;
            this.ListViewColumns.LabelEdit = true;
            this.ListViewColumns.Location = new System.Drawing.Point(269, 6);
            this.ListViewColumns.MultiSelect = false;
            this.ListViewColumns.Name = "ListViewColumns";
            this.ListViewColumns.Size = new System.Drawing.Size(332, 247);
            this.ListViewColumns.TabIndex = 2;
            this.ListViewColumns.Tag = "ListViewColumns";
            this.ListViewColumns.UseCompatibleStateImageBehavior = false;
            this.ListViewColumns.View = System.Windows.Forms.View.Details;
            // 
            // propertyNameHeader
            // 
            this.propertyNameHeader.Text = "Property Name";
            this.propertyNameHeader.Width = 119;
            // 
            // columnNameHeader
            // 
            this.columnNameHeader.Text = "Column Name";
            this.columnNameHeader.Width = 126;
            // 
            // FolderBrowserWorkspace
            // 
            this.FolderBrowserWorkspace.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.FolderBrowserWorkspace.SelectedPath = "laotzu";
            // 
            // MenuStripMain
            // 
            this.MenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.MenuStripMain.Location = new System.Drawing.Point(0, 0);
            this.MenuStripMain.Name = "MenuStripMain";
            this.MenuStripMain.Size = new System.Drawing.Size(624, 24);
            this.MenuStripMain.TabIndex = 7;
            this.MenuStripMain.Tag = "MenuStripMain";
            this.MenuStripMain.Text = "menuStrip";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveToolStripMenuItem,
            this.LoadToolStripMenuItem,
            this.toolStripSeparator1,
            this.SaveSchemaToolStripMenuItem,
            this.LoadSchemaToolStripMenuItem,
            this.toolStripSeparator2,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Tag = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Text = "File";
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.SaveToolStripMenuItem.Tag = "SaveSettings";
            this.SaveToolStripMenuItem.Text = "Save Settings";
            // 
            // LoadToolStripMenuItem
            // 
            this.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem";
            this.LoadToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.LoadToolStripMenuItem.Tag = "LoadSettings";
            this.LoadToolStripMenuItem.Text = "Load Settings";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(142, 6);
            // 
            // SaveSchemaToolStripMenuItem
            // 
            this.SaveSchemaToolStripMenuItem.Name = "SaveSchemaToolStripMenuItem";
            this.SaveSchemaToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.SaveSchemaToolStripMenuItem.Tag = "SaveSchema";
            this.SaveSchemaToolStripMenuItem.Text = "Save Schema";
            // 
            // LoadSchemaToolStripMenuItem
            // 
            this.LoadSchemaToolStripMenuItem.Name = "LoadSchemaToolStripMenuItem";
            this.LoadSchemaToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.LoadSchemaToolStripMenuItem.Tag = "LoadSchema";
            this.LoadSchemaToolStripMenuItem.Text = "Load Schema";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(142, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.ExitToolStripMenuItem.Tag = "Exit";
            this.ExitToolStripMenuItem.Text = "Exit";
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpToolStripMenuItem.Tag = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Text = "Help";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.AboutToolStripMenuItem.Tag = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Text = "About";
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.DefaultExt = "laotzu.json";
            this.SaveFileDialog.Filter = "LaoTzu files|*.laotzu.json";
            this.SaveFileDialog.SupportMultiDottedExtensions = true;
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.Filter = "LaoTzu files|*.laotzu.json";
            // 
            // comboBoxDatabaseType
            // 
            this.comboBoxDatabaseType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDatabaseType.FormattingEnabled = true;
            this.comboBoxDatabaseType.Location = new System.Drawing.Point(12, 265);
            this.comboBoxDatabaseType.Name = "comboBoxDatabaseType";
            this.comboBoxDatabaseType.Size = new System.Drawing.Size(336, 21);
            this.comboBoxDatabaseType.TabIndex = 23;
            // 
            // LaoTzuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 372);
            this.Controls.Add(this.TabControlMain);
            this.Controls.Add(this.MenuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStripMain;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(640, 410);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 410);
            this.Name = "LaoTzuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LaoTzu";
            this.TabControlMain.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            this.tabControlConnectionInfo.ResumeLayout(false);
            this.tabPageConnectionInfo.ResumeLayout(false);
            this.tabPageConnectionInfo.PerformLayout();
            this.tabPageConnectionString.ResumeLayout(false);
            this.tabPageConnectionString.PerformLayout();
            this.statusStripSettingsTab.ResumeLayout(false);
            this.statusStripSettingsTab.PerformLayout();
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.TabPageSchemaInfo.ResumeLayout(false);
            this.TabPageSchemaInfo.PerformLayout();
            this.statusStripSchemaTab.ResumeLayout(false);
            this.statusStripSchemaTab.PerformLayout();
            this.MenuStripMain.ResumeLayout(false);
            this.MenuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected internal System.Windows.Forms.TabPage tabPageSettings;
        protected internal System.Windows.Forms.TabPage TabPageSchemaInfo;
        protected internal System.Windows.Forms.ColumnHeader tableNameColumnHeader;
        protected internal System.Windows.Forms.ColumnHeader columnNameHeader;
        protected internal System.Windows.Forms.ColumnHeader propertyNameHeader;
        protected internal System.Windows.Forms.Button ButtonBrowseForWorkspace;
        protected internal System.Windows.Forms.Label labelWorkspace;
        protected internal System.Windows.Forms.TextBox textBoxWorkspaceFolder;
        protected internal System.Windows.Forms.GroupBox groupBoxOptions;
        protected internal System.Windows.Forms.TextBox TextBoxOutput;
        protected internal System.Windows.Forms.StatusStrip statusStripSettingsTab;
        protected internal System.Windows.Forms.Button buttonGenerateAssemblySettingsTab;
        protected internal System.Windows.Forms.Button buttonExtractSchema;
        protected internal System.Windows.Forms.Button buttonGenerateAssemblySchemaTab;
        protected internal System.Windows.Forms.StatusStrip statusStripSchemaTab;
        public System.Windows.Forms.ColumnHeader classNameColumnHeader;
        protected internal System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.FolderBrowserDialog FolderBrowserWorkspace;
        protected internal System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        public System.Windows.Forms.MenuStrip MenuStripMain;
        public System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem LoadToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        public System.Windows.Forms.SaveFileDialog SaveFileDialog;
        public System.Windows.Forms.OpenFileDialog OpenFileDialog;
        public System.Windows.Forms.Button buttonGenerateCSharp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ListView ListViewTables;
        public System.Windows.Forms.ListView ListViewColumns;
        public System.Windows.Forms.TabControl TabControlMain;
        public System.Windows.Forms.ToolStripMenuItem LoadSchemaToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem SaveSchemaToolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel SettingsStatus;
        public System.Windows.Forms.ToolStripStatusLabel SchemaStatus;
        private System.Windows.Forms.Label labelNamespace;
        public System.Windows.Forms.TextBox TextBoxNamespace;
        private System.Windows.Forms.TabControl tabControlConnectionInfo;
        private System.Windows.Forms.TabPage tabPageConnectionInfo;
        public System.Windows.Forms.TextBox TextBoxDatabaseName;
        private System.Windows.Forms.CheckBox CheckBoxIntegratedSecurity;
        public System.Windows.Forms.TextBox TextBoxUserName;
        protected internal System.Windows.Forms.Label labelDatabaseName;
        protected internal System.Windows.Forms.Button buttonTestConnection;
        protected internal System.Windows.Forms.Label labelUserName;
        public System.Windows.Forms.TextBox TextBoxPassword;
        protected internal System.Windows.Forms.Label labelPassword;
        protected internal System.Windows.Forms.TextBox textBoxServerName;
        protected internal System.Windows.Forms.Label labelServerName;
        private System.Windows.Forms.TabPage tabPageConnectionString;
        protected internal System.Windows.Forms.Button buttonTestConnectionString;
        public System.Windows.Forms.TextBox TextBoxConnectionString;
        private System.Windows.Forms.ComboBox comboBoxDatabaseType;
    }
}