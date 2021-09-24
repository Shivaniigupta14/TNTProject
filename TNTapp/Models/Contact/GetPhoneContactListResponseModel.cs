using System;
using System.Collections.Generic;
using System.Text;

namespace TNTapp.Models.Contact
{
    public class GetPhoneContactListResponseModel
    {
        public int? vipLoadId { get; set; }
        public int? vipID { get; set; }
        public int? customerID { get; set; }
        public int? staffID { get; set; }
        public string phoneNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string FullName
        {
            get
            {
                string _firstName = string.Empty;
                _firstName = firstName + " " + lastName;
                return _firstName;
            }
        }
        public string email { get; set; }
        // public DateTime? dateOfBirth { get; set; }
        public string loadStatus { get; set; }
        public string allPhoneNumbers { get; set; }
        public string responseString { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string errorMessage { get; set; }
        public int birthMonth { get; set; }
        public int birthDayOfMonth { get; set; }
        public string bucket { get; set; }

    }
}
