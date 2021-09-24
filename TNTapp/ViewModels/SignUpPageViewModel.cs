using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using TNTapp.Interfaces;
using TNTapp.Models;
using TNTapp.Models.Account;
using TNTapp.Views.WelcomeScreen;
using Xamarin.Forms;

namespace TNTapp.ViewModels
{
    public class SignUpPageViewModel : BaseViewModel
    {
        // Define local variable
        private const string _emailRegex = @"[a-zA-Z]{1,9}\b*[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]*[a-zA-Z]{0,9}$+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
        #region Constructor
        public SignUpPageViewModel ( INavigation nav)
        {
            Navigation = nav;
            SignUpCommand = new Command(OnSignUpasync);
            CompanyLogoCommand = new Command(CompanyLogoAsync);
            TermsOfUseCommand = new Command(TermsofuserAysnc);
            TermsOfPolicyCommand = new Command(TermsofpolicyAsync);
        }
        #endregion

        #region Properties
        private SignUpModel _SignUpModel = new SignUpModel();
        public SignUpModel SignUpModel
        {
            get { return _SignUpModel; }
            set
            {
                if (_SignUpModel != value)
                {
                    _SignUpModel = value;
                    OnPropertyChanged("SignUpModel");
                }
            }
        }
        private bool _CompanyLogoVisible = false;
        public bool CompanyLogoVisible
        {
            get { return _CompanyLogoVisible; }
            set
            {
                if (_CompanyLogoVisible != value)
                {
                    _CompanyLogoVisible = value;
                    OnPropertyChanged("CompanyLogoVisible");
                }
            }

        }
        private string _CompanyLogo;
        public string CompanyLogo
        {
            get { return _CompanyLogo; }
            set
            {
                if (_CompanyLogo != value)
                {
                    _CompanyLogo = value;
                    OnPropertyChanged("CompanyLogo");
                }
            }
        }
        private string _CompanyLogoBase64;
        public string CompanyLogoBase64
        {
            get { return _CompanyLogoBase64; }
            set
            {
                if (_CompanyLogoBase64 != value)
                {
                    _CompanyLogoBase64 = value;
                    OnPropertyChanged("CompanyLogoBase64");
                }
            }
        }

        #endregion

        #region Command
        public Command SignUpCommand { get; }
        public Command CompanyLogoCommand { get; }
        public Command TermsOfUseCommand { get; }
        public Command TermsOfPolicyCommand { get; }
        #endregion

        #region Method
        /// <summary>
        /// TODO : To define Sign up button event....
        /// </summary>
        /// <param name="objs"></param>
        private async void OnSignUpasync(object objs)
        {
            if (!await ValidateSignUp())
            {
                return;
            }

            //Call api..
            try
            {
                UserDialogs.Instance.ShowLoading("Loading…", MaskType.Clear);
                if (CrossConnectivity.Current.IsConnected)
                {
                    await Task.Run(async () =>
                    {
                        if (_businessCode != null)
                        {
                            await _businessCode.SignUpApi(new SignUpRequestModel()
                            {
                                FirstName = SignUpModel.FirstName,
                                LastName = SignUpModel.LastName,
                                UserName = SignUpModel.UserName,
                                Email = SignUpModel.Email,
                                Password = SignUpModel.Password,
                                CellNumber = SignUpModel.PhoneNumber,
                                CompanyName = SignUpModel.Company,
                                Title = SignUpModel.Title == null ? string.Empty : SignUpModel.Title,
                                OfficePhone = SignUpModel.OfficePhoneNumber == null ? string.Empty : SignUpModel.OfficePhoneNumber,
                                Website = SignUpModel.Website == null ? string.Empty : SignUpModel.Website,
                                NMLSnumber = SignUpModel.NMLS == null ? string.Empty : SignUpModel.NMLS,
                                ReferredBy = SignUpModel.ReferredBy == null ? string.Empty : SignUpModel.ReferredBy,
                                Logo = CompanyLogoBase64
                            },
                            async (obj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestList = (obj as SignUpResponseModel);
                                    if (requestList != null)
                                    {
                                        try
                                        {
                                            var alertConfig = new AlertConfig
                                            {
                                                // Title = "Alert",
                                                Message = "Thank you for signing up. You will be notified as soon as your account is ready.",
                                                OkText = "Ok",
                                                OnAction = async () =>
                                                {
                                                    await Navigation.PushModalAsync(new WelcomePage());
                                                }
                                            };
                                            UserDialogs.Instance.Alert(alertConfig);
                                        }
                                        catch (Exception ex)
                                        {
                                            await UserDialogs.Instance.AlertAsync("SignUp : " + ex.StackTrace, "Success", "OK");
                                        }
                                    }
                                    UserDialog.HideLoading();
                                });
                            }, (objj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestLists = (objj as SignUpResponseModel);
                                    if (requestLists != null)
                                    {
                                        UserDialog.HideLoading();
                                        UserDialog.Alert(requestLists.errorMessage, "", "Ok");
                                    }
                                    else
                                    {
                                        UserDialog.HideLoading();
                                        UserDialog.Alert("Something went wrong.  Please restart the app and try again.", "", "Ok");
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
            // await Navigation.PushModalAsync(new WelcomePage());
        }
        // TODO : To validate SignUp
        private  async Task<bool> ValidateSignUp()
        {
            if (string.IsNullOrEmpty(SignUpModel.FirstName))
            {
                UserDialogs.Instance.Alert("Please enter first name.");
                return false;
            }
            if (string.IsNullOrEmpty(SignUpModel.LastName))
            {
                UserDialogs.Instance.Alert("Please enter last name.");
                return false;
            }
            if (string.IsNullOrEmpty(SignUpModel.UserName))
            {
                UserDialogs.Instance.Alert("Please enter user name.");
                return false;
            }
            if (string.IsNullOrEmpty(SignUpModel.Email))
            {
                UserDialogs.Instance.Alert("Please enter email.");
                return false;
            }
            bool isValid = (Regex.IsMatch(SignUpModel.Email, _emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            if (!isValid)
            {
                UserDialogs.Instance.Alert("Please enter valid email address.");
                return false;
            }
            if (string.IsNullOrEmpty(SignUpModel.Password))
            {
                UserDialogs.Instance.Alert("Please enter password.");
                return false;
            }
            if (string.IsNullOrEmpty(SignUpModel.PhoneNumber))
            {
                UserDialogs.Instance.Alert("Please enter phone number.");
                return false;
            }
            if (string.IsNullOrEmpty(SignUpModel.Company))
            {
                UserDialogs.Instance.Alert("Please enter company name.");
                return false;
            }
            return true;
        }
        /// <summary>
        /// TODO:To define the Company Logo tapped event...
        /// </summary>
        /// <param name="obj"></param>
        private void CompanyLogoAsync(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                //Ask the user if they want to use the camera or pick from the gallery
                var action = await UserDialogs.Instance.ActionSheetAsync("Add Logo", "Cancel", "", null, "Choose Existing", "Take Photo");
                if ((action == "Take Photo"))
                {
                    try
                    {
                        var status = await CrossPermissions.Current.CheckPermissionStatusAsync (Plugin.Permissions.Abstractions.Permission.Camera);

                        if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                        {
                            var result = await CrossPermissions.Current.RequestPermissionsAsync(new[] {Plugin.Permissions.Abstractions.Permission.Camera });
                            status = result[Plugin.Permissions.Abstractions.Permission.Camera];
                        }

                        if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                        {
                            if (!CrossMedia.Current.IsCameraAvailable)
                            {
                                UserDialogs.Instance.Alert("No camera avaialble.", null, "OK");
                                return;
                            }
                            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                            {
                                Directory = "Sample",
                                Name = "test.png"
                            });
                            if (file == null)
                            {
                                UserDialogs.Instance.Alert("Unable to pick profile picture. please try again.", null, "OK");
                                return;
                            }
                            Helpers.AppGlobalConstants.ProfileImgFilePath = file.Path;
                            var ImageByteData = await DependencyService.Get<IMediaService>().ResizeImage(await DependencyService.Get<IMediaService>().GetMediaInBytes(file.Path), 120, 120);
                            CompanyLogoBase64 = Convert.ToBase64String(ImageByteData);
                            CompanyLogo = file.Path;
                            CompanyLogoVisible = true;
                        }
                        UserDialogs.Instance.HideLoading();
                    }
                    catch (Exception exception)
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                                        
                }
                else if ((action == "Choose Existing"))
                {
                    try
                    {
                        var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Photos);

                        if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                        {
                            var result = await CrossPermissions.Current.RequestPermissionsAsync(new[] {Plugin.Permissions.Abstractions.Permission.Photos });
                            status = result[Plugin.Permissions.Abstractions.Permission.Photos];
                        }

                        if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                        {
                            if (!CrossMedia.Current.IsPickPhotoSupported)
                            {
                                return;
                            }
                            var file = await CrossMedia.Current.PickPhotoAsync();
                            if (file == null)
                            {
                                UserDialogs.Instance.Alert("Unable to pick profile picture. please try again.", null, "OK");
                                return;
                            }
                            Helpers.AppGlobalConstants.ProfileImgFilePath = file.Path;
                            var ImageByteData = await DependencyService.Get<IMediaService>().ResizeImage(await DependencyService.Get<IMediaService>().GetMediaInBytes(file.Path), 120, 120);
                            CompanyLogoBase64 = Convert.ToBase64String(ImageByteData);
                            CompanyLogo = file.Path;
                            CompanyLogoVisible = true;
                        }
                        UserDialogs.Instance.HideLoading();
                    }
                    catch (Exception exception)
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                }
            });
        }
        /// <summary>
        /// TODO : To define the terms of user using click the link ....
        /// </summary>
        /// <param name="obj"></param>

        private async void TermsofuserAysnc(object obj)
        {
            Device.OpenUri(new Uri("https://www.tntconnectsus.com/terms-of-service"));
        }
        /// <summary>
        /// TODO : To define the terms of policy using click the link..... 
        /// </summary>
        /// <param name="obj"></param>
        private async void TermsofpolicyAsync(object obj)
        {
            Device.OpenUri(new Uri("https://www.tntconnectsus.com/privacy"));
        }

        #endregion
    }
}
