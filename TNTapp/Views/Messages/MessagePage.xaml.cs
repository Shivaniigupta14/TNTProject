using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNTapp.ViewModels.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TNTapp.Views.Messages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagePage : ContentPage
    {
        // Define local variable
        protected MessagePageViewModel MessagePageVM;
        #region Constructor
        public MessagePage()
        {
            InitializeComponent();
            MessagePageVM = new MessagePageViewModel(this.Navigation);
            this.BindingContext = MessagePageVM;
        }
        #endregion
    }
}