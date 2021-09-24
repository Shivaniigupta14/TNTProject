using System;
using System.Collections.Generic;
using System.Text;

namespace TNTapp.Models.Contact
{
    public class EditPhoneContactRequestModel
    {
        public int VipID { get; set; }
        public int CustomerID { get; set; }
        public int StaffID { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string BirthMonth { get; set; }
        public string BirthDayOfMonth { get; set; }
        public string Bucket { get; set; }
    }

    public class EditPhoneContactResponseModel
    {
        public string responseString { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string errorMessage { get; set; }
    }

    public class DeletePhoneContactRequestModel
    {
        public int VipID { get; set; }
        public int CustomerID { get; set; }
        public int StaffID { get; set; }
    }

    public class DeletePhoneContactResponseModel
    {
        public string responseString { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string errorMessage { get; set; }
    }
}
