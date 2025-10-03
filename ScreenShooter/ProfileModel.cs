using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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
            ImageFormat = ImageFormat.Jpeg;
            ImageQuality = 95L;
            RegionX = 0;
            RegionY = 0;
            RegionWidth = 0;
            RegionHeight = 0;
        }
        public int Id { get; set; }
        public string NameProfile { get; set; }
        public string RootPathFolder { get; set; }
        public bool IsDivideToMonths { get; set; }
        public bool IsSound { get; set; }
        public ImageFormat ImageFormat { get; set; }
        public Int64 ImageQuality { get; set; }

        public int RegionX { get; set; }
        public int RegionY { get; set; }
        public int RegionWidth { get; set; }
        public int RegionHeight { get; set; }

    }
}
