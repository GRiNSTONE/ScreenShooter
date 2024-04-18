using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShooter
{
    internal class ProfileModel
    {
        public ProfileModel () { }
        public ProfileModel(string rootPathFolder, string name = "Новый профиль", bool isDivideToMonths = false, bool isSound = false)
        {
            Id = ProfileManager.Profiles.ProfileModels == null ? 0 : ProfileManager.Profiles.ProfileModels.Max(m => m.Id) + 1;
            NameProfile = name;
            RootPathFolder = rootPathFolder;
            IsDivideToMonths = isDivideToMonths;
            IsSound = isSound;
        }
        public int Id { get; set; }
        public string NameProfile { get; set; }
        public string RootPathFolder { get; set; }
        public bool IsDivideToMonths { get; set; }
        public bool IsSound { get; set; }
    }
}
