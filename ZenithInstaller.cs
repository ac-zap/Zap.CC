using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ZenithClient.Installer
{
    public class InstallerForm : Form
    {
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblPath;
        private TextBox txtPath;
        private Button btnBrowse;
        private CheckBox chkDesktop;
        private CheckBox chkStartMenu;
        private CheckBox chkLaunch;
        private ProgressBar progressBar;
        private Label lblStatus;
        private Button btnAction;
        private Button btnClose;
        private Panel headerPanel;
        private Panel contentPanel;
        private PictureBox picLogo;
        private bool isFinished = false;

        private readonly Color bgDark = Color.FromArgb(6, 11, 31);       // #060B1F
        private readonly Color panelBg = Color.FromArgb(10, 17, 40);      // #0A1128
        private readonly Color cardBorder = Color.FromArgb(30, 64, 175);   // #1E40AF
        private readonly Color accentBlue = Color.FromArgb(56, 189, 248);  // #38BDF8
        private readonly Color textWhite = Color.FromArgb(241, 245, 249);  // #F1F5F9
        private readonly Color textMuted = Color.FromArgb(186, 230, 253);  // #BAE6FD

        public InstallerForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Zenith Client - Assistant d'installation";
            this.Size = new Size(580, 440);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = bgDark;
            this.ForeColor = textWhite;
            this.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);

            // Drag support
            this.MouseDown += (s, e) => {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            };

            // Header
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 85,
                BackColor = panelBg
            };
            headerPanel.MouseDown += (s, e) => {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            };

            picLogo = new PictureBox
            {
                Size = new Size(52, 52),
                Location = new Point(22, 16),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            try {
                Assembly asm = Assembly.GetExecutingAssembly();
                using (Stream imgStream = asm.GetManifestResourceStream("logo.png"))
                {
                    if (imgStream != null) {
                        picLogo.Image = Image.FromStream(imgStream);
                    } else {
                        using (Stream icoStream = asm.GetManifestResourceStream("icon.ico"))
                        {
                            if (icoStream != null) {
                                Icon appIcon = new Icon(icoStream, 48, 48);
                                picLogo.Image = appIcon.ToBitmap();
                                this.Icon = appIcon;
                            }
                        }
                    }
                }
                using (Stream icoStream = asm.GetManifestResourceStream("icon.ico"))
                {
                    if (icoStream != null) {
                        this.Icon = new Icon(icoStream);
                    }
                }
            } catch { }

            lblTitle = new Label
            {
                Text = "ZENITH CLIENT",
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = textWhite,
                Location = new Point(84, 16),
                AutoSize = true
            };

            lblSubtitle = new Label
            {
                Text = "Le client Minecraft nouvelle génération — Performance • Précision • Puissance",
                Font = new Font("Segoe UI", 9.0f, FontStyle.Regular),
                ForeColor = accentBlue,
                Location = new Point(86, 48),
                AutoSize = true
            };

            btnClose = new Button
            {
                Text = "✕",
                Size = new Size(32, 32),
                Location = new Point(538, 12),
                FlatStyle = FlatStyle.Flat,
                ForeColor = textMuted,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 68, 68);
            btnClose.Click += (s, e) => Application.Exit();

            headerPanel.Controls.Add(picLogo);
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblSubtitle);
            headerPanel.Controls.Add(btnClose);

            // Content Panel
            contentPanel = new Panel
            {
                Location = new Point(24, 105),
                Size = new Size(532, 310),
                BackColor = bgDark
            };

            lblPath = new Label
            {
                Text = "Dossier d'installation :",
                Location = new Point(0, 5),
                ForeColor = textMuted,
                AutoSize = true
            };

            string defaultPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Programs",
                "ZenithClient"
            );

            txtPath = new TextBox
            {
                Text = defaultPath,
                Location = new Point(0, 30),
                Size = new Size(420, 26),
                BackColor = panelBg,
                ForeColor = textWhite,
                BorderStyle = BorderStyle.FixedSingle
            };

            btnBrowse = new Button
            {
                Text = "Parcourir...",
                Location = new Point(430, 28),
                Size = new Size(102, 28),
                FlatStyle = FlatStyle.Flat,
                BackColor = panelBg,
                ForeColor = textWhite,
                Cursor = Cursors.Hand
            };
            btnBrowse.FlatAppearance.BorderColor = cardBorder;
            btnBrowse.Click += (s, e) => {
                using (FolderBrowserDialog dlg = new FolderBrowserDialog())
                {
                    dlg.SelectedPath = txtPath.Text;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        txtPath.Text = Path.Combine(dlg.SelectedPath, "ZenithClient");
                    }
                }
            };

            chkDesktop = new CheckBox
            {
                Text = "Créer un raccourci sur le Bureau",
                Checked = true,
                Location = new Point(5, 75),
                Size = new Size(300, 24),
                ForeColor = textWhite
            };

            chkStartMenu = new CheckBox
            {
                Text = "Ajouter au menu Démarrer",
                Checked = true,
                Location = new Point(5, 105),
                Size = new Size(300, 24),
                ForeColor = textWhite
            };

            chkLaunch = new CheckBox
            {
                Text = "Lancer Zenith Client après l'installation",
                Checked = true,
                Location = new Point(5, 135),
                Size = new Size(300, 24),
                ForeColor = textWhite
            };

            progressBar = new ProgressBar
            {
                Location = new Point(0, 185),
                Size = new Size(532, 14),
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                Visible = false
            };

            lblStatus = new Label
            {
                Text = "Cliquez sur « Installer maintenant » pour installer Zenith Client.",
                Location = new Point(0, 210),
                Size = new Size(532, 22),
                ForeColor = textMuted
            };

            btnAction = new Button
            {
                Text = "Installer maintenant",
                Location = new Point(362, 250),
                Size = new Size(170, 42),
                FlatStyle = FlatStyle.Flat,
                BackColor = cardBorder,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAction.FlatAppearance.BorderSize = 0;
            btnAction.Click += BtnAction_Click;

            contentPanel.Controls.Add(lblPath);
            contentPanel.Controls.Add(txtPath);
            contentPanel.Controls.Add(btnBrowse);
            contentPanel.Controls.Add(chkDesktop);
            contentPanel.Controls.Add(chkStartMenu);
            contentPanel.Controls.Add(chkLaunch);
            contentPanel.Controls.Add(progressBar);
            contentPanel.Controls.Add(lblStatus);
            contentPanel.Controls.Add(btnAction);

            this.Controls.Add(contentPanel);
            this.Controls.Add(headerPanel);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen p = new Pen(cardBorder, 1.5f))
            {
                e.Graphics.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
            }
        }

        private void BtnAction_Click(object sender, EventArgs e)
        {
            if (isFinished)
            {
                string targetDir = txtPath.Text;
                string exePath = Path.Combine(targetDir, "ZenithClient.exe");
                if (chkLaunch.Checked && File.Exists(exePath))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(exePath) { WorkingDirectory = targetDir });
                }
                Application.Exit();
                return;
            }

            btnAction.Enabled = false;
            btnBrowse.Enabled = false;
            txtPath.Enabled = false;
            chkDesktop.Enabled = false;
            chkStartMenu.Enabled = false;
            progressBar.Visible = true;
            progressBar.Value = 10;
            lblStatus.Text = "Préparation de l'installation...";

            Thread installThread = new Thread(DoInstall);
            installThread.IsBackground = true;
            installThread.Start();
        }

        private void DoInstall()
        {
            try
            {
                string targetDir = "";
                bool makeDesktop = true;
                bool makeStartMenu = true;
                bool launch = true;

                this.Invoke(new Action(() => {
                    targetDir = txtPath.Text;
                    makeDesktop = chkDesktop.Checked;
                    makeStartMenu = chkStartMenu.Checked;
                    launch = chkLaunch.Checked;
                }));

                InstallerCore.ExecuteInstall(targetDir, makeDesktop, makeStartMenu, (msg, pct) => {
                    UpdateStatus(msg, pct);
                });

                this.Invoke(new Action(() => {
                    isFinished = true;
                    btnAction.Text = "Lancer Zenith Client";
                    btnAction.Enabled = true;
                    btnAction.BackColor = Color.FromArgb(56, 189, 248); // Sky blue
                    btnAction.ForeColor = Color.FromArgb(6, 11, 31);
                    lblStatus.ForeColor = Color.FromArgb(56, 189, 248);
                    lblStatus.Text = "Installation réussie ! Zenith Client est prêt à l'emploi.";
                }));
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() => {
                    lblStatus.ForeColor = Color.FromArgb(239, 68, 68);
                    lblStatus.Text = "Erreur : " + ex.Message;
                    btnAction.Enabled = true;
                    btnAction.Text = "Réessayer";
                }));
            }
        }

        private void UpdateStatus(string message, int progress)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateStatus(message, progress)));
                return;
            }
            lblStatus.Text = message;
            progressBar.Value = Math.Min(100, Math.Max(0, progress));
        }

        // Win32 drag helper
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
    }

    public static class InstallerCore
    {
        public static void KillRunningProcesses()
        {
            string[] procNames = new string[] {
                "ZenithClient", "ZenithClient_app", "zenithclient_app",
                "ZapClient", "ZapClient_app", "zapclient_app",
                "OneClient", "oneclient_app"
            };
            foreach (string name in procNames)
            {
                try
                {
                    foreach (var p in System.Diagnostics.Process.GetProcessesByName(name))
                    {
                        try { p.Kill(); p.WaitForExit(3000); } catch { }
                    }
                }
                catch { }
            }
        }

        public static void ExecuteInstall(string targetDir, bool makeDesktop, bool makeStartMenu, Action<string, int> progressCallback)
        {
            progressCallback("Arrêt des instances existantes...", 15);
            KillRunningProcesses();

            progressCallback("Création du répertoire cible...", 20);
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            progressCallback("Extraction des fichiers de Zenith Client...", 35);
            Assembly asm = Assembly.GetExecutingAssembly();
            using (Stream resStream = asm.GetManifestResourceStream("payload.zip"))
            {
                if (resStream == null)
                {
                    throw new Exception("Archive payload.zip introuvable dans les ressources.");
                }
                using (ZipArchive archive = new ZipArchive(resStream))
                {
                    int count = archive.Entries.Count;
                    int current = 0;
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        string destPath = Path.Combine(targetDir, entry.FullName);
                        if (string.IsNullOrEmpty(entry.Name))
                        {
                            Directory.CreateDirectory(destPath);
                        }
                        else
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(destPath));
                            entry.ExtractToFile(destPath, true);
                        }
                        current++;
                        int pct = 35 + (int)((current / (float)count) * 45);
                        progressCallback("Extraction : " + entry.Name, pct);
                    }
                }
            }

            string mainExe = Path.Combine(targetDir, "ZenithClient.exe");
            string appExe = Path.Combine(targetDir, "ZenithClient_app.exe");
            if (File.Exists(mainExe) && !File.Exists(appExe))
            {
                File.Copy(mainExe, appExe, true);
            }
            else if (File.Exists(appExe) && !File.Exists(mainExe))
            {
                File.Copy(appExe, mainExe, true);
            }

            // Extract icon.ico
            string destIcon = Path.Combine(targetDir, "icon.ico");
            using (Stream icoStream = asm.GetManifestResourceStream("icon.ico"))
            {
                if (icoStream != null)
                {
                    using (FileStream fs = new FileStream(destIcon, FileMode.Create, FileAccess.Write))
                    {
                        icoStream.CopyTo(fs);
                    }
                }
            }

            // Copy self as uninstaller
            string uninstallerExe = Path.Combine(targetDir, "Uninstall.exe");
            try
            {
                string currentExe = Assembly.GetExecutingAssembly().Location;
                if (File.Exists(currentExe))
                {
                    File.Copy(currentExe, uninstallerExe, true);
                }
            }
            catch { }

            progressCallback("Création des raccourcis...", 85);
            if (makeDesktop)
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string shortcutPath = Path.Combine(desktopPath, "Zenith Client.lnk");
                CreateShortcut(shortcutPath, mainExe, targetDir, destIcon, "Zenith Client - Performance • Precision • Power");
            }

            if (makeStartMenu)
            {
                string programsPath = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
                string menuFolder = Path.Combine(programsPath, "Zenith Client");
                Directory.CreateDirectory(menuFolder);
                string shortcutPath = Path.Combine(menuFolder, "Zenith Client.lnk");
                CreateShortcut(shortcutPath, mainExe, targetDir, destIcon, "Zenith Client - Performance • Precision • Power");
            }

            progressCallback("Enregistrement dans le Registre Windows...", 95);
            RegisterUninstall(targetDir, mainExe, destIcon, uninstallerExe);
            RegisterProtocol(mainExe);

            progressCallback("Installation terminée avec succès !", 100);
        }

        public static void CreateShortcut(string shortcutPath, string targetExe, string workDir, string iconPath, string desc)
        {
            try
            {
                Type shellType = Type.GetTypeFromProgID("WScript.Shell");
                dynamic shell = Activator.CreateInstance(shellType);
                dynamic shortcut = shell.CreateShortcut(shortcutPath);
                shortcut.TargetPath = targetExe;
                shortcut.WorkingDirectory = workDir;
                shortcut.Description = desc;
                if (File.Exists(iconPath))
                {
                    shortcut.IconLocation = iconPath + ",0";
                }
                shortcut.Save();
            }
            catch { }
        }

        public static void RegisterUninstall(string targetDir, string exePath, string iconPath, string uninstallerPath)
        {
            try
            {
                // Delete legacy uninstall keys
                try
                {
                    Registry.CurrentUser.DeleteSubKeyTree(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\ZapClient", false);
                    Registry.CurrentUser.DeleteSubKeyTree(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\OneClient", false);
                    string oldMenuZap = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "Zap Client");
                    if (Directory.Exists(oldMenuZap)) Directory.Delete(oldMenuZap, true);
                    string oldMenuOne = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "OneClient");
                    if (Directory.Exists(oldMenuOne)) Directory.Delete(oldMenuOne, true);
                }
                catch { }

                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\ZenithClient"))
                {
                    if (key != null)
                    {
                        key.SetValue("DisplayName", "Zenith Client");
                        key.SetValue("DisplayVersion", "2.10.1");
                        key.SetValue("Publisher", "ZenithClient");
                        key.SetValue("InstallLocation", targetDir);
                        key.SetValue("DisplayIcon", iconPath);
                        key.SetValue("UninstallString", "\"" + (File.Exists(uninstallerPath) ? uninstallerPath : exePath) + "\" /uninstall");
                        key.SetValue("NoModify", 1, RegistryValueKind.DWord);
                        key.SetValue("NoRepair", 1, RegistryValueKind.DWord);
                    }
                }
            }
            catch { }
        }

        public static void RegisterProtocol(string exePath)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Classes\zenithclient"))
                {
                    if (key != null)
                    {
                        key.SetValue("", "URL:ZenithClient Protocol");
                        key.SetValue("URL Protocol", "");
                        using (RegistryKey cmdKey = key.CreateSubKey(@"shell\open\command"))
                        {
                            if (cmdKey != null)
                            {
                                cmdKey.SetValue("", "\"" + exePath + "\" \"%1\"");
                            }
                        }
                    }
                }
            }
            catch { }
        }

        public static void ExecuteUninstall()
        {
            try
            {
                KillRunningProcesses();

                // Delete desktop shortcut
                string desktopShortcut = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Zenith Client.lnk");
                if (File.Exists(desktopShortcut)) File.Delete(desktopShortcut);

                // Delete start menu folder
                string menuFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "Zenith Client");
                if (Directory.Exists(menuFolder)) Directory.Delete(menuFolder, true);

                // Delete registry keys
                try { Registry.CurrentUser.DeleteSubKeyTree(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\ZenithClient", false); } catch { }
                try { Registry.CurrentUser.DeleteSubKeyTree(@"Software\Classes\zenithclient", false); } catch { }

                // Schedule install dir removal
                string installDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (Directory.Exists(installDir) && installDir.EndsWith("ZenithClient", StringComparison.OrdinalIgnoreCase))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = "/C timeout /T 2 /NOBREAK > NUL & rmdir /S /Q \"" + installDir + "\"",
                        WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                        CreateNoWindow = true
                    });
                }

                MessageBox.Show("Zenith Client a été désinstallé avec succès de votre ordinateur.", "Désinstallation terminée", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la désinstallation : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args != null && args.Length > 0)
            {
                string first = args[0].ToLowerInvariant();
                if (first == "/uninstall" || first == "-uninstall" || first == "/u")
                {
                    DialogResult res = MessageBox.Show(
                        "Êtes-vous sûr de vouloir désinstaller Zenith Client de votre ordinateur ?",
                        "Désinstallation de Zenith Client",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );
                    if (res == DialogResult.Yes)
                    {
                        InstallerCore.ExecuteUninstall();
                    }
                    return;
                }
                else if (first == "/s" || first == "/silent")
                {
                    string target = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "Programs",
                        "ZenithClient"
                    );
                    InstallerCore.ExecuteInstall(target, true, true, (m, p) => { });
                    return;
                }
            }

            Application.Run(new InstallerForm());
        }
    }
}
