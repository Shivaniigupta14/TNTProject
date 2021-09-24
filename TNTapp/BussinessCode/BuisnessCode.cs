using System;
using Acr.UserDialogs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TNTapp.Models.Account;
using TNTapp.Models.Contact;
using TNTapp.Providers;
using TNTapp.Services;

namespace TNTapp.BussinessCode
{
    public class BuisnessCode : IBusinessCode
    {
        IApiProvider _apiProvider;
        public BuisnessCode(IApiProvider apiProvider)
        {
            try
            {
                //To initialize service providers...
                _apiProvider = apiProvider;
                HttpClientHandler handler = new HttpClientHandler();
            }
            catch (Exception ex)
            { }
        }

        #region  Apis Definations

        #region Account Apis
        /// <summary>
        /// TODO:To Call The Login Api...
        /// </summary>
        /// <param name="request"></param>
        /// <param name="success"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        public async Task<LoginResponseModel> LoginApi(LoginRequestModel request, Action<object> success, Action<object> failed)
        {
            LoginResponseModel resmodel = new LoginResponseModel();
            try
            {
                // adding api url for login
                var url = string.Format("{0}Login/Login", WebServiceDetails.BaseUri);
                //call Post Api for login
                var result = _apiProvider.Post<LoginResponseModel, LoginRequestModel>(url, request, null);
               
                var response = result.Result;
                LoginResponseModel objres = null;
                dynamic obj = "";
                LoginResponseModel reg = new LoginResponseModel();
                if (response.IsSuccessful != false)
                {
                    objres = JsonConvert.DeserializeObject<LoginResponseModel>(response.RawResult);
                    if (objres.status != "0")
                    {
                        success.Invoke(objres);
                    }
                    else
                        failed.Invoke(objres);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    objres = JsonConvert.DeserializeObject<LoginResponseModel>(response.RawResult);
                    failed.Invoke(objres);
                }
            }
            catch (Exception exception)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Something went wrong.  Please restart the app and try again.", "", "OK");
            }
            return resmodel;
        }

        /// <summary>
        /// TODO:To Call The Change Password Api...
        /// </summary>
        /// <param name="request"></param>
        /// <param name="success"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        public async Task<ChangePasswordResponseModel> ChangePasswordApi(ChangePasswordRequestModel request, Action<object> success, Action<object> failed)
        {
            ChangePasswordResponseModel resmodel = new ChangePasswordResponseModel();
            try
            {
                var url = string.Format("{0}Login/ChangePassword", WebServiceDetails.BaseUri);
                var dic = new Dictionary<string, string>();
                dic.Add("Authorization", "Bearer " + Helpers.Settings.GeneralAccessToken);
                var result = _apiProvider.Post<ChangePasswordResponseModel, ChangePasswordRequestModel>(url, request, dic);
                var response = result.Result;
                ChangePasswordResponseModel objres = null;
                dynamic obj = "";
                ChangePasswordResponseModel reg = new ChangePasswordResponseModel();
                if (response.IsSuccessful != false)
                {
                    objres = JsonConvert.DeserializeObject<ChangePasswordResponseModel>(response.RawResult);
                    if (objres.status != "0")
                    {
                        success.Invoke(objres);
                    }
                    else
                        failed.Invoke(objres);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    objres = JsonConvert.DeserializeObject<ChangePasswordResponseModel>(response.RawResult);
                    failed.Invoke(obj);
                }
            }
            catch (Exception exception)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Something went wrong.  Please restart the app and try again.", "", "OK");
            }
            return resmodel;
        }

        /// <summary>
        /// TODO:To Call The SignUp Api...
        /// </summary>
        /// <param name="request"></param>
        /// <param name="success"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        public async Task<SignUpResponseModel> SignUpApi(SignUpRequestModel request, Action<object> success, Action<object> failed)
        {
            SignUpResponseModel resmodel = new SignUpResponseModel();
            try
            {
                var url = string.Format("{0}SignUp/SignUp", WebServiceDetails.BaseUri);
                var result = _apiProvider.Post<SignUpResponseModel, SignUpRequestModel>(url, request, null);
                var response = result.Result;
                SignUpResponseModel objres = null;

                dynamic obj = "";
                SignUpResponseModel reg = new SignUpResponseModel();
                if (response.IsSuccessful != false)
                {
                    objres = JsonConvert.DeserializeObject<SignUpResponseModel>(response.RawResult);
                    if (objres.status != "0")
                    {
                        success.Invoke(objres);
                    }
                    else
                    {
                        objres = JsonConvert.DeserializeObject<SignUpResponseModel>(response.RawResult);
                        failed.Invoke(objres);
                    }

                }

                else
                {
                    UserDialogs.Instance.HideLoading();
                    objres = JsonConvert.DeserializeObject<SignUpResponseModel>(response.RawResult);
                    failed.Invoke(objres);
                }
            }
            catch (Exception exception)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Something went wrong.  Please restart the app and try again.", "", "OK");
            }
            return resmodel;
        }

        /// <summary>
        /// TODO:To Call The Forgot Password Api...
        /// </summary>
        /// <param name="request"></param>
        /// <param name="success"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        public async Task<ForgotPasswordResponseModel> ForgotPasswordApi(ForgotPasswordRequestModel request, Action<object> success, Action<object> failed)
        {
            ForgotPasswordResponseModel resmodel = new ForgotPasswordResponseModel();
            try
            {
                var url = string.Format("{0}Login?Username=", WebServiceDetails.BaseUri);
                var result = _apiProvider.Post<ForgotPasswordResponseModel, ForgotPasswordRequestModel>(url, request, null);
                var response = result.Result;
                ForgotPasswordResponseModel objres = null;
                dynamic obj = "";
                ForgotPasswordResponseModel reg = new ForgotPasswordResponseModel();
                if (response.IsSuccessful != false)
                {
                    objres = JsonConvert.DeserializeObject<ForgotPasswordResponseModel>(response.RawResult);
                    if (objres.status != "Error")
                    {
                        success.Invoke(objres);
                    }
                    else
                        failed.Invoke(objres);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    failed.Invoke(obj);
                }
            }
            catch (Exception exception)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Something went wrong.  Please restart the app and try again.", "", "OK");
            }
            return resmodel;
        }
        #endregion

        #region Profile Apis
        /// <summary>
        /// TODO:To Call The GetUserPhoto Api...
        /// </summary>
        /// <param name="success"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        public async Task<GetUserPhotoResponseModel> GetUserPhotoApi(Action<object> success, Action<object> failed)
        {
            GetUserPhotoResponseModel resmodel = new GetUserPhotoResponseModel();
            try
            {
                var url = string.Format("{0}UpdateProfile/GetUserPhoto?UserID=" + Helpers.Settings.GeneralUserID + "&Username=" + Helpers.Settings.GeneralUserName, WebServiceDetails.BaseUri);
                var dic = new Dictionary<string, string>();
                dic.Add("Authorization", "Bearer " + Helpers.Settings.GeneralAccessToken);
                var result = _apiProvider.Get<GetUserPhotoResponseModel>(url, dic);
                var response = result.Result;
                GetUserPhotoResponseModel objres = null;
                dynamic obj = "";
                GetUserPhotoResponseModel reg = new GetUserPhotoResponseModel();
                if (result.IsSuccessful != false)
                {
                    objres = JsonConvert.DeserializeObject<GetUserPhotoResponseModel>(result.RawResult);
                    if (objres.Status != "0")
                    {
                        success.Invoke(objres);
                    }
                    else
                        failed.Invoke(objres);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    failed.Invoke(obj);
                }
            }
            catch (Exception exception)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Something went wrong.  Please restart the app and try again.", "", "OK");
            }
            return resmodel;
        }

        /// <summary>
        /// TODO:To Call The GetUserProfile Api...
        /// </summary>
        /// <param name="success"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        public async Task<GetUserProfileResponseModel> GetUserProfileApi(Action<object> success, Action<object> failed)
        {
            GetUserProfileResponseModel resmodel = new GetUserProfileResponseModel();
            try
            {
                var url = string.Format("{0}UpdateProfile/GetUserProfile?UserID=" + Helpers.Settings.GeneralUserID + "&Username=" + Helpers.Settings.GeneralUserName, WebServiceDetails.BaseUri);
                var dic = new Dictionary<string, string>();
                dic.Add("Authorization", "Bearer " + Helpers.Settings.GeneralAccessToken);
                var result = _apiProvider.Get<GetUserProfileResponseModel>(url, dic);
                var response = result.Result;
                GetUserProfileResponseModel objres = null;
                dynamic obj = "";
                GetUserProfileResponseModel reg = new GetUserProfileResponseModel();
                if (result.IsSuccessful != false)
                {
                    objres = JsonConvert.DeserializeObject<GetUserProfileResponseModel>(result.RawResult);
                    if (objres.status != "0")
                    {
                        success.Invoke(objres);
                    }
                    else
                        failed.Invoke(objres);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    failed.Invoke(obj);
                }
            }
            catch (Exception exception)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Something went wrong.  Please restart the app and try again.", "", "OK");
            }
            return resmodel;
        }

        /// <summary>
        /// TODO:To Call The Update Profile Api...
        /// </summary>
        /// <param name="request"></param>
        /// <param name="success"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        public async Task<UpdateProfileResponseModel> UpdateProfileApi(UpdateProfileRequestModel request, Action<object> success, Action<object> failed)
        {
            UpdateProfileResponseModel resmodel = new UpdateProfileResponseModel();
            try
            {
                var url = string.Format("{0}UpdateProfile/UpdateProfile", WebServiceDetails.BaseUri);
                var dic = new Dictionary<string, string>();
                dic.Add("Authorization", "Bearer " + Helpers.Settings.GeneralAccessToken);
                var result = _apiProvider.Post<UpdateProfileResponseModel, UpdateProfileRequestModel>(url, request, dic);
                var response = result.Result;
                UpdateProfileResponseModel objres = null;
                dynamic obj = "";
                UpdateProfileResponseModel reg = new UpdateProfileResponseModel();
                if (response.IsSuccessful != false)
                {
                    objres = JsonConvert.DeserializeObject<UpdateProfileResponseModel>(response.RawResult);
                    if (objres.status != "0")
                    {
                        success.Invoke(objres);
                    }
                    else
                        failed.Invoke(objres);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    failed.Invoke(obj);
                }
            }
            catch (Exception exception)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Something went wrong.  Please restart the app and try again.", "", "OK");
            }
            return resmodel;
        }

        /// <summary>
        /// TODO:To Call The Update user photo...
        /// </summary>
        /// <param name="request"></param>
        /// <param name="success"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        public async Task<UpdateUserPhotoResponseModel> UpdateUserPhotoApi(UpdateUserPhotoRequestModel request, Action<object> success, Action<object> failed)
        {
            UpdateUserPhotoResponseModel resmodel = new UpdateUserPhotoResponseModel();
            try
            {
                var url = string.Format("{0}UpdateProfile/UpdateUserPhoto", WebServiceDetails.BaseUri);
                var dic = new Dictionary<string, string>();
                dic.Add("Authorization", "Bearer " + Helpers.Settings.GeneralAccessToken);
                var result = _apiProvider.Post<UpdateUserPhotoResponseModel, UpdateUserPhotoRequestModel>(url, request, dic);
                var response = result.Result;
                UpdateUserPhotoResponseModel objres = null;
                dynamic obj = "";
                UpdateUserPhotoResponseModel reg = new UpdateUserPhotoResponseModel();
                if (response.IsSuccessful != false)
                {
                    objres = JsonConvert.DeserializeObject<UpdateUserPhotoResponseModel>(response.RawResult);
                    if (objres.status != "0")
                    {
                        success.Invoke(objres);
                    }
                    else
                        failed.Invoke(objres);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    failed.Invoke(obj);
                }
            }
            catch (Exception exception)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Something went wrong.  Please restart the app and try again.", "", "OK");
            }
            return resmodel;
        }
        #endregion

        #region Contact Apis
        /// <summary>
        /// TODO:To Call The UploadPhoneContactList Api...   
        /// </summary>
        /// <param name="request"></param>
        /// <param name="success"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        public async Task<ContactResponseModel> UploadPhoneContactListApi(List<ContactUploadRequestModel> request, Action<object> success, Action<object> failed)
        {
            ContactResponseModel resmodel = new ContactResponseModel();
            try
            {
                var url = string.Format("{0}SaveContact/UploadPhoneContactList?StaffID=" + Helpers.Settings.GeneralStaffId + "&CustomerID=" + Helpers.Settings.GeneralCustomerId, WebServiceDetails.BaseUri);
                var dic = new Dictionary<string, string>();
                dic.Add("Authorization", "Bearer " + Helpers.Settings.GeneralAccessToken);
                var result = _apiProvider.Post<ContactResponseModel, List<ContactUploadRequestModel>>(url, request, dic);
                var response = result.Result;
                ContactResponseModel objres = null;
                dynamic obj = "";
                ContactResponseModel reg = new ContactResponseModel();
                if (response.IsSuccessful != false)
                {
                    objres = JsonConvert.DeserializeObject<ContactResponseModel>(response.RawResult);
                    if (objres.status != "0")
                    {
                        success.Invoke(objres);
                    }
                    else
                        failed.Invoke(objres);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    failed.Invoke(obj);
                }
            }
            catch (Exception exception)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Something went wrong.  Please restart the app and try again.", "", "OK");
            }
            return resmodel;
        }

        /// <summary>
        /// TODO:To Call The GetPhoneContactList Api...
        /// </summary>
        /// <param name="success"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        public async Task<List<GetPhoneContactListResponseModel>> GetPhoneContactListApi(Action<object> success, Action<object> failed)
        {
            List<GetPhoneContactListResponseModel> resmodel = new List<GetPhoneContactListResponseModel>();
            try
            {
                var url = string.Format("{0}SaveContact/GetPhoneContactList?StaffID=" + Helpers.Settings.GeneralStaffId + "&CustomerID=" + Helpers.Settings.GeneralCustomerId + "&UserID=" + Helpers.Settings.GeneralUserID, WebServiceDetails.BaseUri);
                var dic = new Dictionary<string, string>();
                dic.Add("Authorization", "Bearer " + Helpers.Settings.GeneralAccessToken);
                var result = _apiProvider.Get<List<GetPhoneContactListResponseModel>>(url, dic);
                var response = result.Result;
                List<GetPhoneContactListResponseModel> objres = null;
                dynamic obj = "";
                List<GetPhoneContactListResponseModel> reg = new List<GetPhoneContactListResponseModel>();
                if (result.IsSuccessful != false)
                {
                    objres = JsonConvert.DeserializeObject<List<GetPhoneContactListResponseModel>>(result.RawResult);
                    success.Invoke(objres);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    failed.Invoke(obj);
                }
            }
            catch (Exception exception)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Something went wrong.  Please restart the app and try again.", "", "OK");
            }
            return resmodel;
        }

        /// <summary>
        /// TODO:To Call The EditPhoneContact Api...
        /// </summary>
        /// <param name="request"></param>
        /// <param name="success"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        public async Task<EditPhoneContactResponseModel> EditPhoneContactApi(EditPhoneContactRequestModel request, Action<object> success, Action<object> failed)
        {
            EditPhoneContactResponseModel resmodel = new EditPhoneContactResponseModel();
            try
            {
                var url = string.Format("{0}SaveContact/EditPhoneContact", WebServiceDetails.BaseUri);
                var dic = new Dictionary<string, string>();
                dic.Add("Authorization", "Bearer " + Helpers.Settings.GeneralAccessToken);
                var result = _apiProvider.Post<EditPhoneContactResponseModel, EditPhoneContactRequestModel>(url, request, dic);
                var response = result.Result;
                EditPhoneContactResponseModel objres = null;
                dynamic obj = "";
                EditPhoneContactResponseModel reg = new EditPhoneContactResponseModel();
                if (response.IsSuccessful != false)
                {
                    objres = JsonConvert.DeserializeObject<EditPhoneContactResponseModel>(response.RawResult);
                    if (objres.status != "0")
                    {
                        success.Invoke(objres);
                    }
                    else
                        failed.Invoke(objres);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    failed.Invoke(obj);
                }
            }
            catch (Exception exception)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Something went wrong.  Please restart the app and try again.", "", "OK");
            }
            return resmodel;
        }

        /// <summary>
        /// TODO:To Call The DeletePhoneContact Api...
        /// </summary>
        /// <param name="request"></param>
        /// <param name="success"></param>
        /// <param name="failed"></param>
        /// <returns></returns>
        public async Task<DeletePhoneContactResponseModel> DeletePhoneContactApi(DeletePhoneContactRequestModel request, Action<object> success, Action<object> failed)
        {
            DeletePhoneContactResponseModel resmodel = new DeletePhoneContactResponseModel();
            try
            {
                var url = string.Format("{0}SaveContact/DeletePhoneContact", WebServiceDetails.BaseUri);
                var dic = new Dictionary<string, string>();
                dic.Add("Authorization", "Bearer " + Helpers.Settings.GeneralAccessToken);
                var result = _apiProvider.Post<DeletePhoneContactResponseModel, DeletePhoneContactRequestModel>(url, request, dic);
                var response = result.Result;
                DeletePhoneContactResponseModel objres = null;
                dynamic obj = "";
                DeletePhoneContactResponseModel reg = new DeletePhoneContactResponseModel();
                if (response.IsSuccessful != false)
                {
                    objres = JsonConvert.DeserializeObject<DeletePhoneContactResponseModel>(response.RawResult);
                    if (objres.status != "0")
                    {
                        success.Invoke(objres);
                    }
                    else
                        failed.Invoke(objres);
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    failed.Invoke(obj);
                }
            }
            catch (Exception exception)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Something went wrong.  Please restart the app and try again.", "", "OK");
            }
            return resmodel;
        }
        #endregion
        #endregion
    }

}
