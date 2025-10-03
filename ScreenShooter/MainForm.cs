using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            ToggleHotKey();
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
            tbQuality.Value = (int)ProfileManager.ActiveProfile.ImageQuality;
            lblQuality.Text = tbQuality.Value.ToString();
            if (ProfileManager.ActiveProfile.ImageFormat == ImageFormat.Jpeg)
            {
                cbFormat.SelectedIndex = 1;
            }
            else
            {
                cbFormat.SelectedIndex = 0;
            }
            if(ProfileManager.ActiveProfile.RegionHeight > 0)
            {
                rbRegionSc.Checked = true;
                rbFullSc.Checked = false;
            }
            else
            {
                rbRegionSc.Checked = false;
                rbFullSc.Checked = true;
            }
        }

        /// <summary>
        /// Настройка горячей клавиши в зависимости от флага
        /// </summary>
        private void ToggleHotKey()
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
        private async void CreateScreenShot()
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
            int cnt = await GetMaxNumFromFilesNames(ProfileManager.ActiveProfile.RootPathFolder + addMonthsFolder);

            //Log("Bitmap creating...");

            int w;
            int h;
            int x;
            int y;
            if (ProfileManager.ActiveProfile.RegionWidth > 0)
            {
                w = ProfileManager.ActiveProfile.RegionWidth;
                h = ProfileManager.ActiveProfile.RegionHeight;
                x = ProfileManager.ActiveProfile.RegionX;
                y = ProfileManager.ActiveProfile.RegionY;
            }
            else
            {
                w = Screen.PrimaryScreen.Bounds.Width;
                h = Screen.PrimaryScreen.Bounds.Height;
                x = 0;
                y = 0;
            }
            Bitmap bitmap = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bitmap as Image);
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CopyFromScreen(x, y, 0, 0, bitmap.Size);
            Clipboard.SetImage(bitmap);

            string cntString = (++cnt).ToString("0000");
            string fileName = $"{GetFileName()}{cntString}.{ProfileManager.ActiveProfile.ImageFormat.ToString().ToLower()}";

            Log($"[{ProfileManager.ActiveProfile.NameProfile}] {fileName} Saving...");

            if(ProfileManager.ActiveProfile.ImageFormat == ImageFormat.Jpeg)
            {
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, ProfileManager.ActiveProfile.ImageQuality);
                ImageCodecInfo jpegCodec = GetEncoder(ImageFormat.Jpeg);
                bitmap.Save($"{ProfileManager.ActiveProfile.RootPathFolder}{addMonthsFolder}\\{fileName}", jpegCodec, encoderParameters);
                Log($"[{ProfileManager.ActiveProfile.NameProfile}] (Q:{ProfileManager.ActiveProfile.ImageQuality}) {fileName} Saved");
            }
            else
            {
                bitmap.Save($"{ProfileManager.ActiveProfile.RootPathFolder}{addMonthsFolder}\\{fileName}", ProfileManager.ActiveProfile.ImageFormat);
                Log($"[{ProfileManager.ActiveProfile.NameProfile}] {fileName} Saved");
            }
                
            
            
            
            if (ProfileManager.ActiveProfile.IsSound)
            {
                PlaySound();
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                    return codec;
            }
            return null;
        }

        private async Task<int> GetMaxNumFromFilesNames(string path)
        {
            int cnt = 0;
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            int fileNameLength = GetFileName().Length;
            await Task.Run(() => {
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
                foreach (var file in directoryInfo.GetFiles("*.jpeg"))
                {
                    if (file.Name.Length == fileNameLength + 9)
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
                foreach (var file in directoryInfo.GetFiles("*.jpg"))
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
            });
            return cnt;
        }

        private async void PlaySound()
        {
            await Task.Run(() => {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer($"{Application.StartupPath}\\short_click.wav");
                player.Play();
            });
        }

        /// <summary>
        /// Формирование имени файла без счетчика
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
            string dateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:fff");
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
            if (ProfileManager.ActiveProfile.IsDivideToMonths)
            {
                string path = ProfileManager.ActiveProfile.RootPathFolder;
                if(Directory.Exists(ProfileManager.ActiveProfile.RootPathFolder + "\\" + DateTime.Now.ToString("yyyyMM")))
                {
                    path += "\\" + DateTime.Now.ToString("yyyyMM");
                }
                System.Diagnostics.Process.Start("explorer.exe", path);
            }
            else
            {
                System.Diagnostics.Process.Start("explorer.exe", ProfileManager.ActiveProfile.RootPathFolder);
            }
        }

        private void cheEnableScreenShot_CheckedChanged(object sender, EventArgs e)
        {
            ToggleHotKey();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void tlbtnClose_Click(object sender, EventArgs e)
        {
            CloseProgram();
        }

        private void CloseProgram()
        {
            UnRegisterHotKey();
            this.Close();
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Minimized)
            //{
            //    this.ShowInTaskbar = false;
            //    this.Hide();
            //}
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left /*&& this.WindowState == FormWindowState.Minimized*/)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                this.Show();
            }
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Normal;
            //this.ShowInTaskbar = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
        }

        private void cbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFormat.SelectedIndex == 0)
            {
                ProfileManager.ActiveProfile.ImageFormat = ImageFormat.Png;
                tbQuality.Enabled = false;
            }
            else
            {
                ProfileManager.ActiveProfile.ImageFormat = ImageFormat.Jpeg;
                tbQuality.Enabled = true;
            }
            ProfileManager.Save();
            ToggleProfile();
        }

        private void tbQuality_Scroll(object sender, EventArgs e)
        {
            lblQuality.Text = tbQuality.Value.ToString();
        }

        private void tbQuality_MouseUp(object sender, MouseEventArgs e)
        {
            ProfileManager.ActiveProfile.ImageQuality = tbQuality.Value;
            ProfileManager.Save();
            ToggleProfile();
        }

        private async void CreateRegionScreenShot()
        {
            // Сворачиваем основное окно
            this.WindowState = FormWindowState.Minimized;

            // Даем время для сворачивания
            await Task.Delay(500);

            // Показываем селектор области
            using (var regionSelector = new RegionSelector())
            {
                if (regionSelector.ShowDialog() == DialogResult.OK && regionSelector.SelectedRegion != Rectangle.Empty)
                {
                    ProfileManager.ActiveProfile.RegionX = regionSelector.SelectedRegion.X;
                    ProfileManager.ActiveProfile.RegionY = regionSelector.SelectedRegion.Y;
                    ProfileManager.ActiveProfile.RegionWidth = regionSelector.SelectedRegion.Width;
                    ProfileManager.ActiveProfile.RegionHeight = regionSelector.SelectedRegion.Height;
                    ProfileManager.Save();
                    ToggleProfile();
                }
                else
                {
                    rbFullSc.Checked = true;
                    ProfileManager.ActiveProfile.RegionX = 0;
                    ProfileManager.ActiveProfile.RegionY = 0;
                    ProfileManager.ActiveProfile.RegionWidth = 0;
                    ProfileManager.ActiveProfile.RegionHeight = 0;
                    ProfileManager.Save();
                    ToggleProfile();
                }
            }

            // Восстанавливаем окно
            this.WindowState = FormWindowState.Normal;
        }

        private void rbRegionSc_MouseUp(object sender, MouseEventArgs e)
        {
            if (rbRegionSc.Checked)
            {
                if(ProfileManager.ActiveProfile.RegionWidth > 0)
                {
                    EditRegionScreenShot();
                }
                else
                {
                    CreateRegionScreenShot();
                }
            }
        }

        private async void EditRegionScreenShot()
        {
            // Сворачиваем основное окно
            this.WindowState = FormWindowState.Minimized;

            // Даем время для сворачивания
            await Task.Delay(500);

            // Показываем селектор области
            using (var regionSelector = new RegionSelector(GetRegionFromProfile()))
            {
                if (regionSelector.ShowDialog() == DialogResult.OK && regionSelector.SelectedRegion != Rectangle.Empty)
                {
                    ProfileManager.ActiveProfile.RegionX = regionSelector.SelectedRegion.X;
                    ProfileManager.ActiveProfile.RegionY = regionSelector.SelectedRegion.Y;
                    ProfileManager.ActiveProfile.RegionWidth = regionSelector.SelectedRegion.Width;
                    ProfileManager.ActiveProfile.RegionHeight = regionSelector.SelectedRegion.Height;
                    ProfileManager.Save();
                    ToggleProfile();
                }
                else
                {
                    rbFullSc.Checked = true;
                    ProfileManager.ActiveProfile.RegionX = 0;
                    ProfileManager.ActiveProfile.RegionY = 0;
                    ProfileManager.ActiveProfile.RegionWidth = 0;
                    ProfileManager.ActiveProfile.RegionHeight = 0;
                    ProfileManager.Save();
                    ToggleProfile();
                }
            }

            // Восстанавливаем окно
            this.WindowState = FormWindowState.Normal;
        }
        // Получение области из профиля
        private Rectangle GetRegionFromProfile()
        {
            return new Rectangle(
                ProfileManager.ActiveProfile.RegionX,
                ProfileManager.ActiveProfile.RegionY,
                ProfileManager.ActiveProfile.RegionWidth,
                ProfileManager.ActiveProfile.RegionHeight
            );
        }
    }
}
