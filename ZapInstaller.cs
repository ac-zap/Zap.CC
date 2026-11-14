using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ZapClient.Installer
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

        private readonly Color bgDark = Color.FromArgb(11, 15, 23);
        private readonly Color panelBg = Color.FromArgb(17, 24, 39);
        private readonly Color cardBorder = Color.FromArgb(34, 45, 66);
        private readonly Color accentBlue = Color.FromArgb(56, 92, 255);
        private readonly Color textWhite = Color.FromArgb(240, 244, 255);
        private readonly Color textMuted = Color.FromArgb(148, 163, 184);

        public InstallerForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Zap Client - Assistant d'installation";
            this.Size = new Size(580, 440);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = bgDark;
            this.ForeColor = textWhite;
            this.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);

            // Enable drag on borderless form
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
                Size = new Size(48, 48),
                Location = new Point(24, 18),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            try {
                using (Stream icoStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("icon.ico"))
                {
                    if (icoStream != null) {
                        Icon appIcon = new Icon(icoStream, 48, 48);
                        picLogo.Image = appIcon.ToBitmap();
                        this.Icon = appIcon;
                    }
                }
            } catch { }

            lblTitle = new Label
            {
                Text = "ZAP CLIENT",
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = textWhite,
                Location = new Point(84, 16),
                AutoSize = true
            };

            lblSubtitle = new Label
            {
                Text = "Le client Minecraft nouvelle génération",
                Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
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
                "ZapClient"
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
                        txtPath.Text = Path.Combine(dlg.SelectedPath, "ZapClient");
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
                Text = "Lancer Zap Client après l'installation",
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
                Text = "Cliquez sur Installer pour démarrer l'installation.",
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
                BackColor = accentBlue,
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
                string exePath = Path.Combine(targetDir, "ZapClient.exe");
                if (chkLaunch.Checked && File.Exists(exePath))
                {
                    System.Diagnostics.Process.Start(exePath);
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
                this.Invoke(new Action(() => { targetDir = txtPath.Text; }));

                UpdateStatus("Arrêt des instances existantes...", 15);
                string[] procNames = new string[] { "ZapClient", "ZapClient_app", "zapclient_app", "OneClient", "oneclient_app" };
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

                UpdateStatus("Création du répertoire cible...", 20);
                if (!Directory.Exists(targetDir))
                {
                    Directory.CreateDirectory(targetDir);
                }

                UpdateStatus("Extraction des fichiers de Zap Client...", 40);
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
                            int pct = 40 + (int)((current / (float)count) * 40);
                            UpdateStatus("Extraction : " + entry.Name, pct);
                        }
                    }
                }

                // Ensure both ZapClient.exe and ZapClient_app.exe exist and match the freshest binary
                string mainExe = Path.Combine(targetDir, "ZapClient.exe");
                string appExeCaps = Path.Combine(targetDir, "ZapClient_app.exe");
                string appExeLower = Path.Combine(targetDir, "zapclient_app.exe");

                if (File.Exists(appExeCaps))
                {
                    File.Copy(appExeCaps, mainExe, true);
                }
                else if (File.Exists(appExeLower))
                {
                    File.Copy(appExeLower, mainExe, true);
                }
                else if (File.Exists(mainExe))
                {
                    File.Copy(mainExe, appExeCaps, true);
                }

                // Extract embedded icon to destination
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

                UpdateStatus("Création des raccourcis...", 85);
                bool makeDesktop = true;
                bool makeStartMenu = true;
                this.Invoke(new Action(() => {
                    makeDesktop = chkDesktop.Checked;
                    makeStartMenu = chkStartMenu.Checked;
                }));

                if (makeDesktop)
                {
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string shortcutPath = Path.Combine(desktopPath, "Zap Client.lnk");
                    CreateShortcut(shortcutPath, mainExe, targetDir, destIcon, "Zap Client - Next-generation Minecraft Launcher");
                }

                if (makeStartMenu)
                {
                    string programsPath = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
                    string menuFolder = Path.Combine(programsPath, "Zap Client");
                    Directory.CreateDirectory(menuFolder);
                    string shortcutPath = Path.Combine(menuFolder, "Zap Client.lnk");
                    CreateShortcut(shortcutPath, mainExe, targetDir, destIcon, "Zap Client - Next-generation Minecraft Launcher");
                }

                UpdateStatus("Enregistrement dans le Registre Windows...", 95);
                RegisterUninstall(targetDir, mainExe, destIcon);
                RegisterProtocol(mainExe);

                UpdateStatus("Installation terminée avec succès !", 100);

                this.Invoke(new Action(() => {
                    isFinished = true;
                    btnAction.Text = "Lancer Zap Client";
                    btnAction.Enabled = true;
                    btnAction.BackColor = Color.FromArgb(34, 197, 94); // Green success
                    lblStatus.ForeColor = Color.FromArgb(34, 197, 94);
                    lblStatus.Text = "Installation réussie ! Zap Client est prêt à l'emploi.";
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

        private void CreateShortcut(string shortcutPath, string targetExe, string workDir, string iconPath, string desc)
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

        private void RegisterUninstall(string targetDir, string exePath, string iconPath)
        {
            try
            {
                // Clean up any legacy OneClient registry keys or menu folders
                try
                {
                    Registry.CurrentUser.DeleteSubKeyTree(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\OneClient", false);
                    Registry.CurrentUser.DeleteSubKeyTree(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\oneclient", false);
                    string oldMenu = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), "OneClient");
                    if (Directory.Exists(oldMenu)) Directory.Delete(oldMenu, true);
                }
                catch { }

                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\ZapClient"))
                {
                    if (key != null)
                    {
                        key.SetValue("DisplayName", "Zap Client");
                        key.SetValue("DisplayVersion", "2.10.1");
                        key.SetValue("Publisher", "Zap.cc");
                        key.SetValue("InstallLocation", targetDir);
                        key.SetValue("DisplayIcon", iconPath);
                        key.SetValue("NoModify", 1, RegistryValueKind.DWord);
                        key.SetValue("NoRepair", 1, RegistryValueKind.DWord);
                    }
                }
            }
            catch { }
        }

        private void RegisterProtocol(string exePath)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Classes\zapclient"))
                {
                    if (key != null)
                    {
                        key.SetValue("", "URL:ZapClient Protocol");
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

        // Win32 drag helper
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new InstallerForm());
        }
    }
}
