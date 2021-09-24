using System;
using System.Collections.Generic;
using System.Text;

namespace TNTapp.Models.Account
{
    public class LoginResponseModel
    {
        public int staffID { get; set; }
        public string userID { get; set; }
        public string emailAddress { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string printName { get; set; }
        public string staffType { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string alternatePhone { get; set; }
        public string title { get; set; }
        public string primaryPhone { get; set; }
        public string website { get; set; }
        public string nmlsNumber { get; set; }
        public int customerID { get; set; }
        public string token { get; set; }
        public string responseString { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string errorMessage { get; set; }
        public string companyLogo { get; set; }

    }

    public class LoginRequestModel
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}
