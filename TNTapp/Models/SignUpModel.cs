using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TNTapp.Models
{
    public class SignUpModel : BindableObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string OfficePhoneNumber { get; set; }
        public string Website { get; set; }
        public string NMLS { get; set; }
        public string CustomerId { get; set; }
        public string StaffId { get; set; }
        public string ReferredBy { get; set; }
    }
}
