using System;
using System.Collections.Generic;
using System.Text;

namespace TNTapp.Models.Account
{
    public class ChangePasswordRequestModel
    {
        public string Username { get; set; }
        public string EmailID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordResponseModel
    {
        public string responseString { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string errorMessage { get; set; }
    }
}
