using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TNTapp.Models.Account;
using TNTapp.Models.Contact;

namespace TNTapp.BussinessCode
{
    public interface IBusinessCode
    {
        #region Apis Declarations 

        #region Account Apis
        Task<LoginResponseModel> LoginApi(LoginRequestModel request, Action<object> success, Action<object> failed);
        Task<ChangePasswordResponseModel> ChangePasswordApi(ChangePasswordRequestModel request, Action<object> success, Action<object> failed);
        Task<SignUpResponseModel> SignUpApi(SignUpRequestModel request, Action<object> success, Action<object> failed);
        Task<ForgotPasswordResponseModel> ForgotPasswordApi(ForgotPasswordRequestModel request, Action<object> success, Action<object> failed);
        #endregion

        #region Profile Apis
        Task<GetUserPhotoResponseModel> GetUserPhotoApi(Action<object> success, Action<object> failed);
        Task<GetUserProfileResponseModel> GetUserProfileApi(Action<object> success, Action<object> failed);
        Task<UpdateProfileResponseModel> UpdateProfileApi(UpdateProfileRequestModel request, Action<object> success, Action<object> failed);
        Task<UpdateUserPhotoResponseModel> UpdateUserPhotoApi(UpdateUserPhotoRequestModel request, Action<object> success, Action<object> failed);

        #endregion

        #region Contacts Apis
        Task<ContactResponseModel> UploadPhoneContactListApi(List<ContactUploadRequestModel> request, Action<object> success, Action<object> failed);
        Task<EditPhoneContactResponseModel> EditPhoneContactApi(EditPhoneContactRequestModel request, Action<object> success, Action<object> failed);
        Task<DeletePhoneContactResponseModel> DeletePhoneContactApi(DeletePhoneContactRequestModel request, Action<object> success, Action<object> failed);
        Task<List<GetPhoneContactListResponseModel>> GetPhoneContactListApi(Action<object> success, Action<object> failed);
        #endregion

        #endregion
    }
}
