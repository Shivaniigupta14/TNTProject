using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contacts;
using Foundation;
using TNTapp.Interfaces;
using TNTapp.iOS.Dependencies;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserContactService))]
namespace TNTapp.iOS.Dependencies
{
    class UserContactService : IUserContactService
    {
        public UserContactService()
        { }
        public const string ThumbnailPrefix = "thumb";
        public IEnumerable<MobileUserContact> GetPhoneContacts()
        {
            try
            {
                var contacts = new List<MobileUserContact>();

                NSError error = null;
                var keysToFetch = new[]
                {
                    CNContactKey.PhoneNumbers, CNContactKey.GivenName, CNContactKey.FamilyName, CNContactKey.EmailAddresses,
                    CNContactKey.ImageDataAvailable, CNContactKey.ThumbnailImageData
                };

                var request = new CNContactFetchRequest(keysToFetch: keysToFetch);
                request.SortOrder = CNContactSortOrder.GivenName;

                using (var store = new CNContactStore())
                {
                    store.EnumerateContacts(request, out error, new CNContactStoreListContactsHandler(
                        (CNContact c, ref bool stop) =>
                        {                          
                            var contact = new MobileUserContact
                            {
                                Name = string.IsNullOrEmpty(c.FamilyName) ? c.GivenName : $"{c.GivenName} {c.FamilyName}",
                                Number = c.PhoneNumbers.Any() ? c.PhoneNumbers[0].Value.StringValue : string.Empty,
                                
                            };
                            contacts.Add(contact);
                        }));
                }

                return contacts;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}