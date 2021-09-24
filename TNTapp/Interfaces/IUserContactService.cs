using System;
using System.Collections.Generic;
using System.Text;

namespace TNTapp.Interfaces
{
    public interface IUserContactService
    {
        IEnumerable<MobileUserContact> GetPhoneContacts();
    }

    public class MobileUserContact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string ImageUri { get; set; }
        public string Base64 { get; set; }

        public string EmergencyContactImage
        {
            get
            {
                string image = string.Empty;
                if (!string.IsNullOrEmpty(ImageUri))
                    image = ImageUri;
                else
                    image = "profile.png";
                return image;
            }
        }
    }
}
