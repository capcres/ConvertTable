namespace ConvertTable
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mainTableLayout = new TableLayoutPanel();
            mainTabControl = new TabControl();
            convertTabPage = new TabPage();
            convertTableLayout = new TableLayoutPanel();
            fileListLabel = new Label();
            fileTreeView = new TreeView();
            refreshButton = new Button();
            targetPanel = new TableLayoutPanel();
            targetFolderLabel = new Label();
            targetFolderTextBox = new TextBox();
            browseButton = new Button();
            encryptionPanel = new TableLayoutPanel();
            encryptionLabel = new Label();
            encryptionComboBox = new ComboBox();
            passwordLabel = new Label();
            passwordTextBox = new TextBox();
            convertButton = new Button();
            logTabPage = new TabPage();
            logTableLayout = new TableLayoutPanel();
            progressBar = new ProgressBar();
            logTextBox = new RichTextBox();
            mainTableLayout.SuspendLayout();
            mainTabControl.SuspendLayout();
            convertTabPage.SuspendLayout();
            convertTableLayout.SuspendLayout();
            targetPanel.SuspendLayout();
            encryptionPanel.SuspendLayout();
            logTabPage.SuspendLayout();
            logTableLayout.SuspendLayout();
            SuspendLayout();
            // 
            // mainTableLayout
            // 
            mainTableLayout.ColumnCount = 1;
            mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainTableLayout.Controls.Add(mainTabControl, 0, 0);
            mainTableLayout.Dock = DockStyle.Fill;
            mainTableLayout.Location = new Point(0, 0);
            mainTableLayout.Name = "mainTableLayout";
            mainTableLayout.Padding = new Padding(10);
            mainTableLayout.RowCount = 1;
            mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainTableLayout.Size = new Size(1055, 857);
            mainTableLayout.TabIndex = 0;
            // 
            // mainTabControl
            // 
            mainTabControl.Controls.Add(convertTabPage);
            mainTabControl.Controls.Add(logTabPage);
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Location = new Point(13, 13);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(1029, 831);
            mainTabControl.TabIndex = 0;
            // 
            // convertTabPage
            // 
            convertTabPage.Controls.Add(convertTableLayout);
            convertTabPage.Location = new Point(4, 29);
            convertTabPage.Name = "convertTabPage";
            convertTabPage.Padding = new Padding(3);
            convertTabPage.Size = new Size(1021, 798);
            convertTabPage.TabIndex = 0;
            convertTabPage.Text = "Convert";
            convertTabPage.UseVisualStyleBackColor = true;
            // 
            // convertTableLayout
            // 
            convertTableLayout.ColumnCount = 1;
            convertTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            convertTableLayout.Controls.Add(fileListLabel, 0, 0);
            convertTableLayout.Controls.Add(fileTreeView, 0, 1);
            convertTableLayout.Controls.Add(refreshButton, 0, 2);
            convertTableLayout.Controls.Add(targetPanel, 0, 3);
            convertTableLayout.Controls.Add(encryptionPanel, 0, 4);
            convertTableLayout.Controls.Add(convertButton, 0, 5);
            convertTableLayout.Dock = DockStyle.Fill;
            convertTableLayout.Location = new Point(3, 3);
            convertTableLayout.Name = "convertTableLayout";
            convertTableLayout.RowCount = 7;
            convertTableLayout.RowStyles.Add(new RowStyle());
            convertTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            convertTableLayout.RowStyles.Add(new RowStyle());
            convertTableLayout.RowStyles.Add(new RowStyle());
            convertTableLayout.RowStyles.Add(new RowStyle());
            convertTableLayout.RowStyles.Add(new RowStyle());
            convertTableLayout.RowStyles.Add(new RowStyle());
            convertTableLayout.Size = new Size(1015, 792);
            convertTableLayout.TabIndex = 0;
            // 
            // fileListLabel
            // 
            fileListLabel.AutoSize = true;
            fileListLabel.Location = new Point(3, 0);
            fileListLabel.Name = "fileListLabel";
            fileListLabel.Size = new Size(80, 20);
            fileListLabel.TabIndex = 0;
            fileListLabel.Text = "Excel Files:";
            // 
            // fileTreeView
            // 
            fileTreeView.CheckBoxes = true;
            fileTreeView.Dock = DockStyle.Fill;
            fileTreeView.Location = new Point(3, 23);
            fileTreeView.Name = "fileTreeView";
            fileTreeView.Size = new Size(1009, 562);
            fileTreeView.TabIndex = 1;
            // 
            // refreshButton
            // 
            refreshButton.Dock = DockStyle.Fill;
            refreshButton.Location = new Point(3, 591);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(1009, 35);
            refreshButton.TabIndex = 2;
            refreshButton.Text = "새로고침";
            refreshButton.UseVisualStyleBackColor = true;
            // 
            // targetPanel
            // 
            targetPanel.ColumnCount = 2;
            targetPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            targetPanel.ColumnStyles.Add(new ColumnStyle());
            targetPanel.Controls.Add(targetFolderLabel, 0, 0);
            targetPanel.Controls.Add(targetFolderTextBox, 0, 1);
            targetPanel.Controls.Add(browseButton, 1, 1);
            targetPanel.Dock = DockStyle.Fill;
            targetPanel.Location = new Point(3, 632);
            targetPanel.Name = "targetPanel";
            targetPanel.RowCount = 2;
            targetPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            targetPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            targetPanel.Size = new Size(1009, 55);
            targetPanel.TabIndex = 3;
            // 
            // targetFolderLabel
            // 
            targetFolderLabel.AutoSize = true;
            targetFolderLabel.Location = new Point(3, 0);
            targetFolderLabel.Name = "targetFolderLabel";
            targetFolderLabel.Size = new Size(102, 20);
            targetFolderLabel.TabIndex = 0;
            targetFolderLabel.Text = "Target Folder:";
            // 
            // targetFolderTextBox
            // 
            targetFolderTextBox.Dock = DockStyle.Fill;
            targetFolderTextBox.Location = new Point(3, 23);
            targetFolderTextBox.Name = "targetFolderTextBox";
            targetFolderTextBox.Size = new Size(917, 27);
            targetFolderTextBox.TabIndex = 4;
            // 
            // browseButton
            // 
            browseButton.Location = new Point(926, 23);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(80, 27);
            browseButton.TabIndex = 5;
            browseButton.Text = "Browse";
            browseButton.UseVisualStyleBackColor = true;
            // 
            // encryptionPanel
            // 
            encryptionPanel.ColumnCount = 2;
            encryptionPanel.ColumnStyles.Add(new ColumnStyle());
            encryptionPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            encryptionPanel.Controls.Add(encryptionLabel, 0, 0);
            encryptionPanel.Controls.Add(encryptionComboBox, 0, 1);
            encryptionPanel.Controls.Add(passwordLabel, 1, 0);
            encryptionPanel.Controls.Add(passwordTextBox, 1, 1);
            encryptionPanel.Dock = DockStyle.Fill;
            encryptionPanel.Location = new Point(3, 693);
            encryptionPanel.Name = "encryptionPanel";
            encryptionPanel.RowCount = 2;
            encryptionPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            encryptionPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            encryptionPanel.Size = new Size(1009, 55);
            encryptionPanel.TabIndex = 6;
            // 
            // encryptionLabel
            // 
            encryptionLabel.AutoSize = true;
            encryptionLabel.Location = new Point(3, 0);
            encryptionLabel.Name = "encryptionLabel";
            encryptionLabel.Size = new Size(143, 20);
            encryptionLabel.TabIndex = 0;
            encryptionLabel.Text = "Encryption Method:";
            // 
            // encryptionComboBox
            // 
            encryptionComboBox.Dock = DockStyle.Fill;
            encryptionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            encryptionComboBox.FormattingEnabled = true;
            encryptionComboBox.Items.AddRange(new object[] { "DES", "AES256CBC" });
            encryptionComboBox.Location = new Point(3, 23);
            encryptionComboBox.Name = "encryptionComboBox";
            encryptionComboBox.Size = new Size(374, 28);
            encryptionComboBox.TabIndex = 7;
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(383, 0);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(75, 20);
            passwordLabel.TabIndex = 8;
            passwordLabel.Text = "Password:";
            // 
            // passwordTextBox
            // 
            passwordTextBox.Dock = DockStyle.Fill;
            passwordTextBox.Location = new Point(383, 23);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(623, 27);
            passwordTextBox.TabIndex = 8;
            // 
            // convertButton
            // 
            convertButton.Dock = DockStyle.Fill;
            convertButton.Location = new Point(3, 754);
            convertButton.Name = "convertButton";
            convertButton.Size = new Size(1009, 35);
            convertButton.TabIndex = 9;
            convertButton.Text = "Convert";
            convertButton.UseVisualStyleBackColor = true;
            // 
            // logTabPage
            // 
            logTabPage.Controls.Add(logTableLayout);
            logTabPage.Location = new Point(4, 29);
            logTabPage.Name = "logTabPage";
            logTabPage.Padding = new Padding(3);
            logTabPage.Size = new Size(1021, 798);
            logTabPage.TabIndex = 1;
            logTabPage.Text = "Log";
            logTabPage.UseVisualStyleBackColor = true;
            // 
            // logTableLayout
            // 
            logTableLayout.ColumnCount = 1;
            logTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            logTableLayout.Controls.Add(progressBar, 0, 0);
            logTableLayout.Controls.Add(logTextBox, 0, 1);
            logTableLayout.Dock = DockStyle.Fill;
            logTableLayout.Location = new Point(3, 3);
            logTableLayout.Name = "logTableLayout";
            logTableLayout.RowCount = 2;
            logTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            logTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            logTableLayout.Size = new Size(1015, 792);
            logTableLayout.TabIndex = 0;
            // 
            // progressBar
            // 
            progressBar.Dock = DockStyle.Fill;
            progressBar.Location = new Point(3, 3);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(1009, 19);
            progressBar.TabIndex = 12;
            // 
            // logTextBox
            // 
            logTextBox.Dock = DockStyle.Fill;
            logTextBox.Location = new Point(3, 28);
            logTextBox.Name = "logTextBox";
            logTextBox.ReadOnly = true;
            logTextBox.Size = new Size(1009, 761);
            logTextBox.TabIndex = 11;
            logTextBox.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1055, 857);
            Controls.Add(mainTableLayout);
            Name = "MainForm";
            Text = "Convert Table";
            mainTableLayout.ResumeLayout(false);
            mainTabControl.ResumeLayout(false);
            convertTabPage.ResumeLayout(false);
            convertTableLayout.ResumeLayout(false);
            convertTableLayout.PerformLayout();
            targetPanel.ResumeLayout(false);
            targetPanel.PerformLayout();
            encryptionPanel.ResumeLayout(false);
            encryptionPanel.PerformLayout();
            logTabPage.ResumeLayout(false);
            logTableLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel mainTableLayout;
        private TabControl mainTabControl;
        private TabPage convertTabPage;
        private TabPage logTabPage;
        private TableLayoutPanel convertTableLayout;
        private TableLayoutPanel logTableLayout;
        private Label fileListLabel;
        private TreeView fileTreeView;
        private Button refreshButton;
        private TableLayoutPanel targetPanel;
        private Label targetFolderLabel;
        private TextBox targetFolderTextBox;
        private Button browseButton;
        private TableLayoutPanel encryptionPanel;
        private Label encryptionLabel;
        private ComboBox encryptionComboBox;
        private Label passwordLabel;
        private TextBox passwordTextBox;
        private Button convertButton;
        private RichTextBox logTextBox;
        private ProgressBar progressBar;
    }
}