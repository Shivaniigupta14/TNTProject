using System;
using System.Collections.Generic;
using System.Text;

namespace TNTapp.Models.Account
{ 
    public class SignUpRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string CellNumber { get; set; }
        public string CompanyName { get; set; }
        public string ReferredBy { get; set; }
        public string Title { get; set; }
        public string OfficePhone { get; set; }
        public string Website { get; set; }
        public string NMLSnumber { get; set; }
        public string Logo { get; set; }
    }

    public class SignUpResponseModel
    {
        public int signUpID { get; set; }
        public object userID { get; set; }
        public string emailAddress { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public object printName { get; set; }
        public string username { get; set; }
        public string cellNumber { get; set; }
        public string title { get; set; }
        public string officeNumber { get; set; }
        public string website { get; set; }
        public string responseString { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string errorMessage { get; set; }
    }

}
