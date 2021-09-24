using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using TNTapp.Interfaces;
using TNTapp.Models.Account;
using TNTapp.Views.EditProfile;
using TNTapp.Views.WelcomeScreen;
using Xamarin.Forms;

namespace TNTapp.ViewModels.Home
{
    public class ProfilePageViewModel : BaseViewModel
    {
        //Define Local variable 
        public bool IsPhotoUploaded = false;
        #region Constructor
        public ProfilePageViewModel(INavigation nav)
        {
            Navigation = nav;
            LogOutCommand = new Command(OnLogOutCommandAsync);
            ProfileImageCommand = new Command(OnProfileImageAsync);
            EditCommand = new Command(OnEditCommandAsync);
        }

        #endregion


        #region Properties

        private string _UserName = "Dru Snyder";
        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName != value)
                {
                    _UserName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }

        private string _Email = "dru@retainliyakty.com Snyder";
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

        private string _ProfileImage = "user.png";
        public string ProfileImage
        {
            get { return _ProfileImage; }
            set
            {
                if (_ProfileImage != value)
                {
                    _ProfileImage = value;
                    OnPropertyChanged("ProfileImage");
                }
            }
        }

        private string _ProfileImgBase64;
        public string ProfileImgBase64
        {
            get { return _ProfileImgBase64; }
            set
            {
                if (_ProfileImgBase64 != value)
                {
                    _ProfileImgBase64 = value;
                    OnPropertyChanged("ProfileImgBase64");
                }
            }
        }
        private bool _IsPhotoCapture = false;
        public bool IsPhotoCapture
        {
            get { return _IsPhotoCapture; }
            set
            {
                if (_IsPhotoCapture != value)
                {
                    _IsPhotoCapture = value;
                    OnPropertyChanged("IsPhotoCapture");
                }
            }
        }
        #endregion


        #region Command
        public Command LogOutCommand { get; }
        public Command ProfileImageCommand { get; }
        public Command EditCommand { get; }
        #endregion

        #region Method
        /// <summary>
        /// TODO : to define Logout button event.......
        /// </summary>
        /// <param name="obj"></param>
        private async void OnLogOutCommandAsync(object obj)
        {
            Helpers.Settings.GeneralAccessToken = string.Empty;
            Helpers.Settings.GeneralUserProfilePic = string.Empty;
            Helpers.Settings.GeneralUserName = string.Empty;
            App.Current.MainPage = new WelcomePage();
        }
        /// <summary>
        /// TODO:To define the get user photo Api...
        /// </summary>
        /// <returns></returns>
        public async Task GetUserPhotoDetail()
        {
            //Call api...
            try
            {
                UserDialogs.Instance.ShowLoading("Loading…", MaskType.Clear);
                if (CrossConnectivity.Current.IsConnected)
                {
                    await Task.Run(async () =>
                    {
                        if (_businessCode != null)
                        {
                            await _businessCode.GetUserPhotoApi(
                            async (obj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestList = (obj as GetUserPhotoResponseModel);
                                    if (requestList != null)
                                    {
                                        try
                                        {
                                            Helpers.Settings.GeneralUserProfilePic = requestList.ImageBase64;
                                            Xamarin.Forms.MessagingCenter.Send<string>("", "UpdateProfileImage");
                                        }
                                        catch (Exception ex)
                                        {
                                            await UserDialogs.Instance.AlertAsync(ex.StackTrace, "Success", "OK");
                                        }
                                    }
                                    UserDialog.HideLoading();
                                });
                            }, (objj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestLists = (objj as GetUserPhotoResponseModel);
                                    if (requestLists != null)
                                    {
                                        UserDialog.HideLoading();
                                        UserDialog.Alert(requestLists.ErrorMessage, "", "Ok");
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
        }
        /// <summary>
        ///  TODO : To define 
        /// </summary>
        /// <param name="obj"></param>
        private async void OnProfileImageAsync(object obj)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                //Ask the user if they want to use the camera or pick from the gallery
                var action = await UserDialogs.Instance.ActionSheetAsync("Add Photo", "Cancel", "", null, "Choose Existing", "Take Photo");

                //Take photo
                if ((action == "Take Photo"))
                {
                    try
                    {
                        //taking permission for camera
                        var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Camera);

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
                            IsPhotoCapture = true;
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
                            ProfileImgBase64 = Convert.ToBase64String(ImageByteData);
                            await UpdatePhoto();
                            if (IsPhotoUploaded)
                            {
                                ProfileImage = file.Path;
                                Helpers.Settings.GeneralUserProfilePic = ProfileImgBase64;
                                Xamarin.Forms.MessagingCenter.Send<string>("", "UpdateProfileImage");
                            }
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
                            IsPhotoCapture = true;
                            var file = await CrossMedia.Current.PickPhotoAsync();
                            if (file == null)
                            {
                                UserDialogs.Instance.Alert("Unable to pick profile picture. please try again.", null, "OK");
                                return;
                            }
                            Helpers.AppGlobalConstants.ProfileImgFilePath = file.Path;
                          
                            var ImageByteData = await DependencyService.Get<IMediaService>().ResizeImage(await DependencyService.Get<IMediaService>().GetMediaInBytes(file.Path), 120, 120);
                            ProfileImgBase64 = Convert.ToBase64String(ImageByteData);
                            await UpdatePhoto();
                            if (IsPhotoUploaded)
                            {
                                ProfileImage = file.Path;
                                Helpers.Settings.GeneralUserProfilePic = ProfileImgBase64;
                                Xamarin.Forms.MessagingCenter.Send<string>("", "UpdateProfileImage");
                            }
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
        /// TODO : To define Update photo of user by Api .....
        /// </summary>
        /// <returns></returns>

        private async  Task UpdatePhoto()
        {
            //Call api...
            try
            {
                UserDialogs.Instance.ShowLoading("Loading…", MaskType.Clear);
                if (CrossConnectivity.Current.IsConnected)
                {
                    await Task.Run(async () =>
                    {
                        if (_businessCode != null)
                        {
                            await _businessCode.UpdateUserPhotoApi(new UpdateUserPhotoRequestModel()
                            {
                                UserID = Helpers.Settings.GeneralUserID,
                                Username = Helpers.Settings.GeneralUserName,
                                PhotoString = ProfileImgBase64,
                            },
                            async (obj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestList = (obj as UpdateUserPhotoResponseModel);
                                    if (requestList != null)
                                    {
                                        try
                                        {
                                            var alertConfig = new AlertConfig
                                            {
                                                Message = "Your profile photo has been updated successfully.",
                                                OkText = "Ok",
                                                OnAction = async () =>
                                                {
                                                    Helpers.Settings.GeneralUserProfilePic = ProfileImgBase64;
                                                    Xamarin.Forms.MessagingCenter.Send<string>("", "UpdateProfileImage");
                                                }
                                            };
                                            UserDialogs.Instance.Alert(alertConfig);
                                        }
                                        catch (Exception ex)
                                        {
                                            await UserDialogs.Instance.AlertAsync(ex.StackTrace, "Success", "OK");
                                        }
                                    }
                                    UserDialog.HideLoading();
                                });
                            }, (objj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestLists = (objj as UpdateUserPhotoResponseModel);
                                    if (requestLists != null)
                                    {
                                        UserDialog.HideLoading();
                                        UserDialog.Alert(requestLists.errorMessage, "Alert", "Ok");
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
                
            }
        }
        /// <summary>
        /// TODO : To define Edit Button event....
        /// </summary>
        /// <param name="obj"></param>

        private async  void OnEditCommandAsync(object obj)
        {
            await Navigation.PushModalAsync(new EditProfilePage());
        }

        #endregion
    }
}
