using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShooter
{
    public partial class ProfileList : Form
    {
        public ProfileList()
        {
            InitializeComponent();
            lbProfilesList.ValueMember = "Id";
            lbProfilesList.DisplayMember = "NameProfile";
            LoadList();
        }

        private void LoadList()
        {
            lbProfilesList.Items.Clear();
            lbProfilesList.Items.AddRange(ProfileManager.Profiles.ProfileModels.ToArray());
        }

        private void tbNameOfNewProfile_MouseClick(object sender, MouseEventArgs e)
        {
            tbNameOfNewProfile.SelectAll();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                ProfileModel profile = new ProfileModel(folderBrowserDialog.SelectedPath, tbNameOfNewProfile.Text);
                ProfileManager.Profiles.ProfileModels.Add(profile);
                ProfileManager.Profiles.IsActive = profile.Id;
                ProfileManager.ActiveProfile = ProfileManager.Profiles.ProfileModels.Where(it => it.Id == profile.Id).First();
                ProfileManager.Save();
                tbNameOfNewProfile.Text = "";
                LoadList();
            }
            
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if(lbProfilesList.SelectedItem != null && lbProfilesList.Items.Count > 1)
            {
                ProfileModel delItem = lbProfilesList.SelectedItem as ProfileModel;

                if(ProfileManager.ActiveProfile == delItem)
                {
                    ProfileManager.ActiveProfile = ProfileManager.Profiles.ProfileModels.Where(it => it.Id != delItem.Id).First();
                    ProfileManager.Profiles.IsActive = ProfileManager.ActiveProfile.Id;
                }
                
                ProfileManager.Profiles.ProfileModels.Remove(delItem);
                lbProfilesList.Items.Remove(delItem);

                ProfileManager.Save();
            }
            else
            {
                MessageBox.Show("Нельзя удалить все профили", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            if (lbProfilesList.SelectedItem != null && lbProfilesList.Items.Count > 1)
            {
                ProfileModel selectItem = lbProfilesList.SelectedItem as ProfileModel;

                ProfileManager.ActiveProfile = ProfileManager.Profiles.ProfileModels.Where(it => it.Id == selectItem.Id).First();
                ProfileManager.Profiles.IsActive = ProfileManager.ActiveProfile.Id;

                ProfileManager.Save();
            }
        }
    }
}
