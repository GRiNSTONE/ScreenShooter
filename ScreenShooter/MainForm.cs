using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShooter
{
    public partial class MainForm : Form
    {
        KeyboardHook keyboardHook;// = new KeyboardHook();

        public MainForm()
        {
            InitializeComponent();

            //keyboardHook.OneKeyPressed += new EventHandler<OneKeyPressedEventArgs>(KeyPressedMethod);

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            ConfigureSettingHotKey();
            ProfileManager.Load();
            ToggleProfile();
        }

        private void ToggleProfile()
        {
            lblNameOfProfile.Text = ProfileManager.ActiveProfile.NameProfile;
            chbDivideToMoths.Checked = ProfileManager.ActiveProfile.IsDivideToMonths;
            chbEnableSound.Checked = ProfileManager.ActiveProfile.IsSound;
            tbScreenShotPath.Text = ProfileManager.ActiveProfile.RootPathFolder;
            this.Text = "Screen Shooter - "+ ProfileManager.ActiveProfile.NameProfile;
            tltbActiveProfileName.Text = ProfileManager.ActiveProfile.NameProfile;
        }

        /// <summary>
        /// Настройка горичей клавиши в зависимости от флага
        /// </summary>
        private void ConfigureSettingHotKey()
        {
            if(cheEnableScreenShot.Checked)
            {
                RegisterHotKey();
            }
            else
            {
                UnRegisterHotKey();
            }
        }

        /// <summary>
        /// Регистрация горячей клавиши
        /// </summary>
        private void RegisterHotKey()
        {
            keyboardHook = new KeyboardHook();
            keyboardHook.OneKeyPressed += new EventHandler<OneKeyPressedEventArgs>(KeyPressedMethod);
            keyboardHook.RegisterHotKey(Keys.PrintScreen);
        }

        /// <summary>
        /// Отключение горячей клавиши
        /// </summary>
        private void UnRegisterHotKey()
        {
            keyboardHook.Dispose();
        }

        /// <summary>
        /// Главный метод создания скриншота
        /// </summary>
        private void CreateScreenShot()
        {
            string addMonthsFolder = "";
            if (ProfileManager.ActiveProfile.IsDivideToMonths)
            {
                addMonthsFolder = "\\" + DateTime.Now.ToString("yyyyMM");
                if (!Directory.Exists($"{ProfileManager.ActiveProfile.RootPathFolder}{addMonthsFolder}"))
                {
                    Directory.CreateDirectory($"{ProfileManager.ActiveProfile.RootPathFolder}{addMonthsFolder}");
                }
            }

            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                    Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            int cnt = 0;
            DirectoryInfo directoryInfo = new DirectoryInfo(ProfileManager.ActiveProfile.RootPathFolder + addMonthsFolder);
            int fileNameLength = GetFileName().Length;
            foreach (var file in directoryInfo.GetFiles("*.png"))
            {
                if (file.Name.Length == fileNameLength + 8)
                {
                    try
                    {
                        int number = Convert.ToInt32(file.Name.Substring(fileNameLength, 4));
                        if (number > cnt)
                            cnt = number;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            string cntString = (++cnt).ToString("0000");
            Clipboard.SetImage(bitmap);
            string fileName = $"{GetFileName()}{cntString}.png";
            

            bitmap.Save($"{ProfileManager.ActiveProfile.RootPathFolder}{addMonthsFolder}\\{fileName}", ImageFormat.Png);
            if(ProfileManager.ActiveProfile.IsSound)
            {
                PlaySound();
            }
            Log($"[{ProfileManager.ActiveProfile.NameProfile}] {fileName} Saved");
        }

        private async void PlaySound()
        {
            await Task.Run(() => {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer($"{Application.StartupPath}\\short_click.wav");
                player.Play();
            });
        }

        /// <summary>
        /// Формирование имени файла
        /// </summary>
        /// <returns></returns>
        private string GetFileName()
        {
            return $"{ProfileManager.ActiveProfile.NameProfile}" + DateTime.Now.ToString("yyyyMMdd");
        }

        private void KeyPressedMethod(object sender, OneKeyPressedEventArgs e)
        {
            Log("CreateScreenShot");
            CreateScreenShot();
        }

        /// <summary>
        /// Запись в Лог
        /// </summary>
        /// <param name="message"></param>
        private void Log(string message)
        {
            string dateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            lbLog.Items.Add($"> {dateTime} {message}");
        }

        private void btnProfileList_Click(object sender, EventArgs e)
        {
            ProfileList dlg = new ProfileList();
            dlg.ShowDialog();
            ToggleProfile();
        }

        private void tbScreenShotPath_MouseClick(object sender, MouseEventArgs e)
        {
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                ProfileManager.ActiveProfile.RootPathFolder = folderBrowserDialog.SelectedPath;
                ProfileManager.Save();
                ToggleProfile();
            }
        }

        private void chbDivideToMoths_MouseUp(object sender, MouseEventArgs e)
        {
            ProfileManager.ActiveProfile.IsDivideToMonths = chbDivideToMoths.Checked;
            ProfileManager.Save();
            ToggleProfile();
        }

        private void chbEnableSound_MouseUp(object sender, MouseEventArgs e)
        {
            ProfileManager.ActiveProfile.IsSound = chbEnableSound.Checked;
            ProfileManager.Save();
            ToggleProfile();
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", ProfileManager.ActiveProfile.RootPathFolder);
        }

        private void cheEnableScreenShot_CheckedChanged(object sender, EventArgs e)
        {
            ConfigureSettingHotKey();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void tlbtnClose_Click(object sender, EventArgs e)
        {
            UnRegisterHotKey();
            this.Close();
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
            }
        }
    }
}
