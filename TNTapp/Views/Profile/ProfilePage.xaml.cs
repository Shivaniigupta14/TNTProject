using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using TNTapp.ViewModels.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TNTapp.Views.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        // Define local variable
        protected ProfilePageViewModel ProfilePageVM;
        #region Constructor
        public ProfilePage()
        {
            InitializeComponent();
            ProfilePageVM = new ProfilePageViewModel(this.Navigation);
            this.BindingContext = ProfilePageVM;

            if (!string.IsNullOrEmpty(Helpers.Settings.GeneralUserProfilePic))
            {
                ImageProfile.Source = Utility.Utility.GetImageFromBase64(Helpers.Settings.GeneralUserProfilePic);
            }

            if (!string.IsNullOrEmpty(Helpers.Settings.GeneralUserName))
            {
                ProfilePageVM.UserName = Helpers.Settings.GeneralUserName;
            }
            if (!string.IsNullOrEmpty(Helpers.Settings.GeneralUserEmail))
            {
                ProfilePageVM.Email = Helpers.Settings.GeneralUserEmail;
            }

            MessagingCenter.Subscribe<string>("", "UpdateProfileImage", (sender) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                  
                    if (!string.IsNullOrEmpty(Helpers.Settings.GeneralUserProfilePic))
                    {
                        var _imgProfile = Utility.Utility.GetImageFromBase64(Helpers.Settings.GeneralUserProfilePic);
                        if (_imgProfile != null)
                        {
                            ImageProfile.Source = _imgProfile;
                        }
                        else
                        {
                            ImageProfile.Source = "user.png";
                        }
                    }
                });
            });

        }
        #endregion
        #region Event Handler
        /// <summary>
        /// TODO:To define the page on appearing event...
        /// </summary>
        protected async override void OnAppearing()
        {
            Xamarin.Forms.MessagingCenter.Send<string>("", "RefreshStatusBar");
            base.OnAppearing();
            if (!ProfilePageVM.IsPhotoCapture)
            {
                await ProfilePageVM.GetUserPhotoDetail();
            }
        }
        #endregion
    }
}

