using System;
using System.Collections.Generic;
using System.Text;

namespace TNTapp.Models.Account
{
    public class UpdateUserPhotoRequestModel
    {
        public string Username { get; set; }
        public string UserID { get; set; }
        public string PhotoString { get; set; }
    }

    public class UpdateUserPhotoResponseModel
    {
        public string responseString { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string errorMessage { get; set; }
    }
}
