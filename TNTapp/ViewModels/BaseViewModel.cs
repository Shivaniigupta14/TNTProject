using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using TNTapp.BussinessCode;
using Xamarin.Forms;

namespace TNTapp.ViewModels
{
    public class BaseViewModel : BindableObject
    {

        protected readonly IBusinessCode _businessCode;

        #region Constructor
        public BaseViewModel(INavigation navigation = null) 
        {
            Navigation = navigation;
            BackCommand = new Command(OnBackAsync);
            _businessCode = SimpleIoc.Default.GetInstance<IBusinessCode>();
        }
        #endregion

        #region Properties
        private INavigation _Navigation;
        public INavigation Navigation
        {
            get { return _Navigation; }
            set
            {
                if (_Navigation != value)
                {
                    _Navigation = value;
                    OnPropertyChanged("Navigation");
                }
            }
        }
       
        public Command BackCommand { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// ToDO:  Navigate to Back Page
        /// </summary>
        /// <param name="obj"></param>
        private async void OnBackAsync(object obj)
        {
            await PopModalAsync();
        }

        public Acr.UserDialogs.IUserDialogs UserDialog
        {
            get
            {
                return Acr.UserDialogs.UserDialogs.Instance;
            }
        }
        public async Task PushModalAsync(Page page)
        {
            if (Navigation != null)
                await Navigation.PushModalAsync(page);
        }

        public async Task PopModalAsync()
        {
            if (Navigation != null)
                await Navigation.PopModalAsync();
        }

        public async Task PushAsync(Page page)
        {
            if (Navigation != null)
                await Navigation.PushModalAsync(page);
        }

        public async Task PopAsync()
        {
            if (Navigation != null)
                await Navigation.PopModalAsync();
        }
        #endregion

    }
}
