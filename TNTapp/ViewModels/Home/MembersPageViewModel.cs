using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Newtonsoft.Json;
using Plugin.Permissions;
using TNTapp.Common;
using TNTapp.Interfaces;
using TNTapp.Models.Contact;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.Connectivity;

namespace TNTapp.ViewModels.Home
{
    public class MembersPageViewModel : BaseViewModel
    {
        //Define Local variable
        int i = 0;
        List<ContactModel> _contactLst = new List<ContactModel>();
        List<ContactModel> _ConLst = new List<ContactModel>();
        #region Constructor
        public MembersPageViewModel(INavigation nav)
        {
            Navigation = nav;
            SyncCommand = new Command(SyncCommandAsync);
        }

        #endregion

        #region properties
        private bool _IsContactListNotVisible = false;
        public bool IsContactListNotVisible
        {
            get { return _IsContactListNotVisible; }
            set
            {
                if (_IsContactListNotVisible != value)
                {
                    _IsContactListNotVisible = value;
                    OnPropertyChanged("IsContactListNotVisible");
                }
            }
        }

        private bool _IsContactListVisible = false;
        public bool IsContactListVisible
        {
            get { return _IsContactListVisible; }
            set
            {
                if (_IsContactListVisible != value)
                {
                    _IsContactListVisible = value;
                    OnPropertyChanged("IsContactListVisible");
                }
            }
        }

        private ObservableCollection<GetPhoneContactListResponseModel> _PhoneContactList;
        public ObservableCollection<GetPhoneContactListResponseModel> PhoneContactList
        {
            get { return _PhoneContactList; }
            set
            {
                if (_PhoneContactList != value)
                {
                    _PhoneContactList = value;
                    OnPropertyChanged("PhoneContactList");
                }
            }
        }

        private ObservableCollection<Xamarin.Essentials.Contact> _ContactsList;
        public ObservableCollection<Xamarin.Essentials.Contact> ContactsList
        {
            get { return _ContactsList; }
            set
            {
                if (_ContactsList != value)
                {
                    _ContactsList = value;
                    OnPropertyChanged("ContactsList");
                }
            }
        }

        private ObservableCollection<MobileUserContact> _ContactDetailList;
        public ObservableCollection<MobileUserContact> ContactDetailList
        {
            get { return _ContactDetailList; }
            set
            {
                if (_ContactDetailList != value)
                {
                    _ContactDetailList = value;
                    OnPropertyChanged("ContactDetailList");
                }
            }
        }

        private ObservableCollection<ContactModel> _TempContactDetailList;
        public ObservableCollection<ContactModel> TempContactDetailList
        {
            get { return _TempContactDetailList; }
            set
            {
                if (_TempContactDetailList != value)
                {
                    _TempContactDetailList = value;
                    OnPropertyChanged("TempContactDetailList");
                }
            }
        }

        private ObservableCollection<ContactModel> _TempNewContactDetailList;
        public ObservableCollection<ContactModel> TempNewContactDetailList
        {
            get { return _TempNewContactDetailList; }
            set
            {
                if (_TempNewContactDetailList != value)
                {
                    _TempNewContactDetailList = value;
                    OnPropertyChanged("TempNewContactDetailList");
                }
            }
        }

        private GetPhoneContactListResponseModel _SelectedContactListItem;
        public GetPhoneContactListResponseModel SelectedContactListItem
        {
            get { return _SelectedContactListItem; }
            set
            {
                if (_SelectedContactListItem != value)
                {
                    _SelectedContactListItem = value;
                    OnPropertyChanged("SelectedContactListItem");
                }
            }
        }

        #endregion

        #region Command
        public Command SyncCommand { get; }
        #endregion

        #region Methods

        /// <summary>0
        /// TODO:To Call The GetPhoneContactList From Device...
        /// </summary>
        /// <returns></returns>
        public async Task GetContactListFromDevice()
        {
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Contacts);
            if (permissionStatus != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                if (i == 0)
                {
                    i++;
                    AppPermission permission = new AppPermission();
                    Tuple<string, bool> res = await permission.GetContactAccessPermission();
                }
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Task.Run(async () =>
                    {
                        UserDialogs.Instance.ShowLoading();
                        try
                        {
                            var contacts = await Contacts.GetAllAsync();
                            ContactsList = new ObservableCollection<Xamarin.Essentials.Contact>();
                            foreach (var contact in contacts)
                            {
                                if ((contact.Phones.Count > 0) && !string.IsNullOrEmpty(contact.DisplayName))
                                {
                                    ContactsList.Add(contact);
                                }
                            }

                            List<ContactModel> _contactLst = new List<ContactModel>();
                            List<ContactModel> _newContactLst = new List<ContactModel>();
                            foreach (var item in ContactsList)
                            {
                                ContactModel ContactModel = new ContactModel();
                                ContactModel.PhoneNumber = item.Phones[0].ToString();
                                ContactModel.EmailAddress = item.Emails.Count == 0 ? string.Empty : string.Join(",", item.Emails);
                                ContactModel.FirstName = item.GivenName == null ? string.Empty : item.GivenName;
                                ContactModel.LastName = item.FamilyName == null ? string.Empty : item.FamilyName;
                                ContactModel.FullName = item.DisplayName;
                                ContactModel.DateOfBirth = DateTime.Now;
                                ContactModel.AllPhoneNumbers = item.Phones.Count == 0 ? string.Empty : string.Join(",", item.Phones);
                                ContactModel.IsSync = false;
                                _contactLst.Add(ContactModel);
                            }
                            _ConLst = _contactLst.OrderBy(a => a.FullName).ToList();
                            TempContactDetailList = new System.Collections.ObjectModel.ObservableCollection<ContactModel>(_ConLst);
                        }
                        catch (Exception ex)
                        {
                            var msg = ex.Message;
                        }
                        UserDialogs.Instance.HideLoading();

                    }).ConfigureAwait(false);
                });
            }
        }

        /// <summary>
        /// TODO:To Call The GetPhoneContactList Api...
        /// </summary>
        /// <returns></returns>
        public async Task GetContactListFromServer()
        {
            //Call api...
            try
            {
                UserDialogs.Instance.ShowLoading("Loading…", MaskType.Clear);
                if (CrossConnectivity.Current.IsConnected)
                {
                    await Task.Run(async () =>
                    {
                        if (_businessCode != null)
                        {
                            await _businessCode.GetPhoneContactListApi(
                            async (obj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestList = (obj as List<GetPhoneContactListResponseModel>);
                                    if (requestList != null && requestList.Count > 0)
                                    {
                                        try
                                        {
                                            PhoneContactList = new ObservableCollection<GetPhoneContactListResponseModel>(requestList);
                                            if (PhoneContactList.Count > 0)
                                            {
                                                IsContactListVisible = true;
                                                IsContactListNotVisible = false;
                                            }
                                            else
                                            {
                                                IsContactListVisible = false;
                                                IsContactListNotVisible = true;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await UserDialogs.Instance.AlertAsync(ex.StackTrace, "Success", "OK");
                                        }
                                    }
                                    UserDialog.HideLoading();
                                });
                            }, (objj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    UserDialog.HideLoading();
                                    UserDialog.Alert("Something went wrong.  Please restart the app and try again.", "", "Ok");
                                });
                            });
                        }
                    }).ConfigureAwait(false);
                }
                else
                {
                    UserDialogs.Instance.Loading().Hide();
                    await UserDialogs.Instance.AlertAsync("No Network Connection found, Please try again!", "", "Okay");
                }
            }
            catch (Exception ex)
            {
                UserDialog.HideLoading();
            }
        }
        /// <summary>
        /// TODO : To Define The Sync Command...
        /// </summary>
        /// <param name="objs"></param>
        private async void SyncCommandAsync(object objs)
        {
            //Call api..
            try
            {
                UserDialogs.Instance.ShowLoading("Syncing contacts…", MaskType.Clear);
                if (CrossConnectivity.Current.IsConnected)
                {
                    await Task.Run(async () =>
                    {
                        if (_businessCode != null)
                        {
                            List<ContactUploadRequestModel> NewRequestContactList = new List<ContactUploadRequestModel>();

                            foreach (var item in TempContactDetailList)
                            {
                                ContactUploadRequestModel ContactModel = new ContactUploadRequestModel();
                                ContactModel.PhoneNumber = item.PhoneNumber;
                                ContactModel.EmailAddress = item.EmailAddress;
                                ContactModel.FirstName = item.FirstName;
                                ContactModel.LastName = item.LastName;
                                ContactModel.DateOfBirth = null;
                                ContactModel.AllPhoneNumbers = item.AllPhoneNumbers;
                                NewRequestContactList.Add(ContactModel);
                                item.IsSync = true;
                            }
                            NewRequestContactList = NewRequestContactList.OrderBy(a => a.FirstName).ToList();
                            await _businessCode.UploadPhoneContactListApi(NewRequestContactList,
                            async (obj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestList = (obj as ContactResponseModel);
                                    if (requestList != null)
                                    {
                                        try
                                        {
                                            var alertConfig = new AlertConfig
                                            {
                                                Message = TempContactDetailList.Count.ToString() + " " + "of your contacts have phone numbers and were successfully synced.Your members list will be updated shortly.Thank you!",
                                                OkText = "Ok",
                                                OnAction = async () =>
                                                {
                                                    var json = JsonConvert.SerializeObject(TempContactDetailList);
                                                    Helpers.Settings.GeneralGetAllSyncContactListJason = json;
                                                }
                                            };
                                            UserDialogs.Instance.Alert(alertConfig);
                                        }
                                        catch (Exception ex)
                                        {
                                            await UserDialogs.Instance.AlertAsync(ex.StackTrace, "Success", "OK");
                                        }
                                    }
                                    UserDialog.HideLoading();
                                });
                            }, (objj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestLists = (objj as ContactResponseModel);
                                    if (requestLists != null)
                                    {
                                        UserDialog.HideLoading();
                                        UserDialog.Alert(requestLists.errorMessage, "", "Ok");
                                    }
                                    else
                                    {
                                        UserDialog.HideLoading();
                                        UserDialog.Alert("Something went wrong.  Please restart the app and try again.", "", "Ok");
                                    }
                                });
                            });
                        }
                    }).ConfigureAwait(false);
                }
                else
                {
                    UserDialogs.Instance.Loading().Hide();
                    await UserDialogs.Instance.AlertAsync("No Network Connection found, Please try again!", "", "Okay");
                }
            }
            catch (Exception ex)
            {
                UserDialog.HideLoading();
            }
        }
        
        /// <summary>
        /// TODO:To Call The EditPhoneContact Api...
        /// </summary>
        /// <returns></returns>
        public async Task EditPhoneContactAsync(EditPhoneContactRequestModel editPhoneContact)
        {
            //Call api...
            try
            {
                UserDialogs.Instance.ShowLoading("Loading…", MaskType.Clear);
                if (CrossConnectivity.Current.IsConnected)
                {
                    await Task.Run(async () =>
                    {
                        if (_businessCode != null)
                        {
                            await _businessCode.EditPhoneContactApi(new EditPhoneContactRequestModel()
                            {
                                VipID = editPhoneContact.VipID,
                                CustomerID = editPhoneContact.CustomerID,
                                StaffID = editPhoneContact.StaffID,
                                PhoneNumber = editPhoneContact.PhoneNumber,
                                FirstName = editPhoneContact.FirstName,
                                LastName = editPhoneContact.LastName,
                                Email = editPhoneContact.Email,
                                BirthMonth = editPhoneContact.BirthMonth,
                                BirthDayOfMonth = editPhoneContact.BirthDayOfMonth,
                                Bucket = editPhoneContact.Bucket,
                            },
                            async (obj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestList = (obj as EditPhoneContactResponseModel);
                                    if (requestList != null)
                                    {
                                        try
                                        {
                                            {
                                                await GetContactListFromServer();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            await UserDialogs.Instance.AlertAsync(ex.StackTrace, "Success", "OK");
                                        }
                                    }
                                    UserDialog.HideLoading();
                                });
                            }, (objj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestLists = (objj as EditPhoneContactResponseModel);
                                    if (requestLists != null)
                                    {
                                        UserDialog.HideLoading();
                                        UserDialog.Alert(requestLists.errorMessage, "", "Ok");
                                    }
                                    else
                                    {
                                        UserDialog.HideLoading();
                                        UserDialog.Alert("Something went wrong.  Please restart the app and try again.", "", "Ok");
                                    }
                                });
                            });
                        }
                    }).ConfigureAwait(false);
                }
                else
                {
                    UserDialogs.Instance.Loading().Hide();
                    await UserDialogs.Instance.AlertAsync("No Network Connection found, Please try again!", "", "Okay");
                }
            }
            catch (Exception ex)
            {
                UserDialog.HideLoading();
            }
        }

        /// <summary>
        /// TODO:To Call The EditPhoneContact Api...
        /// </summary>
        /// <returns></returns>
        public async Task DeletePhoneContactAsync(GetPhoneContactListResponseModel getPhoneContactListResponseModel)
        {
            //Call api...
            try
            {
                UserDialogs.Instance.ShowLoading("Loading…", MaskType.Clear);
                if (CrossConnectivity.Current.IsConnected)
                {
                    await Task.Run(async () =>
                    {
                        if (_businessCode != null)
                        {
                            await _businessCode.DeletePhoneContactApi(new DeletePhoneContactRequestModel()
                            {
                                VipID = (int)getPhoneContactListResponseModel.vipID,
                                CustomerID = (int)getPhoneContactListResponseModel.customerID,
                                StaffID = (int)getPhoneContactListResponseModel.staffID,
                            },
                            async (obj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestList = (obj as DeletePhoneContactResponseModel);
                                    if (requestList != null)
                                    {
                                        try
                                        {
                                            UserDialogs.Instance.Alert("Your contact has been deleted successfully.", "", "OK");
                                        }
                                        catch (Exception ex)
                                        {
                                            await UserDialogs.Instance.AlertAsync(ex.StackTrace, "Success", "OK");
                                        }
                                    }
                                    UserDialog.HideLoading();
                                });
                            }, (objj) =>
                            {
                                Device.BeginInvokeOnMainThread(async () =>
                                {
                                    var requestLists = (objj as DeletePhoneContactResponseModel);
                                    if (requestLists != null)
                                    {
                                        UserDialog.HideLoading();
                                        UserDialog.Alert(requestLists.errorMessage, "", "Ok");
                                    }
                                    else
                                    {
                                        UserDialog.HideLoading();
                                        UserDialog.Alert("Something went wrong.  Please restart the app and try again.", "", "Ok");
                                    }
                                });
                            });
                        }
                    }).ConfigureAwait(false);
                }
                else
                {
                    UserDialogs.Instance.Loading().Hide();
                    await UserDialogs.Instance.AlertAsync("No Network Connection found, Please try again!", "", "Okay");
                }
            }
            catch (Exception ex)
            {
                UserDialog.HideLoading();
            }
        }
        #endregion
    }
}
