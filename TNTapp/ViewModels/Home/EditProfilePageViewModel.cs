using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Connectivity;
using TNTapp.Models;
using TNTapp.Models.Account;
using Xamarin.Forms;

namespace TNTapp.ViewModels.Home
{
    public class EditProfilePageViewModel :BaseViewModel
    {
        #region Constructor
        public EditProfilePageViewModel(INavigation nav)
        {
            Navigation = nav;
            UpdateProfileCommand = new Command(OnUpdateProfileAsync);
        }
        #endregion

        #region Properties
        private GetUserProfileResponseModel _UpdateProfileModel = new GetUserProfileResponseModel();
        public GetUserProfileResponseModel UpdateProfileModel
        {
            get { return _UpdateProfileModel; }
            set
            {
                if (_UpdateProfileModel != value)
                {
                    _UpdateProfileModel = value;
                    OnPropertyChanged("UpdateProfileModel");
                }
            }
        }
        #endregion

        #region Command
        public Command UpdateProfileCommand { get; }
        #endregion
        #region Method

        /// <summary>
        /// TODO:To call the GetUserProfile...
        /// </summary>
        /// <returns></returns>
        public async Task GetUserProfileDetail()
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
                            await _businessCode.GetUserProfileApi(
                            async (obj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestList = (obj as GetUserProfileResponseModel);
                                    if (requestList != null)
                                    {
                                        try
                                        {
                                            UpdateProfileModel = requestList;
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
                                    var requestLists = (objj as GetUserProfileResponseModel);
                                    if (requestLists != null)
                                    {
                                        UserDialog.HideLoading();
                                        UserDialog.Alert(requestLists.errorMessage, "null", "Ok");
                                    }
                                    else
                                    {
                                        UserDialog.HideLoading();
                                        UserDialog.Alert("Something went wrong.  Please restart the app and try again.", "null", "Ok");
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
        /// ToDO : to define update button event...
        /// </summary>
        /// <param name="objs"></param>
        private async void OnUpdateProfileAsync(object objs)
        {
            if (!await Validate()) { return; }
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
                            await _businessCode.UpdateProfileApi(new UpdateProfileRequestModel()
                            {
                                UserID = Helpers.Settings.GeneralUserID,
                                Username = Helpers.Settings.GeneralUserName,
                                FirstName = UpdateProfileModel.firstName,
                                LastName = UpdateProfileModel.lastName,
                                CellNumber = UpdateProfileModel.cellNumber,
                                Title = UpdateProfileModel.title,
                                Website = UpdateProfileModel.website,
                                OfficePhone = UpdateProfileModel.officeNumber,
                                NMLSnumber = UpdateProfileModel.nmlSnumber == null ? string.Empty : UpdateProfileModel.nmlSnumber,
                                ReferredBy = UpdateProfileModel.referredBy == null ? string.Empty : "TntConnect",
                            },
                            async (obj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestList = (obj as UpdateProfileResponseModel);
                                    if (requestList != null)
                                    {
                                        try
                                        {
                                            var alertConfig = new AlertConfig
                                            {
                                                Message = "Your profile has been updated successfully.",
                                                OkText = "Ok",
                                                OnAction = async () =>
                                                {
                                                    await Navigation.PushModalAsync(new Views.Home.HomeTabbedPage());
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
                                    var requestLists = (objj as UpdateProfileResponseModel);
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
        }
        
        /// <summary>
        /// TODO : Validate the inputs
        /// </summary>
        /// <returns></returns>
        private async Task<bool> Validate()
        {
            if (string.IsNullOrEmpty(UpdateProfileModel.firstName))
            {
                UserDialogs.Instance.Alert("Please enter First name.");
                return false;
            }
            if (string.IsNullOrEmpty(UpdateProfileModel.lastName))
            {
                UserDialogs.Instance.Alert("Please enter Last name.");
                return false;
            }
            if (string.IsNullOrEmpty(UpdateProfileModel.cellNumber))
            {
                UserDialogs.Instance.Alert("Please enter Cell Number .");
                return false;
            }
            if(string.IsNullOrEmpty(UpdateProfileModel.title))
            {
                UserDialogs.Instance.Alert("Please enter Title.");
                return false;
            }
            if (string.IsNullOrEmpty(UpdateProfileModel.website))
            {
                UserDialogs.Instance.Alert("Please enter Website.");
                return false;
            }
            if (string.IsNullOrEmpty(UpdateProfileModel.officeNumber))
            {
                UserDialogs.Instance.Alert("Please enter Office Number.");
                return false;
            }
            return true;
        }

        #endregion
    }
}
