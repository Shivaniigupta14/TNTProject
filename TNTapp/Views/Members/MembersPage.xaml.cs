using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using TNTapp.Models.Contact;
using TNTapp.ViewModels.Home;
using TNTapp.ViewModels.PopUp;
using TNTapp.Views.PopUp;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace TNTapp.Views.Members
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MembersPage : ContentPage
    {
        //Define Local Variables Here... 
        public MembersPageViewModel MembersVM;
        ContactEditPopUpPage ContactEditPopUpPage;
        ObservableCollection<Xamarin.Essentials.Contact> contactsList = new ObservableCollection<Xamarin.Essentials.Contact>();

        #region Constructor
        public MembersPage()
        {
            InitializeComponent();
            // iOS Platform
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            MembersVM = new MembersPageViewModel(this.Navigation);
            this.BindingContext = MembersVM;
        }
        #endregion

        #region Event Handler
        /// <summary>
        /// TODO:To define the page on appearing event...
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await MembersVM.GetContactListFromDevice();
            await MembersVM.GetContactListFromServer();
        }

        /// <summary>
        /// TODO:To Define the Contactlist Item tapped event...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ContactList_Tapped(object sender, EventArgs e)
        {
            var contact = (sender as Grid).BindingContext as GetPhoneContactListResponseModel;
            if (contact != null)
            {
                MembersVM.SelectedContactListItem = contact;
                ContactEditPopUpPage = new ContactEditPopUpPage(contact, this);
                await Navigation.PushPopupAsync(ContactEditPopUpPage, true);
            }
        }

        /// <summary>
        /// TODO:To Define the Delete tapped event...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Delete_Tapped(object sender, EventArgs e)
        {
            var _selectcontact = (sender as Grid).BindingContext as GetPhoneContactListResponseModel;
            if (_selectcontact != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("", "Do you really want to delete this contact?", "Yes", "No");
                    if (result)
                    {
                        await MembersVM.DeletePhoneContactAsync(_selectcontact);
                    }
                });
            }
        }

        /// <summary>
        /// TODO:To define the contact update list...
        /// </summary>
        /// <param name="editPhoneContact"></param>
        /// <returns></returns>
        public async Task UpdateContact(EditPhoneContactRequestModel editPhoneContact)
        {
            await MembersVM.EditPhoneContactAsync(editPhoneContact);
        }
        /// <summary>
        /// TODO:To define the search of contact based on firstname
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchString2_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterName();
        }
        private void FilterName()
        {
            try
            {
                var abc = MembersVM.PhoneContactList;
                string filterserch = SearchString2.Text;             
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
        }
        #endregion

    }
}