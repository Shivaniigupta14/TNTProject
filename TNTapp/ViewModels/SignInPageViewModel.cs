using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Connectivity;
using TNTapp.Models.Account;
using TNTapp.Views.Home;
using TNTapp.Views.Password;
using Xamarin.Forms;

namespace TNTapp.ViewModels
{
    public class SignInPageViewModel : BaseViewModel
    {
        //Define Local Variables Here...  
        ObservableCollection<Xamarin.Essentials.Contact> contactsList = new ObservableCollection<Xamarin.Essentials.Contact>();

        #region Constructor
        public SignInPageViewModel(INavigation nav)
        {
            Navigation = nav;
            SignInCommand = new Command(OnSignInasync);
            ForgotPasswordCommand = new Command(OnForgotPasswordAsync);
        }
        #endregion


        #region Command
        public Command SignInCommand { get; }
        public Command ForgotPasswordCommand { get; }
        #endregion

        #region Properties
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    OnPropertyChanged("Password");
                }
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// TODO : Sign In Button event....
        /// </summary>
        /// <param name="objs"></param>
        private async void OnSignInasync(object objs)
        {
            if (!await ValidateSignIn())
            {
                return ;
            }
            //  Call api..
            try
            {
                UserDialogs.Instance.ShowLoading("Authenticating User…", MaskType.Clear);
                if (CrossConnectivity.Current.IsConnected)
                {
                    await Task.Run(async () =>
                    {
                        if (_businessCode != null)
                        {
                            await _businessCode.LoginApi(new LoginRequestModel()
                            {
                                userName = Email,
                                password = Password,
                            },
                            async (obj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestList = (obj as LoginResponseModel);
                                    if (requestList != null)
                                    {
                                        try
                                        {
                                            Helpers.Settings.GeneralAccessToken = requestList.token;
                                            Helpers.Settings.GeneralUserID = requestList.userID;
                                            Helpers.Settings.GeneralUserEmail = requestList.emailAddress;
                                            Helpers.Settings.GeneralUserName = requestList.username;
                                            Helpers.Settings.GeneralUserFullName = requestList.firstName + " " + requestList.lastName;
                                            Helpers.Settings.GeneralStaffId = requestList.staffID.ToString();
                                            Helpers.Settings.GeneralCustomerId = requestList.customerID.ToString();
                                            App.Current.MainPage = new HomeTabbedPage();
                                        }
                                        catch (Exception ex)
                                        {
                                            await UserDialogs.Instance.AlertAsync("Login : " + ex.StackTrace, "Success", "OK");
                                        }
                                    }
                                    UserDialog.HideLoading();
                                });
                            }, (objj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestLists = (objj as LoginResponseModel);
                                    if (requestLists != null)
                                    {
                                        UserDialog.HideLoading();
                                        UserDialog.Alert(requestLists.errorMessage, "", "Ok");
                                    }
                                });
                            });
                        }
                    }).ConfigureAwait(false);
                }
                else
                {
                    UserDialogs.Instance.Loading().Hide();
                    await UserDialogs.Instance.AlertAsync("No Network Connection found, Please try again!", "", "Okay");
                }
            }
            catch (Exception ex)
            {
                UserDialog.HideLoading();
            }
        
        }
        //Validation for Sign Up
        private async Task<bool> ValidateSignIn()
        {
            if (string.IsNullOrEmpty(Email))
            {
                UserDialogs.Instance.Alert("Please enter username.");
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                UserDialogs.Instance.Alert("Please enter password.");
                return false;
            }
            UserDialogs.Instance.HideLoading();
            return true;
        }

        /// <summary>
        /// TODO:  Define Forgot Password Tapped event....
        /// </summary>
        /// <param name="obj"></param>
        private async void OnForgotPasswordAsync(object obj)
        {
            Device.OpenUri(new Uri("https://app.retainloyalty.com/account/RecoverPassword"));
        }
        #endregion
    }
}
