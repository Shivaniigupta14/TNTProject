using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TNTapp.ViewModels.PopUp
{
    public class ContactEditPopUpPageVM : BaseViewModel
    {
        //Define Local Variables Here...
        public ContactEditPopUpPageVM(INavigation nav)
        {
            Navigation = nav;
        }
        #region Properties

        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if (_FirstName != value)
                {
                    _FirstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }

        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (_LastName != value)
                {
                    _LastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        private string _PhoneNumber;
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set
            {
                if (_PhoneNumber != value)
                {
                    _PhoneNumber = value;
                    OnPropertyChanged("PhoneNumber");
                }
            }
        }
        private string _EmailAddress;
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set
            {
                if (_EmailAddress != value)
                {
                    _EmailAddress = value;
                    OnPropertyChanged("EmailAddress");
                }
            }
        }
        private string _DOB = "Date of birth";
        public string DOB
        {
            get { return _DOB; }
            set
            {
                if (_DOB != value)
                {
                    _DOB = value;
                    OnPropertyChanged("DOB");
                }
            }
        }

        private DateTime _SelectDOB = DateTime.Now;
        public DateTime SelectDOB
        {
            get { return _SelectDOB; }
            set
            {
                if (_SelectDOB != value)
                {
                    _SelectDOB = value;
                    OnPropertyChanged("SelectDOB");
                }
            }
        }

        private string _BirthMonth = "Birthday Month";
        public string BirthMonth
        {
            get { return _BirthMonth; }
            set
            {
                if (_BirthMonth != value)
                {
                    _BirthMonth = value;
                    OnPropertyChanged("BirthMonth");
                }
            }
        }

        private string _BirthDayOfMonth = "Birth Day of Month";
        public string BirthDayOfMonth
        {
            get { return _BirthDayOfMonth; }
            set
            {
                if (_BirthDayOfMonth != value)
                {
                    _BirthDayOfMonth = value;
                    OnPropertyChanged("BirthDayOfMonth");
                }
            }

        }

        private string _BucketOfItem = "Select Bucket of item";
        public string BucketOfItem
        {
            get { return _BucketOfItem; }
            set
            {
                if (_BucketOfItem != value)
                {
                    _BucketOfItem = value;
                    OnPropertyChanged("BucketOfItem");
                }
            }
        }
        #endregion

       
    }
}
