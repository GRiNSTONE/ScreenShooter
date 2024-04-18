using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShooter
{
    internal class ProfileManager
    {
        private static readonly string SaveFile = $"{Application.StartupPath}\\SaveProfiles.json";
        public static ProfilesSave Profiles { get; set; }
        public static ProfileModel ActiveProfile { get; set; }

        public static void Load()
        {
            if (File.Exists(SaveFile))
            {
                string textSaveFile = File.ReadAllText(SaveFile);
                Profiles = JsonConvert.DeserializeObject<ProfilesSave>(textSaveFile);
                ActiveProfile = Profiles.ProfileModels == null ? new ProfileModel($"{Application.StartupPath}\\") : Profiles.ProfileModels.Where(it => it.Id == Profiles.IsActive).First();
                if(Profiles.ProfileModels == null)
                {
                    Profiles.ProfileModels = new List<ProfileModel>
                    {
                        ActiveProfile
                    };
                }
            }
            else
            {
                throw new Exception("Файл сохранения не найден");
            }
        }

        public static void Save()
        {
            if(File.Exists(SaveFile))
            {
                string textSaveFile = JsonConvert.SerializeObject(Profiles);
                File.WriteAllText(SaveFile, textSaveFile);
            }
            else
            {
                throw new Exception("Файл сохранения не найден");
            }
        }
    }
}
