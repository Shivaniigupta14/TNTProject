using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TNTapp.Models
{
    public class UpdateProfileModel : BindableObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Title { get; set; }
        public string OfficePhoneNumber { get; set; }
        public string Website { get; set; }
        public string NMLS { get; set; }
        public string ReferredBy { get; set; }
    }
}
