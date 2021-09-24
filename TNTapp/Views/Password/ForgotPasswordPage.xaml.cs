using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNTapp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TNTapp.Views.Password
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        //Define Local variable
        protected ForgotPasswordViewModel ForgotPasswordVM;
        #region Constructor
        public ForgotPasswordPage()
        {
            InitializeComponent();
            ForgotPasswordVM = new ForgotPasswordViewModel(this.Navigation);
            this.BindingContext=ForgotPasswordVM;
        }
        #endregion
    }
}