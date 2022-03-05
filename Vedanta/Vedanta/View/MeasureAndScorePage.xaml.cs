using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vedanta.Utility;
using Vedanta.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vedanta.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeasureAndScorePage : ContentPage
    {
        public MeasureAndScorePageViewModel vm;
        public MeasureAndScorePage()
        {
            InitializeComponent();
             vm = (MeasureAndScorePageViewModel)BindingContext;

           
           // GenerateDynamicGridForGembaComplition(7);
        }

        //private void GenerateDynamicGrid(int level)
        //{
        //    var fram = new Frame { BackgroundColor = Color.Green, Padding = new Thickness { Bottom = 0, Top = 0, Left = 0, Right = 0 } };
        //    if (level < 10)
        //    {
        //        ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
        //        ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(8, GridUnitType.Star) });
        //        ProgressBarGrid.Children.Add(fram, 0, 0);

        //    }
        //    else if(level>=10&& level < 20)
        //    {
        //        ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
        //        ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });
        //        ProgressBarGrid.Children.Add(fram, 0, 0);

        //    }
        //    else if(level>=20&& level < 30)
        //    {
        //        ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });
        //        ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
        //        ProgressBarGrid.Children.Add(fram, 0, 0);

        //    }
        //    else if (level >= 30 && level < 35)
        //    {
        //        ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(8, GridUnitType.Star) });
        //        ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
        //        ProgressBarGrid.Children.Add(fram, 0, 0);

        //    }
        //    else if (level == 35)
        //    {
        //        //ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });
        //        //ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
                
        //        ProgressBarGrid.Children.Add(fram, 0, 0);

        //    }


        //}
      
        
        
        //private void GenerateDynamicGridForGembaComplition(int level)
        //{
        //    var fram = new Frame { BackgroundColor = Color.Green, Padding = new Thickness { Bottom = 0, Top = 0, Left = 0, Right = 0 } };
        //    if (level < 10)
        //    {
        //        ProgressBarGridGembaCompletion.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
        //        ProgressBarGridGembaCompletion.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(8, GridUnitType.Star) });
        //        ProgressBarGridGembaCompletion.Children.Add(fram, 0, 0);

        //    }
        //    else if(level>=10&& level < 20)
        //    {
        //        ProgressBarGridGembaCompletion.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
        //        ProgressBarGridGembaCompletion.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });
        //        ProgressBarGridGembaCompletion.Children.Add(fram, 0, 0);

        //    }
        //    else if(level>=20&& level < 30)
        //    {
        //        ProgressBarGridGembaCompletion.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });
        //        ProgressBarGridGembaCompletion.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
        //        ProgressBarGridGembaCompletion.Children.Add(fram, 0, 0);

        //    }
        //    else if (level >= 30 && level < 35)
        //    {
        //        ProgressBarGridGembaCompletion.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(8, GridUnitType.Star) });
        //        ProgressBarGridGembaCompletion.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
        //        ProgressBarGridGembaCompletion.Children.Add(fram, 0, 0);

        //    }
        //    else if (level == 35)
        //    {
        //        //ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });
        //        //ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

        //        ProgressBarGridGembaCompletion.Children.Add(fram, 0, 0);

        //    }


        //}


        protected override void OnAppearing()
        {
            base.OnAppearing();
           // GenerateDynamicGrid(Session.Instance.CurrentGembaSchedule.Score);
        }

        protected override bool OnBackButtonPressed()
        {
            MessagingCenter.Send<string, string>("DeviceBackButttonToHomePage", "BackToHome", "");
            return true;
        }

    }
}