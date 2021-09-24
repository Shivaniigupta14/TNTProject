using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using TNTapp.Models.Contact;
using TNTapp.ViewModels.PopUp;
using TNTapp.Views.Members;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TNTapp.Views.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactEditPopUpPage : PopupPage
    {
        ContactEditPopUpPageVM ContactEditPopUpVM;
        GetPhoneContactListResponseModel MobileUserContact;
        MembersPage MembersPage;
        public event EventHandler DialogClosed;
        public event EventHandler DialogShow;
        public event EventHandler DialogClosing;
        public event EventHandler DialogShowing;
        public ContactEditPopUpPage(GetPhoneContactListResponseModel _mobileUserContact, MembersPage membersPage)
        {
            InitializeComponent();
            ContactEditPopUpVM = new ContactEditPopUpPageVM(this.Navigation);
            this.BindingContext = ContactEditPopUpVM;
            MembersPage = membersPage;
            if (_mobileUserContact != null)
            {
                MobileUserContact = _mobileUserContact;
                ContactEditPopUpVM.FirstName = _mobileUserContact.firstName;
                ContactEditPopUpVM.LastName = _mobileUserContact.lastName;
                ContactEditPopUpVM.EmailAddress = _mobileUserContact.email;
                ContactEditPopUpVM.PhoneNumber = _mobileUserContact.phoneNumber;
                ContactEditPopUpVM.BirthDayOfMonth = MobileUserContact.birthDayOfMonth == 0 ? "Birth Day of Month" : MobileUserContact.birthDayOfMonth.ToString();
                ContactEditPopUpVM.BirthMonth = MobileUserContact.birthMonth == 0 ? "Birthday Month" : MobileUserContact.birthMonth.ToString();
                ContactEditPopUpVM.BucketOfItem = MobileUserContact.bucket == string.Empty ? "Select Bucket of item" : MobileUserContact.bucket.ToString();
            }
        }
        private void ShowDialogAnimation(Xamarin.Forms.VisualElement dialog, Xamarin.Forms.VisualElement bg)
        {
            dialog.TranslationY = bg.Height;
            bg.IsVisible = true;
            dialog.IsVisible = true;

            ////ANIMATIONS 
            var showBgAnimation = OpacityAnimation(bg, 0, 0.5);
            var showDialogAnimation = TransLateYAnimation(dialog, bg.Height, 0);

            ////EXECUTE ANIMATIONS
            this.Animate("showBg", showBgAnimation, 16, 200, Easing.Linear, (d, f) => { });
            this.Animate("showMenu", showDialogAnimation, 16, 200, Easing.Linear, (d, f) =>
            {
                OnDialogShow(new EventArgs());
            });
            OnDialogShowing(new EventArgs());
        }
        private async void HideDialogAnimation(Xamarin.Forms.VisualElement dialog, Xamarin.Forms.VisualElement bg)
        {
            //ANIMATIONS     
            var hideBgAnimation = OpacityAnimation(bg, 0.5, 0);
            var showDialogAnimation = TransLateYAnimation(dialog, 0, bg.Height);

            ////EXECUTE ANIMATIONS
            this.Animate("hideBg", hideBgAnimation, 16, 200, Easing.Linear, (d, f) => { });
            this.Animate("hideMenu", showDialogAnimation, 16, 200, Easing.Linear, (d, f) =>
            {
                bg.IsVisible = false;
                dialog.IsVisible = false;
                dialog.TranslationY = PopUpBgLayout.Height;

                OnDialogClosing(new EventArgs());
            });
            await Navigation.PopAllPopupAsync(true);

            OnDialogClosing(new EventArgs());
        }
        public void ShowDialog()
        {
            ShowDialogAnimation(PopUpDialogLayout, PopUpBgLayout);
        }
        public void HideDialog()
        {
            HideDialogAnimation(PopUpDialogLayout, PopUpBgLayout);
        }
        protected virtual void OnDialogClosed(EventArgs e)
        {
            DialogClosed?.Invoke(this, e);
        }
        protected virtual void OnDialogShow(EventArgs e)
        {
            DialogShow?.Invoke(this, e);
        }
        protected virtual void OnDialogClosing(EventArgs e)
        {
            DialogClosing?.Invoke(this, e);
        }
        protected virtual void OnDialogShowing(EventArgs e)
        {
            DialogShowing?.Invoke(this, e);
        }
        private static Animation TransLateYAnimation(Xamarin.Forms.VisualElement element, double from, double to)
        {
            return new Animation(d => { element.TranslationY = d; }, from, to);
        }
        private static Animation TransLateXAnimation(Xamarin.Forms.VisualElement element, double from, double to)
        {
            return new Animation(d => { element.TranslationX = d; }, from, to);
        }
        private static Animation OpacityAnimation(Xamarin.Forms.VisualElement element, double from, double to)
        {
            return new Animation(d => { element.Opacity = d; }, from, to);
        }

        #region Event Handlers

        /// <summary>
        /// TODO : To Define Cancel Button Tapped Event...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Delete_Tapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("", "Do you really want to delete this contact?", "Yes", "No");
                if (result)
                {
                    HideDialog();
                    await MembersPage.MembersVM.DeletePhoneContactAsync(MobileUserContact);
                }
            });
        }

        /// <summary>
        /// TODO : To Define Save Button Tapped Event...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Save_Tapped(object sender, EventArgs e)
        {
            HideDialog();
            EditPhoneContactRequestModel editPhoneContact = new EditPhoneContactRequestModel();
            editPhoneContact.VipID = (int)MobileUserContact.vipID;
            editPhoneContact.CustomerID = (int)MobileUserContact.customerID;
            editPhoneContact.StaffID = (int)MobileUserContact.staffID;
            editPhoneContact.FirstName = ContactEditPopUpVM.FirstName;
            editPhoneContact.LastName = ContactEditPopUpVM.LastName;
            editPhoneContact.Email = ContactEditPopUpVM.EmailAddress == null ? string.Empty : ContactEditPopUpVM.EmailAddress;
            editPhoneContact.PhoneNumber = ContactEditPopUpVM.PhoneNumber;
            editPhoneContact.BirthMonth = ContactEditPopUpVM.BirthMonth == "Birthday Month" ? string.Empty : ContactEditPopUpVM.BirthMonth;
            editPhoneContact.BirthDayOfMonth = ContactEditPopUpVM.BirthDayOfMonth == "Birth Day of Month" ? string.Empty : ContactEditPopUpVM.BirthDayOfMonth;
            editPhoneContact.Bucket = ContactEditPopUpVM.BucketOfItem == "Select Bucket of item" ? string.Empty : ContactEditPopUpVM.BucketOfItem;
            await MembersPage.UpdateContact(editPhoneContact);
        }


        /// <summary>
        /// TODO : To Define BirthMonth Tapped Event...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BirthMonth_Tapped(object sender, EventArgs e)
        {
            pckBirthMonth.Focus();
        }

        /// <summary>
        /// TODO : To Define BirthDayOfMonth Tapped Event...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BirthDayOfMonth_Tapped(object sender, EventArgs e)
        {
            pckBirthDayOfMonth.Focus();
        }

        private void PckBirthMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pckBirthMonth.SelectedIndex != -1)
            {
            }
        }
        /// <summary>
        /// TODO:To Define BucketItem Tapped Event...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bucket_Item_Tapped(object sender, EventArgs e)
        {
            pckBucketitem.Focus();

        }
        private void pckBucketitem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pckBucketitem.SelectedIndex != -1)
            {

            }
        }

        /// <summary>
        /// TODO:To Define The Close PopUp Button Tapped Event...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Tapped(object sender, EventArgs e)
        {
            HideDialog();
        }
        #endregion
    }
}