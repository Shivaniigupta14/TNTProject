using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNTapp.ViewModels.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TNTapp.Views.EditProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilePage : ContentPage
    { 
        // Define Local Variable
        protected EditProfilePageViewModel EditProfilePageVM;
        #region Constructor
        public EditProfilePage()
        {
            InitializeComponent();
            EditProfilePageVM = new EditProfilePageViewModel(this.Navigation);
            this.BindingContext = EditProfilePageVM;
        }
        #endregion
        #region EventHandler
        /// <summary>
        /// TODO:To define the page on appearing event...
        /// </summary>
        protected async override void OnAppearing()
        {
            Xamarin.Forms.MessagingCenter.Send<string>("", "RefreshStatusBar");
            base.OnAppearing();
            await EditProfilePageVM.GetUserProfileDetail();
        }
        #endregion
    }
}