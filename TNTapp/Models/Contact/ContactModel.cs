using System;
using System.Collections.Generic;
using System.Text;

namespace TNTapp.Models.Contact
{
    public class ContactModel
    {
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AllPhoneNumbers { get; set; }
        public bool IsSync { get; set; }
    }

    public class ContactRequestModel
    {
        public List<ContactUploadRequestModel> contactList { get; set; }
    }

    public class ContactUploadRequestModel
    {
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string AllPhoneNumbers { get; set; }

    }

    public class ContactResponseModel
    {
        public string responseString { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string errorMessage { get; set; }
    }


}
