using System;
using System.Collections.Generic;
using System.Text;

namespace TNTapp.Models.Account
{
    public class ForgotPasswordRequestModel
    {
        public string UserName { get; set; }
        public string NewPassword { get; set; }
        public string Email { get; set; }
    }

    public class ForgotPasswordResponseModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public string errorMessage { get; set; }
    }
}
