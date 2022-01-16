using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vedanta.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomProgressBar : ContentView
    {

        public static readonly BindableProperty ProgressProperty = BindableProperty.Create(
            nameof(Progress),
            typeof(int),
            typeof(CustomProgressBar),
            defaultValue: 0,
            defaultBindingMode:BindingMode.TwoWay,
            propertyChanged: OnProgressPercentChange
            );

        private static void OnProgressPercentChange(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomProgressBar)bindable;
            GenerateDynamicGrid((int)newValue, bindable);
        }

        public CustomProgressBar()
        {
            InitializeComponent();
        }

        public int Progress
        {
            get
            {
                return (int)GetValue(ProgressProperty);
            }

            set
            {
                base.SetValue(ProgressProperty, value);
            }
        }

        private static void GenerateDynamicGrid(int level, BindableObject bindable)
        {
            try
            {
                var control = (CustomProgressBar)bindable;
                var ProgressBarGrid = control.ProgressBarGrid;
                var fram = new Frame { BackgroundColor = Color.Green, Padding = new Thickness(0) };
                var value = ((double)level / 10);
                int percentageValue = Convert.ToInt32(Math.Round(value, 0));
                int secondColumnValue = 10 - percentageValue;
                if (secondColumnValue < 0)
                {
                    secondColumnValue = 0;
                }
                control.ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(percentageValue, GridUnitType.Star) });
                ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(secondColumnValue, GridUnitType.Star) });
                ProgressBarGrid.Children.Add(fram, 0, 0);

            }
            catch (Exception ex)
            {

               
            }
















            //if (level < 10)
            //{
            //    control. ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            //    ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(8, GridUnitType.Star) });
            //    ProgressBarGrid.Children.Add(fram, 0, 0);

            //}
            //else if (level >= 10 && level < 20)
            //{
            //    ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            //    ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });
            //    ProgressBarGrid.Children.Add(fram, 0, 0);

            //}
            //else if (level >= 20 && level < 30)
            //{
            //    ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });
            //    ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
            //    ProgressBarGrid.Children.Add(fram, 0, 0);

            //}
            //else if (level >= 30 && level < 35)
            //{
            //    ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(8, GridUnitType.Star) });
            //    ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            //    ProgressBarGrid.Children.Add(fram, 0, 0);

            //}
            //else if (level == 35)
            //{
            //    //ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });
            //    //ProgressBarGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });

            //    ProgressBarGrid.Children.Add(fram, 0, 0);

            //}

           

        }
    }
}