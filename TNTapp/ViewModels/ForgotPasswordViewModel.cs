using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TNTapp.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        #region Constructor
        public ForgotPasswordViewModel(INavigation nav)
        {
            Navigation = nav;
            ChangePasswordCommand = new Command(OnChangePasswordAsync);
        }

        #endregion

        #region Properties
        public Command ChangePasswordCommand { get; }
        
        #endregion

        #region Method
        private async void OnChangePasswordAsync(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
