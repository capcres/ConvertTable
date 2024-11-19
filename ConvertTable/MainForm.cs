using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConvertTable
{
    public partial class MainForm : Form
    {
        private const string VERSION = "1.0.1";
        private const string TITLE = "Convert Table";

        public class ConvertSettings
        {
            public List<string> CheckedPaths { get; set; } = new List<string>();
            public List<string> ExpandedPaths { get; set; } = new List<string>(); // 확장된 노드 경로 추가
            public string TargetFolder { get; set; }
            public string EncryptionMethod { get; set; }
            public string Password { get; set; }
            public int WindowWidth { get; set; }
            public int WindowHeight { get; set; }
            public int WindowX { get; set; }
            public int WindowY { get; set; }
            public FormWindowState WindowState { get; set; }
        }

        private Dictionary<string, string> filePathMap;
        private bool isUpdatingCheckState = false;
        private readonly string baseTablePath;

        private readonly string settingsPath = Path.Combine(Application.StartupPath, "settings.json");
        private bool isUserScrolled = false;

        public MainForm()
        {
            InitializeComponent();
            filePathMap = new Dictionary<string, string>();
            baseTablePath = Path.Combine(Application.StartupPath, "Table");

            UpdateFormTitle();
            InitializeEvents();
            LoadExcelFiles();
        }

        private void UpdateFormTitle()
        {
            this.Text = $"{TITLE} v{VERSION}";
        }

        private void InitializeEvents()
        {
            // TreeView Events
            fileTreeView.AfterCheck += FileTreeView_AfterCheck;
            fileTreeView.BeforeSelect += FileTreeView_BeforeSelect;

            // Button Events
            refreshButton.Click += RefreshButton_Click;
            browseButton.Click += BrowseButton_Click;
            convertButton.Click += ConvertButton_Click;

            // Form Events
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;

            logTextBox.VScroll += LogTextBox_VScroll;
            logTextBox.TextChanged += LogTextBox_TextChanged;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // ComboBox 초기 선택
            encryptionComboBox.SelectedIndex = 0;
            LoadSettings();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void FileTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            // 노드 선택을 취소
            e.Cancel = true;
        }

        private void FileTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (isUpdatingCheckState) return;

            try
            {
                isUpdatingCheckState = true;

                // 체크 상태 변경된 노드에 대해 자식과 부모 노드 상태 업데이트
                UpdateChildNodes(e.Node);
                UpdateParentNodes(e.Node);
            }
            finally
            {
                isUpdatingCheckState = false;
            }
        }

        private void UpdateChildNodes(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = node.Checked;
                UpdateChildNodes(child);
            }
        }

        private void UpdateParentNodes(TreeNode node)
        {
            TreeNode parent = node.Parent;
            if (parent != null)
            {
                bool allChecked = true;
                bool anyChecked = false;

                foreach (TreeNode sibling in parent.Nodes)
                {
                    if (sibling.Checked)
                    {
                        anyChecked = true;
                    }
                    else
                    {
                        allChecked = false;
                    }

                    if (!allChecked && anyChecked) break;
                }

                parent.Checked = allChecked;
                UpdateParentNodes(parent);
            }
        }

        private void LoadSettings()
        {
            try
            {
                if (File.Exists(settingsPath))
                {
                    string jsonString = File.ReadAllText(settingsPath);
                    var settings = System.Text.Json.JsonSerializer.Deserialize<ConvertSettings>(jsonString);

                    if (settings != null)
                    {
                        // 기존 설정들 복원
                        targetFolderTextBox.Text = settings.TargetFolder;
                        encryptionComboBox.Text = settings.EncryptionMethod;
                        passwordTextBox.Text = settings.Password;

                        // 창 크기와 위치 복원
                        if (settings.WindowWidth > 0 && settings.WindowHeight > 0)
                        {
                            this.Width = settings.WindowWidth;
                            this.Height = settings.WindowHeight;
                        }

                        bool isOnScreen = false;
                        foreach (Screen screen in Screen.AllScreens)
                        {
                            if (screen.WorkingArea.Contains(settings.WindowX, settings.WindowY))
                            {
                                isOnScreen = true;
                                break;
                            }
                        }

                        if (isOnScreen)
                        {
                            this.Location = new Point(settings.WindowX, settings.WindowY);
                        }

                        this.WindowState = settings.WindowState;

                        // 트리뷰 상태 복원 (상대 경로 사용)
                        RestoreTreeState(fileTreeView.Nodes, settings.CheckedPaths, settings.ExpandedPaths);
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage($"설정 로드 중 오류 발생: {ex.Message}");
            }
        }

        private void RestoreCheckedNodes(TreeNodeCollection nodes, List<string> checkedPaths)
        {
            foreach (TreeNode node in nodes)
            {
                string nodePath = node.Tag?.ToString() ?? "";
                if (checkedPaths.Contains(nodePath))
                {
                    node.Checked = true;
                }
                RestoreCheckedNodes(node.Nodes, checkedPaths);
            }
        }

        private void SaveSettings()
        {
            try
            {
                var settings = new ConvertSettings
                {
                    CheckedPaths = new List<string>(),
                    ExpandedPaths = new List<string>(),
                    TargetFolder = targetFolderTextBox.Text,
                    EncryptionMethod = encryptionComboBox.Text,
                    Password = passwordTextBox.Text,
                    WindowWidth = this.WindowState == FormWindowState.Normal ? this.Width : this.RestoreBounds.Width,
                    WindowHeight = this.WindowState == FormWindowState.Normal ? this.Height : this.RestoreBounds.Height,
                    WindowX = this.WindowState == FormWindowState.Normal ? this.Location.X : this.RestoreBounds.X,
                    WindowY = this.WindowState == FormWindowState.Normal ? this.Location.Y : this.RestoreBounds.Y,
                    WindowState = this.WindowState
                };

                SaveTreeState(fileTreeView.Nodes, settings.CheckedPaths, settings.ExpandedPaths);

                string jsonString = System.Text.Json.JsonSerializer.Serialize(settings, new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(settingsPath, jsonString);
            }
            catch (Exception ex)
            {
                LogMessage($"설정 저장 중 오류 발생: {ex.Message}");
            }
        }

        private string GetRelativePath(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath)) return string.Empty;

            try
            {
                return Path.GetRelativePath(baseTablePath, absolutePath);
            }
            catch
            {
                return string.Empty;
            }
        }

        // 상대 경로를 절대 경로로 변환
        private string GetAbsolutePath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return string.Empty;

            try
            {
                return Path.GetFullPath(Path.Combine(baseTablePath, relativePath));
            }
            catch
            {
                return string.Empty;
            }
        }

        private void SaveTreeState(TreeNodeCollection nodes, List<string> checkedPaths, List<string> expandedPaths)
        {
            foreach (TreeNode node in nodes)
            {
                string absolutePath = node.Tag?.ToString() ?? "";
                string relativePath = GetRelativePath(absolutePath);

                if (node.Checked && !string.IsNullOrEmpty(relativePath))
                {
                    checkedPaths.Add(relativePath);
                }
                if (node.IsExpanded && !string.IsNullOrEmpty(relativePath))
                {
                    expandedPaths.Add(relativePath);
                }
                SaveTreeState(node.Nodes, checkedPaths, expandedPaths);
            }
        }

        private void RestoreTreeState(TreeNodeCollection nodes, List<string> checkedPaths, List<string> expandedPaths)
        {
            foreach (TreeNode node in nodes)
            {
                string absolutePath = node.Tag?.ToString() ?? "";
                string relativePath = GetRelativePath(absolutePath);

                if (!string.IsNullOrEmpty(relativePath))
                {
                    if (checkedPaths.Contains(relativePath))
                    {
                        node.Checked = true;
                    }
                    if (expandedPaths.Contains(relativePath))
                    {
                        node.Expand();
                    }
                }
                RestoreTreeState(node.Nodes, checkedPaths, expandedPaths);
            }
        }

        private void CollectCheckedPaths(TreeNodeCollection nodes, List<string> checkedPaths)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    checkedPaths.Add(node.Tag?.ToString() ?? "");
                }
                CollectCheckedPaths(node.Nodes, checkedPaths);
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadExcelFiles();
            LogMessage("파일 목록을 새로고침했습니다.");
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    targetFolderTextBox.Text = folderDialog.SelectedPath;
                    LogMessage($"대상 폴더가 선택되었습니다: {folderDialog.SelectedPath}");
                }
            }
        }

        private async void ConvertButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(targetFolderTextBox.Text))
            {
                MessageBox.Show("대상 폴더를 선택해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("암호를 입력해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveSettings();

            List<string> checkedFiles = new List<string>();
            GetCheckedFiles(fileTreeView.Nodes, checkedFiles);

            if (checkedFiles.Count == 0)
            {
                MessageBox.Show("변환할 파일을 선택해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            convertButton.Enabled = false;
            progressBar.Visible = true;
            progressBar.Minimum = 0;
            progressBar.Maximum = checkedFiles.Count;
            progressBar.Value = 0;

            try
            {
                string baseTargetFolder = targetFolderTextBox.Text.TrimEnd('\\');
                string textTableBaseFolder = Path.Combine(Application.StartupPath, "TextTable");

                for (int i = 0; i < checkedFiles.Count; i++)
                {
                    string filePath = checkedFiles[i];
                    string fileName = Path.GetFileName(filePath);
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);

                    string sourceDir = Path.GetDirectoryName(filePath);
                    string relativeFolderPath = sourceDir.Replace(baseTablePath + "\\", "");

                    string targetFolder = Path.Combine(baseTargetFolder, relativeFolderPath).TrimEnd('\\') + "\\";
                    string textTableFolder = Path.Combine(textTableBaseFolder, relativeFolderPath).TrimEnd('\\') + "\\";

                    Directory.CreateDirectory(targetFolder);
                    Directory.CreateDirectory(textTableFolder);

                    LogMessage($"Source: {filePath}");
                    LogMessage($"Target: {targetFolder}");
                    LogMessage($"TextTable: {textTableFolder}");

                    // 각 인자를 배열로 전달
                    var processStartInfo = new ProcessStartInfo
                    {
                        FileName = "ConvertXLSX.exe",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    };

                    // 각 인자를 개별적으로 추가
                    processStartInfo.ArgumentList.Add(passwordTextBox.Text);
                    processStartInfo.ArgumentList.Add(encryptionComboBox.Text);
                    processStartInfo.ArgumentList.Add(sourceDir);
                    processStartInfo.ArgumentList.Add(fileNameWithoutExt);
                    processStartInfo.ArgumentList.Add(targetFolder);
                    processStartInfo.ArgumentList.Add(textTableFolder);

                    try
                    {
                        LogMessage($"파일 변환 시작 ({i + 1}/{checkedFiles.Count}): {relativeFolderPath}\\{fileName}");
                        string arguments = string.Join(" ", processStartInfo.ArgumentList.Select(arg => $"\"{arg}\""));
                        LogMessage($"실행 명령어: {processStartInfo.FileName} {arguments}");

                        using (Process process = Process.Start(processStartInfo))
                        {
                            string output = await process.StandardOutput.ReadToEndAsync();
                            await process.WaitForExitAsync();

                            LogMessage(output);
                            LogMessage($"변환 완료: {relativeFolderPath}\\{fileName}");
                        }

                        progressBar.Value = i + 1;
                        Application.DoEvents();
                    }
                    catch (Exception ex)
                    {
                        LogMessage($"오류 발생: {ex.Message}");
                    }
                }
            }
            finally
            {
                convertButton.Enabled = true;
                await Task.Delay(1000);
                progressBar.Visible = false;
            }
        }

        private void LoadExcelFiles()
        {
            try
            {
                fileTreeView.BeginUpdate();
                isUpdatingCheckState = true;

                fileTreeView.Nodes.Clear();
                filePathMap.Clear();
                string tableFolder = Path.Combine(Application.StartupPath, "Table");

                if (Directory.Exists(tableFolder))
                {
                    TreeNode rootNode = new TreeNode("Table")
                    {
                        Tag = tableFolder
                    };
                    fileTreeView.Nodes.Add(rootNode);
                    LoadDirectory(tableFolder, rootNode);

                    // 설정에서 확장 상태를 로드하므로 자동 확장은 제거
                    // rootNode.Expand();
                }
                else
                {
                    LogMessage("Table 폴더를 찾을 수 없습니다.");
                }
            }
            finally
            {
                isUpdatingCheckState = false;
                fileTreeView.EndUpdate();
            }
        }

        // 모든 노드를 펼치는 재귀 함수
        private void ExpandAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.Expand();
                if (node.Nodes.Count > 0)
                {
                    ExpandAllNodes(node.Nodes);
                }
            }
        }

        private void LoadDirectory(string directory, TreeNode parentNode)
        {
            // 하위 디렉토리 로드
            foreach (string dir in Directory.GetDirectories(directory))
            {
                TreeNode dirNode = new TreeNode(Path.GetFileName(dir))
                {
                    Tag = dir
                };
                parentNode.Nodes.Add(dirNode);
                LoadDirectory(dir, dirNode);
            }

            // Excel 파일 로드 - 임시 파일 제외
            foreach (string file in Directory.GetFiles(directory, "*.xlsx"))
            {
                string fileName = Path.GetFileName(file);

                // 임시 파일 제외 ("~$" 로 시작하는 파일)
                if (!fileName.StartsWith("~$"))
                {
                    TreeNode fileNode = new TreeNode(fileName)
                    {
                        Tag = file
                    };
                    parentNode.Nodes.Add(fileNode);
                    filePathMap[fileName] = file;
                }
            }
        }

        private void GetCheckedFiles(TreeNodeCollection nodes, List<string> checkedFiles)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked && node.Nodes.Count == 0) // 파일만 추가
                {
                    checkedFiles.Add(node.Tag.ToString());
                }
                GetCheckedFiles(node.Nodes, checkedFiles);
            }
        }

        private void LogTextBox_VScroll(object sender, EventArgs e)
        {
            // 현재 스크롤 위치 확인
            int visibleLines = logTextBox.ClientSize.Height / logTextBox.Font.Height;
            int totalLines = logTextBox.GetLineFromCharIndex(logTextBox.TextLength) + 1;
            int currentLine = logTextBox.GetLineFromCharIndex(logTextBox.GetCharIndexFromPosition(new Point(0, 0)));

            // 사용자가 스크롤을 최하단이 아닌 곳으로 이동했는지 확인
            isUserScrolled = (currentLine + visibleLines < totalLines);
        }

        private void LogTextBox_TextChanged(object sender, EventArgs e)
        {
            // 스크롤이 최하단에 있거나 사용자가 스크롤을 움직이지 않은 경우에만 자동 스크롤
            if (!isUserScrolled)
            {
                logTextBox.SelectionStart = logTextBox.TextLength;
                logTextBox.ScrollToCaret();
            }
        }

        private void LogMessage(string message)
        {
            if (logTextBox.InvokeRequired)
            {
                logTextBox.Invoke(new Action<string>(LogMessage), message);
                return;
            }

            logTextBox.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}");

            // 사용자가 스크롤을 움직이지 않은 경우에만 자동 스크롤
            if (!isUserScrolled)
            {
                logTextBox.SelectionStart = logTextBox.TextLength;
                logTextBox.ScrollToCaret();
            }
        }
    }
}