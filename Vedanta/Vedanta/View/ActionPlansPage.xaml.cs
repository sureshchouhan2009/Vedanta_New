using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vedanta.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActionPlansPage : ContentPage
    {
        public ActionPlansPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            MessagingCenter.Send<string, string>("ActionPlansPage", "ExitPopUp", "");

            return true;
        }
    }
}