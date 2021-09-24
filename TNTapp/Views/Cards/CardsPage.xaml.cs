using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNTapp.ViewModels.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TNTapp.Views.Cards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardsPage : ContentPage
    {
        //Define Local Variable
        protected CardsPageViewModel CardsPageVM;
        #region Constructor
        public CardsPage()
        {
            InitializeComponent();
            CardsPageVM = new CardsPageViewModel(this.Navigation);
            this.BindingContext = CardsPageVM;
        }
        #endregion
    }
}