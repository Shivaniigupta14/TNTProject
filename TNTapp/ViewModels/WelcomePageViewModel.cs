using System;
using System.Collections.Generic;
using System.Text;
using TNTapp.Views.SignIN;
using TNTapp.Views.SignUP;
using Xamarin.Forms;

namespace TNTapp.ViewModels
{
    public class WelcomePageViewModel : BaseViewModel
    {
        //Define Local Variables Here... 

        #region Constructor
        public WelcomePageViewModel(INavigation nav)
        {
            Navigation = nav;
            SignUpCommand = new Command(SignUpAsync);
            SignInCommand = new Command(SignInAsync);
        }
        #endregion

        #region Properties
        public Command SignUpCommand { get; }
        public Command SignInCommand { get; }
        #endregion


        #region Method 
        /// <summary>
        /// TODO : To define the SignIn Button ....
        /// </summary>
        /// <param name="obj"></param>
        private async void SignInAsync(object obj)
        {
            await Navigation.PushModalAsync(new SignInPage());
        }
        /// <summary>
        /// TODO : To define the SignUp Button ....
        /// </summary>
        /// <param name="obj"></param>
        private async void SignUpAsync(object obj)
        {
            await Navigation.PushModalAsync(new SignUpPage());

        }
        #endregion
    }
}
