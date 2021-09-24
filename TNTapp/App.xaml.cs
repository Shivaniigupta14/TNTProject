using GalaSoft.MvvmLight.Ioc;
using TNTapp.BussinessCode;
using TNTapp.Views.WelcomeScreen;
using Xamarin.Forms;

namespace TNTapp
{
    public partial class App : Application
    {

        //TODO : To Define Global Varialbes Here....
        private static Autofac.IContainer _container;
        protected readonly IBusinessCode _businessCode;

        public App()
        {
            InitializeComponent();
            //To initalize ...
            AppSetup appSetup = new AppSetup();
            _container = appSetup.CreateContainer();
            _businessCode = SimpleIoc.Default.GetInstance<IBusinessCode>();

            
            if (!string.IsNullOrEmpty(Helpers.Settings.GeneralUserName))
            {
                App.Current.MainPage = new Views.Home.HomeTabbedPage();
            }
            else
            { 
                MainPage = new WelcomePage();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
