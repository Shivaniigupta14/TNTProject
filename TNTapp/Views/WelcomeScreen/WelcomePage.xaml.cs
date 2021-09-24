using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNTapp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TNTapp.Views.WelcomeScreen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {

        // Define local variable .....
        protected WelcomePageViewModel WelcomeVM;
        #region Constructor
        public WelcomePage()
        {
            InitializeComponent();
            Xamarin.Forms.MessagingCenter.Send<string>("", "RefreshStatusBar");
            WelcomeVM = new WelcomePageViewModel(this.Navigation);
            this.BindingContext = WelcomeVM;
        }
        #endregion
    }
}