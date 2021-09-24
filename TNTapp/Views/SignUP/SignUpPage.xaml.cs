using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNTapp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TNTapp.Views.SignUP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        // Define local variable
        protected SignUpPageViewModel SignUpVM;
        #region Constructor
        public SignUpPage()
        {
            InitializeComponent();
            SignUpVM = new SignUpPageViewModel(this.Navigation);
            this.BindingContext = SignUpVM;
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
        }
        #endregion
    }
}