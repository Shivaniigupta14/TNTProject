using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNTapp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TNTapp.Views.SignIN
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        // Define Local Variable
        protected SignInPageViewModel SignInVM;
        #region Constructor
        public SignInPage()
        {
            InitializeComponent();
            SignInVM = new SignInPageViewModel(this.Navigation);
            this.BindingContext = SignInVM;
        }
        #endregion
    }
}