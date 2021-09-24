using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TNTapp.Models.Account
{
    public class UpdateProfileRequestModel
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CellNumber { get; set; }
        public string ReferredBy { get; set; }
        public string Title { get; set; }
        public string OfficePhone { get; set; }
        public string Website { get; set; }
        public string NMLSnumber { get; set; }
    }
    public class UpdateProfileResponseModel
    {
        public object firstName { get; set; }
        public object lastName { get; set; }
        public object cellNumber { get; set; }
        public object title { get; set; }
        public object officeNumber { get; set; }
        public object website { get; set; }
        public object referredBy { get; set; }
        public object nmlSnumber { get; set; }
        public string responseString { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string errorMessage { get; set; }
    }

    public class GetUserPhotoRequestModel
    {
        public string UserID { get; set; }
        public string Username { get; set; }
    }

    public class GetUserPhotoResponseModel
    {
        public string ResponseString { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public string ImageBase64 { get; set; }
    }

    public class GetUserProfileResponseModel 
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string cellNumber { get; set; }
        public string title { get; set; }
        public string officeNumber { get; set; }
        public string website { get; set; }
        public string referredBy { get; set; }
        public string nmlSnumber { get; set; }
        public string responseString { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string errorMessage { get; set; }
    }
}
