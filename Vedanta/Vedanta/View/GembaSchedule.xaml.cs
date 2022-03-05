using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vedanta.Service;
using Vedanta.Utility;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vedanta.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GembaSchedule : ContentPage
    {
        public GembaSchedule()
        {
            InitializeComponent();
           
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        protected override  bool OnBackButtonPressed()
        {
            MessagingCenter.Send<string, string>("GembaSchedule", "ExitPopUp", "");

            return true;
        }
    }
}