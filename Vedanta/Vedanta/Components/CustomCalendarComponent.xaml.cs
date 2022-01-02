using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vedanta.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vedanta.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomCalendarComponent : ContentView
    {


        /// <summary>
        ///     Bindable property for MinimumDate
        /// </summary>
        public static readonly BindableProperty InitialDateProperty =
            BindableProperty.Create(nameof(InitialDate), typeof(DateTime), typeof(CustomCalendarComponent), DateTime.Now,
                 propertyChanged:
                    (bindable, oldValue, newValue) =>
                    {
                        ((CustomCalendarComponent)bindable).SetSelectedDate((DateTime)newValue);
                    });

        private void SetSelectedDate(DateTime newValue)
        {
            InitialDate = (DateTime)newValue;

        }


        public DateTime InitialDate
        {
            get { return (DateTime)GetValue(InitialDateProperty); }
            set
            {
                SetValue(InitialDateProperty, value);
            }
        }




        public CustomCalendarComponent()
        {
            InitializeComponent();
            DateTime pagedate = InitialDate;
            bool nextDisable = true;
            bool prevDisable = true;
            dt = pagedate;
            currentmonth = pagedate.Month;
            currentdate = pagedate.Day;
            currentyear = pagedate.Year;
            previousdateselection = prevDisable;
            nextdateselection = nextDisable;
            heading.Text = pagedate.ToString("MMMM", culture) + " " + pagedate.Year;
            footer.IsVisible = false;
            Init();

        }







        public event EventHandler<DateSelectedEvent> SelectedDateEvent;
        //static CultureInfo culture = new CultureInfo("nl-NL");
        static CultureInfo culture = new CultureInfo(CultureInfo.CurrentCulture.Name);
        public bool horseplanning = false, start_date = false, end_date = false, selectedstart = false;
        public DateTime endDate = DateTime.Now;
        public bool previousdateselection = true, nextdateselection = true;
        Grid days = new Grid() { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
        public DateTime dt = DateTime.Now;
        public int currentmonth = DateTime.Now.Month, currentyear = DateTime.Now.Year, listyears = DateTime.Now.Year, currentdate = DateTime.Now.Day;
        Image leftarrow = new Image { Source = "Previous.png", Aspect = Aspect.Fill, BindingContext = 0, HeightRequest = 25, WidthRequest = 25, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center };
        Image rightarrow = new Image { Source = "Next.png", Aspect = Aspect.Fill, BindingContext = 0, HeightRequest = 25, WidthRequest = 25, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
        Frame CalendarFrame = new Frame
        {
            CornerRadius = 10,
            BorderColor = Color.LightGray,
            BackgroundColor = Color.White,
            Padding = 0,
            //HasShadow = true
        };

        StackLayout FramsInnerStack = new StackLayout
        {
            Padding = 0,
            Margin = 0
        };

        //Image closepopup = new Image
        //{
        //    Margin = new Thickness(0, 0, 10, 0),
        //    Source = "iconspin.png",
        //    Aspect = Aspect.AspectFit,
        //    BindingContext = 0,
        //    HeightRequest = 20,
        //    WidthRequest = 20,
        //    HorizontalOptions = LayoutOptions.End,
        //    VerticalOptions = LayoutOptions.Start
        //};
        CustomLabel DayLable = new CustomLabel() { Text = "Day", FontSize = 16, TextColor = Color.Gray, BindingContext = 0, FontAttributes = FontAttributes.Italic, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
        // CustomLabel WeekLable=  new CustomLabel() { Text ="Week",  FontSize = 16, TextColor = Color.Gray, BindingContext = 0, FontAttributes = FontAttributes.Italic, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
        CustomLabel MonthLable = new CustomLabel() { Text = "Month", FontSize = 16, TextColor = Color.Gray, BindingContext = 0, FontAttributes = FontAttributes.Italic, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
        CustomLabel YearLable = new CustomLabel() { Text = "Year", FontSize = 16, TextColor = Color.Gray, BindingContext = 1, FontAttributes = FontAttributes.Italic, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };





        CustomLabel heading = new CustomLabel
        {
            Text = DateTime.Now.ToString("MMMM", culture) + " " + DateTime.Now.Year,
            FontSize = 16,
            TextColor = Color.Black,
            BindingContext = 0,
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Center,
        };
        public DateTime startDate = DateTime.Now;
        List<DateTime> seldatelist = new List<DateTime>();
        Grid footer = new Grid() { HorizontalOptions = LayoutOptions.End };

        Grid newFooter = new Grid { };
        bool tap = true;

        public CustomCalendarComponent(DateTime pagedate, bool nextDisable = true, bool prevDisable = true)
        {
            InitializeComponent();
            dt = pagedate;
            currentmonth = pagedate.Month;
            currentdate = pagedate.Day;
            currentyear = pagedate.Year;
            previousdateselection = prevDisable;
            nextdateselection = nextDisable;
            heading.Text = pagedate.ToString("MMMM", culture) + " " + pagedate.Year;
            footer.IsVisible = false;
            Init();
        }

        public CustomCalendarComponent(DateTime update, bool startsel, bool horseplan, DateTime? startorend)
        {
            InitializeComponent();

            selectedstart = startsel;
            if (startsel)
            {
                startDate = update;
                endDate = update;
                if (startorend.HasValue)
                {
                    start_date = true;
                    endDate = startorend.Value;
                    //dt = update;
                    //currentmonth = dt.Month;
                    //currentyear = dt.Year;
                    //listyears = dt.Year;
                    //currentdate = dt.Day;
                }
            }
            else
            {
                startDate = update;
                endDate = update;
                if (startorend.HasValue)
                {
                    end_date = true;
                    startDate = startorend.Value;
                    //dt = startorend.Value;
                    //currentmonth = dt.Month;
                    //currentyear = dt.Year;
                    //listyears = dt.Year;
                    //currentdate = dt.Day;
                }

            }
            heading.Text = update.ToString("MMMM", culture) + " " + update.Year;
            dt = update;
            currentmonth = dt.Month;
            currentyear = dt.Year;
            listyears = dt.Year;
            currentdate = dt.Day;

            horseplanning = horseplan;

            Init();
        }

        void Init()
        {
            Grid optionGrid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                ColumnSpacing = 5
            };
            optionGrid.RowDefinitions.Add(new RowDefinition { });
            optionGrid.ColumnDefinitions.Add(new ColumnDefinition());
            optionGrid.ColumnDefinitions.Add(new ColumnDefinition());
            optionGrid.ColumnDefinitions.Add(new ColumnDefinition());


            optionGrid.Children.Add(DayLable, 0, 0);
            optionGrid.Children.Add(MonthLable, 1, 0);
            optionGrid.Children.Add(YearLable, 2, 0);


            dateview.Children.Add(optionGrid);

            //added year click action
            var YearOptionGesture = new TapGestureRecognizer();
            YearOptionGesture.NumberOfTapsRequired = 1;
            YearOptionGesture.Tapped += YearOption_Clicked;
            YearLable.GestureRecognizers.Add(YearOptionGesture);

            //added Moth click action
            var MonthOptionGesture = new TapGestureRecognizer();
            MonthOptionGesture.NumberOfTapsRequired = 1;
            MonthOptionGesture.Tapped += MonthOption_Clicked;
            MonthLable.GestureRecognizers.Add(MonthOptionGesture);


            Grid header = new Grid() { };
            header.RowDefinitions.Add(new RowDefinition { Height = 70 });
            header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.40, GridUnitType.Star) });
            header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.30, GridUnitType.Star) });
            header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.30, GridUnitType.Star) });

            header.Children.Add(heading, 0, 0);
            header.Children.Add(leftarrow, 1, 0);
            header.Children.Add(rightarrow, 2, 0);
            //header.Children.Add(closepopup, 2, 0);
            //header.Padding = new Thickness(0, 10, 0, 10);
            var headingTapped = new TapGestureRecognizer();
            headingTapped.Tapped += Heading_Clicked;
            heading.GestureRecognizers.Add(headingTapped);
            //heading.Clicked += Heading_Clicked;

            var leftarrowtapped = new TapGestureRecognizer();
            leftarrowtapped.Tapped += Leftarrow_Clicked;
            leftarrow.GestureRecognizers.Add(leftarrowtapped);

            var rightarrowtapped = new TapGestureRecognizer();
            rightarrowtapped.Tapped += Rightarrow_Clicked;
            rightarrow.GestureRecognizers.Add(rightarrowtapped);

            //var closepopuptapped = new TapGestureRecognizer();
            //closepopuptapped.Tapped += Closepopuptapped_Tapped; ;
            //closepopup.GestureRecognizers.Add(closepopuptapped);
            int heightandWidth = Device.iOS == Device.RuntimePlatform ? 35 : 40;
            days.RowDefinitions.Add(new RowDefinition { Height = heightandWidth });
            days.RowDefinitions.Add(new RowDefinition { Height = heightandWidth });
            days.RowDefinitions.Add(new RowDefinition { Height = heightandWidth });
            days.RowDefinitions.Add(new RowDefinition { Height = heightandWidth });
            days.RowDefinitions.Add(new RowDefinition { Height = heightandWidth });
            days.RowDefinitions.Add(new RowDefinition { Height = heightandWidth });
            days.RowDefinitions.Add(new RowDefinition { Height = heightandWidth });

            days.ColumnDefinitions.Add(new ColumnDefinition { });
            days.ColumnDefinitions.Add(new ColumnDefinition { });
            days.ColumnDefinitions.Add(new ColumnDefinition { });
            days.ColumnDefinitions.Add(new ColumnDefinition { });
            days.ColumnDefinitions.Add(new ColumnDefinition { });
            days.ColumnDefinitions.Add(new ColumnDefinition { });
            days.ColumnDefinitions.Add(new ColumnDefinition { });
            days.ColumnDefinitions.Add(new ColumnDefinition { });
            days.ColumnDefinitions.Add(new ColumnDefinition { });

            days.ColumnSpacing = 0;

            FramsInnerStack.Children.Add(header);

            //  dateview.Children.Add(header);

            //startDate = new DateTime(currentyear, currentmonth, currentdate);
            if (endDate > startDate)
            {
                seldatelist = Enumerable.Range(0, endDate.Subtract(startDate).Days + 1)
                         .Select(d => startDate.AddDays(d)).ToList();

                seldatelist.Add(endDate);
            }

            DaysGrid();
            FramsInnerStack.Children.Add(days);
            CalendarFrame.Content = FramsInnerStack;
            dateview.Children.Add(CalendarFrame);
            //dateview.Children.Add(days);

            newFooter.RowDefinitions.Add(new RowDefinition { Height = 70 });
            newFooter.ColumnDefinitions.Add(new ColumnDefinition { });
            newFooter.ColumnDefinitions.Add(new ColumnDefinition { });
            newFooter.ColumnDefinitions.Add(new ColumnDefinition { });

            var TimeLabel = new Label() { Text = "Time", FontSize = 18, TextColor = Color.Black, BindingContext = 0, FontAttributes = FontAttributes.Bold, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            Frame timePickerFrame = new Frame
            {
                BorderColor = Color.Gray,
                Padding = 0,
                HeightRequest = 40.0

            };
            Frame AmPmFrame = new Frame
            {
                BorderColor = Color.Gray,
                Padding = 0,
                HeightRequest = 40.0

            };
            var timepicker = new CustomTimePicker()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            timePickerFrame.Content = timepicker;

            StackLayout ampm = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Margin = 0,
                Padding = 0,
                BackgroundColor = Color.Gray,
            };
            Button am = new Button
            {
                Text = "AM",
                BackgroundColor = Color.White,
                WidthRequest = 50,
                HeightRequest = 40
            };
            Button pm = new Button
            {
                Text = "PM",
                BackgroundColor = Color.Transparent,
                WidthRequest = 50,
                HeightRequest = 40
            };
            ampm.Children.Add(am);
            ampm.Children.Add(pm);
            AmPmFrame.Content = ampm;
            newFooter.Children.Add(TimeLabel, 0, 0);
            newFooter.Children.Add(timePickerFrame, 1, 0);
            newFooter.Children.Add(AmPmFrame, 2, 0);  //fotter of the calendar
            //dateview.Children.Add(newFooter);
            FramsInnerStack.Children.Add(newFooter);


            //setting default selection option
            SetupOptionsGridUI(SelectedOption.Day);



            //footer block of calendar
            #region notrquired for new requirement
            footer.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            footer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            footer.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });


            Frame resetframe = new Frame();
            resetframe.HasShadow = false;
            resetframe.BorderColor = Color.White;
            resetframe.BackgroundColor = Color.Red; //SharedClass.Translate("CAL_Reset")
            resetframe.Content = new CustomLabel { Text = "SharedClass.Translate", TextColor = Color.White, FontSize = 12, HorizontalTextAlignment = TextAlignment.Center };

            var resettapped = new TapGestureRecognizer();
            resettapped.Tapped += Resettapped_Tapped;
            resetframe.GestureRecognizers.Add(resettapped);

            resetframe.CornerRadius = 0;
            resetframe.Padding = 10;
            resetframe.Margin = 10;

            footer.Children.Add(resetframe, 1, 0);

            Frame saveframe = new Frame();
            saveframe.HasShadow = false;
            saveframe.BorderColor = Color.Green;
            saveframe.BackgroundColor = Color.Red;
            saveframe.Content = new CustomLabel { Text = "SharedClass.Translate", TextColor = Color.White, FontSize = 12, HorizontalTextAlignment = TextAlignment.Center };

            var savetapped = new TapGestureRecognizer();
            savetapped.Tapped += savetapped_Tapped; ;
            saveframe.GestureRecognizers.Add(savetapped);

            saveframe.CornerRadius = 0;
            saveframe.Padding = 10;
            saveframe.Margin = 10;

            footer.Children.Add(saveframe, 0, 0);

            dateview.Children.Add(footer);
            #endregion
        }



        private void Resettapped_Tapped(object sender, EventArgs e)
        {
            // PopupNavigation.Instance.PopAsync();
            // HorsePlanningScreen.reset();
        }

        private void savetapped_Tapped(object sender, EventArgs e)
        {
            // PopupNavigation.Instance.PopAsync();
            // HorsePlanningScreen.savedate();
        }

        private void Closepopuptapped_Tapped(object sender, EventArgs e)
        {
            // PopupNavigation.Instance.PopAsync();
        }

        private void Rightarrow_Clicked(object sender, EventArgs e)
        {
            if (tap)
            {
                tap = false;
                var btn = (sender as Image);
                var t = Convert.ToInt32(btn.BindingContext);
                if (!nextdateselection)
                {
                    if (currentyear >= DateTime.Now.Year && currentmonth >= DateTime.Now.Month && t == 0)
                    {
                        tap = true;
                        return;
                    }
                    if (currentyear >= DateTime.Now.Year && t == 1)
                    {
                        tap = true;
                        return;
                    }
                    if (t == 2 && listyears >= currentyear - 42)
                    {
                        tap = true;
                        return;
                    }
                }

                if (t == 0)//days
                {
                    currentmonth++;
                    if (currentmonth > 12)
                    {
                        currentmonth = 1;
                        currentyear++;
                    }
                    DaysGrid();
                    var monthname = new DateTime(currentyear, currentmonth, 1).ToString("MMMM", culture);
                    heading.Text = monthname + " " + currentyear;
                }
                else if (t == 1)//month
                {
                    currentyear++;
                    heading.Text = currentyear.ToString();
                    MonthsGrid();
                }
                else if (t == 2)//year
                {
                    listyears += 42;
                    heading.Text = (listyears + 1).ToString() + "-" + (listyears + 42).ToString();
                    YearsGrid(listyears);
                }
                tap = true;
            }
        }

        private void Leftarrow_Clicked(object sender, EventArgs e)
        {
            if (tap)
            {
                tap = false;
                var btn = (sender as Image);
                var t = Convert.ToInt32(btn.BindingContext);

                if (!previousdateselection)
                {
                    if (currentyear <= DateTime.Now.Year && currentmonth <= DateTime.Now.Month && t == 0)
                    {
                        tap = true;
                        return;
                    }
                    if (currentyear <= DateTime.Now.Year && t == 1)
                    {
                        tap = true;
                        return;
                    }
                    if (t == 2 && listyears <= currentyear + 42)
                    {
                        tap = true;
                        return;
                    }
                }

                if (t == 0)//days
                {
                    currentmonth--;
                    if (currentmonth < 1)
                    {
                        currentmonth = 12;
                        currentyear--;
                    }
                    DaysGrid();

                    var monthname = new DateTime(currentyear, currentmonth, 1).ToString("MMMM", culture);
                    heading.Text = monthname + " " + currentyear;
                }
                else if (t == 1)//month
                {
                    currentyear--;
                    heading.Text = currentyear.ToString();
                    MonthsGrid();
                }
                else if (t == 2)//year
                {
                    listyears -= 42;
                    if (listyears < 0)
                    { listyears = DateTime.Now.Year; listyears -= 42; }
                    heading.Text = (listyears + 1).ToString() + "-" + (listyears + 42).ToString();
                    YearsGrid(listyears);
                }
                tap = true;
            }
        }

        private void Heading_Clicked(object sender, EventArgs e)
        {
            //currentmonth = DateTime.Now.Month;
            //currentyear = DateTime.Now.Year;
            //listyears = DateTime.Now.Year;

            var btn = (sender as CustomLabel);
            var bc = btn.BindingContext;

            var t = Convert.ToInt32(bc);
            if (t == 0)//days
            {
                MonthsGrid();
                heading.Text = currentyear.ToString();

                heading.BindingContext = 1;
                leftarrow.BindingContext = 1;
                rightarrow.BindingContext = 1;
            }
            else if (t == 1)//month
            {
                listyears -= 42;
                heading.Text = (listyears + 1).ToString() + "-" + (listyears + 42).ToString();
                YearsGrid(listyears);
                heading.BindingContext = 2;
                leftarrow.BindingContext = 2;
                rightarrow.BindingContext = 2;
            }
            else if (t == 2)//year
            {

            }
        }
        private void YearOption_Clicked(object sender, EventArgs e)
        {


            var btn = (sender as CustomLabel);
            var bc = btn.BindingContext;

            var t = Convert.ToInt32(bc);
            SetupOptionsGridUI(SelectedOption.Year);
            if (t == 0)//days
            {
                MonthsGrid();
                heading.Text = currentyear.ToString();

                heading.BindingContext = 1;
                leftarrow.BindingContext = 1;
                rightarrow.BindingContext = 1;
            }
            else if (t == 1)//month
            {
                listyears -= 42;
                heading.Text = (listyears + 1).ToString() + "-" + (listyears + 42).ToString();
                YearsGrid(listyears);
                heading.BindingContext = 2;
                leftarrow.BindingContext = 2;
                rightarrow.BindingContext = 2;
            }
            else if (t == 2)//year
            {

            }
        }
        private void MonthOption_Clicked(object sender, EventArgs e)
        {


            var btn = (sender as CustomLabel);
            var bc = btn.BindingContext;
            SetupOptionsGridUI(SelectedOption.Month);
            var t = Convert.ToInt32(bc);

            if (t == 0)//days
            {
                MonthsGrid();
                heading.Text = currentyear.ToString();

                heading.BindingContext = 1;
                leftarrow.BindingContext = 1;
                rightarrow.BindingContext = 1;
            }
            else if (t == 1)//month
            {
                listyears -= 42;
                heading.Text = (listyears + 1).ToString() + "-" + (listyears + 42).ToString();
                YearsGrid(listyears);
                heading.BindingContext = 2;
                leftarrow.BindingContext = 2;
                rightarrow.BindingContext = 2;
            }
            else if (t == 2)//year
            {

            }
        }

        static int CheckRowCount(DayOfWeek day)
        {
            int days = 0;
            switch (day)
            {
                case DayOfWeek.Friday:
                    days = 6;
                    break;
                case DayOfWeek.Monday:
                    days = 2;
                    break;
                case DayOfWeek.Saturday:
                    days = 7;
                    break;
                case DayOfWeek.Sunday:
                    days = 1;
                    break;
                case DayOfWeek.Thursday:
                    days = 5;
                    break;
                case DayOfWeek.Tuesday:
                    days = 3;
                    break;
                case DayOfWeek.Wednesday:
                    days = 4;
                    break;
                default:
                    break;
            }
            return days;
        }

        public static List<DateTime> GetDates(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                             .Select(day => new DateTime(year, month, day)) // Map each day to a date
                             .ToList(); // Load dates into a list
        }

        public void DaysGrid()
        {
            days.Children.Clear();
            int row = 0;

            CustomLabel su = new CustomLabel { Text = culture.DateTimeFormat.GetAbbreviatedDayName(DayOfWeek.Sunday).ToUpper(), TextColor = Color.Gray, FontSize = 12, HorizontalTextAlignment = TextAlignment.Center };
            CustomLabel mo = new CustomLabel { Text = culture.DateTimeFormat.GetAbbreviatedDayName(DayOfWeek.Monday).ToUpper(), TextColor = Color.Gray, FontSize = 12, HorizontalTextAlignment = TextAlignment.Center };
            CustomLabel tu = new CustomLabel { Text = culture.DateTimeFormat.GetAbbreviatedDayName(DayOfWeek.Tuesday).ToUpper(), TextColor = Color.Gray, FontSize = 12, HorizontalTextAlignment = TextAlignment.Center };
            CustomLabel we = new CustomLabel { Text = culture.DateTimeFormat.GetAbbreviatedDayName(DayOfWeek.Wednesday).ToUpper(), TextColor = Color.Gray, FontSize = 12, HorizontalTextAlignment = TextAlignment.Center };
            CustomLabel th = new CustomLabel { Text = culture.DateTimeFormat.GetAbbreviatedDayName(DayOfWeek.Thursday).ToUpper(), TextColor = Color.Gray, FontSize = 12, HorizontalTextAlignment = TextAlignment.Center };
            CustomLabel fr = new CustomLabel { Text = culture.DateTimeFormat.GetAbbreviatedDayName(DayOfWeek.Friday).ToUpper(), TextColor = Color.Gray, FontSize = 12, HorizontalTextAlignment = TextAlignment.Center };
            CustomLabel sa = new CustomLabel { Text = culture.DateTimeFormat.GetAbbreviatedDayName(DayOfWeek.Saturday).ToUpper(), TextColor = Color.Gray, FontSize = 12, HorizontalTextAlignment = TextAlignment.Center };

            if (Device.iOS == Device.RuntimePlatform)
            {
                su.FontFamily = "Comfortaa";
                mo.FontFamily = "Comfortaa";
                tu.FontFamily = "Comfortaa";
                we.FontFamily = "Comfortaa";
                th.FontFamily = "Comfortaa";
                fr.FontFamily = "Comfortaa";
                sa.FontFamily = "Comfortaa";
            }
            days.Children.Add(su, 1, row);
            days.Children.Add(mo, 2, row);
            days.Children.Add(tu, 3, row);
            days.Children.Add(we, 4, row);
            days.Children.Add(th, 5, row);
            days.Children.Add(fr, 6, row);
            days.Children.Add(sa, 7, row);

            var currentmonthdates = GetDates(currentyear, currentmonth);
            //int weekdays = 0;


            row++;
            foreach (var c in currentmonthdates)
            {
                //if (weekdays == 7)
                //{
                //    row++;
                //}
                //weekdays = CheckRowCount(c.DayOfWeek);

                bool selectedrange = seldatelist.Any(z => z.Day == c.Day && z.Month == c.Month && z.Year == c.Year) && horseplanning;
                //!horseplanning &&
                Color datecompare = ((dt.Day == c.Day) && (dt.Month == c.Month) && (dt.Year == c.Year)) ? Color.Gray : Color.Transparent;
                bool enddatecompare = ((endDate.Day == c.Day) && (endDate.Month == c.Month) && (endDate.Year == c.Year));
                bool startdatecompare = ((startDate.Day == c.Day) && (startDate.Month == c.Month) && (startDate.Year == c.Year));
                int fontsize = (Device.RuntimePlatform == Device.iOS) ? 12 : 10;
                switch (c.DayOfWeek)
                {
                    case DayOfWeek.Friday:
                        Button fri = new Button
                        {
                            BindingContext = (int)DayOfWeek.Friday,
                            CommandParameter = c.Day.ToString(),
                            Text = c.Day.ToString(),
                            TextColor = selectedrange ? ((enddatecompare || startdatecompare) ? Color.Black : Color.White) : Color.Black,
                            BackgroundColor = selectedrange ? ((enddatecompare || startdatecompare) ? Color.Black : Color.White) : datecompare,
                            CornerRadius = selectedrange ? ((enddatecompare || startdatecompare) ? 18 : 0) : 18,
                            FontSize = fontsize
                        };
                        fri.Clicked += Days_Clicked;
                        days.Children.Add(fri, 6, row);
                        break;
                    case DayOfWeek.Monday:
                        Button mond = new Button
                        {
                            BindingContext = (int)DayOfWeek.Monday,
                            CommandParameter = c.Day.ToString(),
                            Text = c.Day.ToString(),
                            TextColor = selectedrange ? ((enddatecompare || startdatecompare) ? Color.White : Color.Black) : Color.Black,
                            BackgroundColor = selectedrange ? ((enddatecompare || startdatecompare) ? Color.Black : Color.White) : datecompare,
                            CornerRadius = selectedrange ? ((enddatecompare || startdatecompare) ? 18 : 0) : 18,
                            FontSize = fontsize
                        };
                        mond.Clicked += Days_Clicked;
                        days.Children.Add(mond, 2, row);
                        break;
                    case DayOfWeek.Saturday:
                        Button sat = new Button
                        {
                            BindingContext = (int)DayOfWeek.Saturday,
                            CommandParameter = c.Day.ToString(),
                            Text = c.Day.ToString(),
                            TextColor = selectedrange ? ((enddatecompare || startdatecompare) ? Color.White : Color.Black) : Color.Black,
                            BackgroundColor = selectedrange ? ((enddatecompare || startdatecompare) ? Color.Black : Color.White) : datecompare,
                            CornerRadius = selectedrange ? ((enddatecompare || startdatecompare) ? 18 : 0) : 18,
                            FontSize = fontsize
                        };
                        sat.Clicked += Days_Clicked;
                        days.Children.Add(sat, 7, row);
                        row++;
                        break;
                    case DayOfWeek.Sunday:
                        Button sun = new Button
                        {
                            BindingContext = (int)DayOfWeek.Sunday,
                            CommandParameter = c.Day.ToString(),
                            Text = c.Day.ToString(),
                            TextColor = selectedrange ? ((enddatecompare || startdatecompare) ? Color.White : Color.Black) : Color.Black,
                            BackgroundColor = selectedrange ? ((enddatecompare || startdatecompare) ? Color.Black : Color.White) : datecompare,
                            CornerRadius = selectedrange ? ((enddatecompare || startdatecompare) ? 18 : 0) : 18,
                            FontSize = fontsize
                        };
                        sun.Clicked += Days_Clicked;
                        days.Children.Add(sun, 1, row);
                        break;
                    case DayOfWeek.Thursday:
                        Button thr = new Button
                        {
                            BindingContext = (int)DayOfWeek.Thursday,
                            CommandParameter = c.Day.ToString(),
                            Text = c.Day.ToString(),
                            TextColor = selectedrange ? ((enddatecompare || startdatecompare) ? Color.White : Color.Black) : Color.Black,
                            BackgroundColor = selectedrange ? ((enddatecompare || startdatecompare) ? Color.Black : Color.White) : datecompare,
                            CornerRadius = selectedrange ? ((enddatecompare || startdatecompare) ? 18 : 0) : 18,
                            FontSize = fontsize
                        };
                        thr.Clicked += Days_Clicked;
                        days.Children.Add(thr, 5, row);
                        break;
                    case DayOfWeek.Tuesday:
                        Button tue = new Button
                        {
                            BindingContext = (int)DayOfWeek.Tuesday,
                            CommandParameter = c.Day.ToString(),
                            Text = c.Day.ToString(),
                            TextColor = selectedrange ? ((enddatecompare || startdatecompare) ? Color.Green : Color.Black) : Color.Black,
                            BackgroundColor = selectedrange ? ((enddatecompare || startdatecompare) ? Color.Black : Color.White) : datecompare,
                            CornerRadius = selectedrange ? ((enddatecompare || startdatecompare) ? 18 : 0) : 18,
                            FontSize = fontsize
                        };
                        tue.Clicked += Days_Clicked;
                        days.Children.Add(tue, 3, row);
                        break;
                    case DayOfWeek.Wednesday:
                        Button wed = new Button
                        {
                            BindingContext = (int)DayOfWeek.Wednesday,
                            CommandParameter = c.Day.ToString(),
                            Text = c.Day.ToString(),
                            TextColor = selectedrange ? ((enddatecompare || startdatecompare) ? Color.Green : Color.Black) : Color.Black,
                            BackgroundColor = selectedrange ? ((enddatecompare || startdatecompare) ? Color.Black : Color.White) : datecompare,
                            CornerRadius = selectedrange ? ((enddatecompare || startdatecompare) ? 18 : 0) : 18,
                            FontSize = fontsize
                        };
                        wed.Clicked += Days_Clicked;
                        days.Children.Add(wed, 4, row);
                        break;
                    default:
                        break;
                }
            }


        }

        private void Days_Clicked(object sender, EventArgs e)
        {
            if (tap)
            {
                SetupOptionsGridUI(SelectedOption.Day);

                tap = false;
                var selbtn = (sender as Button);
                var selbtnbc = Convert.ToInt32(selbtn.BindingContext);
                currentdate = Convert.ToInt32(selbtn.CommandParameter);
                if (!previousdateselection)
                {
                    //var c = new DateTime(currentyear, currentmonth, currentdate);
                    if (currentmonth <= DateTime.Now.Month && currentyear <= DateTime.Now.Year && currentdate < DateTime.Now.Day)
                    {
                        currentdate = dt.Day;
                        tap = true;
                        return;
                    }
                }
                if (!nextdateselection)
                {
                    if (currentmonth >= DateTime.Now.Month && currentyear >= DateTime.Now.Year && currentdate > DateTime.Now.Day)
                    {
                        currentdate = dt.Day;
                        tap = true;
                        return;
                    }
                }


                if (horseplanning && end_date)
                {
                    endDate = new DateTime(currentyear, currentmonth, currentdate);

                    dt = endDate;
                    currentmonth = dt.Month;
                    currentyear = dt.Year;
                    listyears = dt.Year;
                    currentdate = dt.Day;

                    if (endDate < startDate)
                    {
                        tap = true;
                        return;
                    }
                    seldatelist = Enumerable.Range(0, endDate.Subtract(startDate).Days + 1)
                         .Select(d => startDate.AddDays(d)).ToList();

                    seldatelist.Add(endDate);
                    DaysGrid();

                    //HorsePlanningScreen.enddateselect(endDate);
                }
                else if (horseplanning && start_date)
                {
                    startDate = new DateTime(currentyear, currentmonth, currentdate);

                    dt = startDate;
                    currentmonth = dt.Month;
                    currentyear = dt.Year;
                    listyears = dt.Year;
                    currentdate = dt.Day;

                    if (startDate > endDate)
                    {
                        tap = true;
                        return;
                    }
                    seldatelist = Enumerable.Range(0, endDate.Subtract(startDate).Days)
                         .Select(d => startDate.AddDays(d)).ToList();

                    seldatelist.Add(endDate);
                    DaysGrid();

                    // HorsePlanningScreen.startdateselect(startDate);
                }
                else
                {
                    foreach (var item in days.Children)
                    {
                        var btn = (item as Button);
                        if (btn != null)
                        {
                            //var curr = Convert.ToInt32(btn.BindingContext);
                            var currdate = Convert.ToInt32(btn.CommandParameter);
                            if (currentdate == currdate)
                            {
                                selbtn.BackgroundColor = Color.LightBlue;
                                selbtn.TextColor = Color.Blue;
                                selbtn.CornerRadius = 18;
                            }
                            else
                            {
                                btn.TextColor = Color.Black;
                                btn.CornerRadius = 18;
                                btn.BackgroundColor = Color.Transparent;
                            }
                        }
                    }
                    InitialDate = new DateTime(currentyear, currentmonth, currentdate);
                    //Text = SelectedDateTime.ToString();
                    //dateselected(null, new DateSelectedEvent(new DateTime(currentyear, currentmonth, currentdate)));
                    SelectedDateEvent(null, new DateSelectedEvent(new DateTime(currentyear, currentmonth, currentdate)));
                }

                if (horseplanning && !start_date && !end_date)
                {
                    if (selectedstart)
                    {
                        startDate = new DateTime(currentyear, currentmonth, currentdate);
                        // HorsePlanningScreen.startdateselect(startDate);
                    }
                    else
                    {
                        endDate = new DateTime(currentyear, currentmonth, currentdate);
                        // HorsePlanningScreen.enddateselect(endDate);
                    }
                }
                tap = true;
            }
        }

        private void SetupOptionsGridUI(SelectedOption selectedOption)
        {

            switch (selectedOption)
            {
                case SelectedOption.Day:
                    DayLable.FontAttributes = FontAttributes.Bold;
                    MonthLable.FontAttributes = FontAttributes.Italic;
                    YearLable.FontAttributes = FontAttributes.Italic;
                    break;
                case SelectedOption.Month:
                    DayLable.FontAttributes = FontAttributes.Italic;
                    YearLable.FontAttributes = FontAttributes.Italic;
                    MonthLable.FontAttributes = FontAttributes.Bold;
                    break;

                case SelectedOption.Year:

                    MonthLable.FontAttributes = FontAttributes.Italic;
                    YearLable.FontAttributes = FontAttributes.Bold;
                    DayLable.FontAttributes = FontAttributes.Italic;
                    break;

                default:
                    DayLable.FontAttributes = FontAttributes.Bold;
                    MonthLable.FontAttributes = FontAttributes.Italic;
                    YearLable.FontAttributes = FontAttributes.Italic;
                    break;

            };




            //if (selectedOption == SelectedOption.Day)
            //{
            //    DayLable.FontAttributes = FontAttributes.Bold;
            //    MonthLable.FontAttributes = FontAttributes.Italic;
            //    YearLable.FontAttributes = FontAttributes.Italic;
            //}
            //else if (selectedOption == SelectedOption.Month)
            //{
            //    DayLable.FontAttributes = FontAttributes.Italic;
            //    YearLable.FontAttributes = FontAttributes.Italic;
            //    MonthLable.FontAttributes = FontAttributes.Bold;
            //}
            //else if(selectedOption==SelectedOption.Year)
            //{
            //    MonthLable.FontAttributes = FontAttributes.Italic;
            //    YearLable.FontAttributes = FontAttributes.Bold;
            //    DayLable.FontAttributes = FontAttributes.Italic;
            //}

        }

        public void MonthsGrid()
        {
            days.Children.Clear();
            int row = 0;
            Button jan = new Button { Text = culture.DateTimeFormat.GetAbbreviatedMonthName(1).ToUpper(), TextColor = Color.Black, BindingContext = (int)months.jan, FontSize = 12, BackgroundColor = Color.Transparent };//BackgroundColor = (currentyear == dt.Year && dt.Month == (int)months.jan) ? Color.Black : Color.Orange,
            Button feb = new Button { Text = culture.DateTimeFormat.GetAbbreviatedMonthName(2).ToUpper(), TextColor = Color.Black, BindingContext = (int)months.feb, FontSize = 12, BackgroundColor = Color.Transparent };
            Button mar = new Button { Text = culture.DateTimeFormat.GetAbbreviatedMonthName(3).ToUpper(), TextColor = Color.Black, BindingContext = (int)months.mar, FontSize = 12, BackgroundColor = Color.Transparent };
            Button apr = new Button { Text = culture.DateTimeFormat.GetAbbreviatedMonthName(4).ToUpper(), TextColor = Color.Black, BindingContext = (int)months.apr, FontSize = 12, BackgroundColor = Color.Transparent };
            Button may = new Button { Text = culture.DateTimeFormat.GetAbbreviatedMonthName(5).ToUpper(), TextColor = Color.Black, BindingContext = (int)months.may, FontSize = 12, BackgroundColor = Color.Transparent };
            Button jun = new Button { Text = culture.DateTimeFormat.GetAbbreviatedMonthName(6).ToUpper(), TextColor = Color.Black, BindingContext = (int)months.jun, FontSize = 12, BackgroundColor = Color.Transparent };
            Button jul = new Button { Text = culture.DateTimeFormat.GetAbbreviatedMonthName(7).ToUpper(), TextColor = Color.Black, BindingContext = (int)months.jul, FontSize = 12, BackgroundColor = Color.Transparent };
            Button aug = new Button { Text = culture.DateTimeFormat.GetAbbreviatedMonthName(8).ToUpper(), TextColor = Color.Black, BindingContext = (int)months.aug, FontSize = 12, BackgroundColor = Color.Transparent };
            Button sep = new Button { Text = culture.DateTimeFormat.GetAbbreviatedMonthName(9).ToUpper(), TextColor = Color.Black, BindingContext = (int)months.sep, FontSize = 12, BackgroundColor = Color.Transparent };
            Button oct = new Button { Text = culture.DateTimeFormat.GetAbbreviatedMonthName(10).ToUpper(), TextColor = Color.Black, BindingContext = (int)months.oct, FontSize = 12, BackgroundColor = Color.Transparent };
            Button nov = new Button { Text = culture.DateTimeFormat.GetAbbreviatedMonthName(11).ToUpper(), TextColor = Color.Black, BindingContext = (int)months.nov, FontSize = 12, BackgroundColor = Color.Transparent };
            Button dec = new Button { Text = culture.DateTimeFormat.GetAbbreviatedMonthName(12).ToUpper(), TextColor = Color.Black, BindingContext = (int)months.dec, FontSize = 12, BackgroundColor = Color.Transparent };
            if (Device.iOS == Device.RuntimePlatform)
            {
                jan.FontFamily = "Comfortaa";
                feb.FontFamily = "Comfortaa";
                mar.FontFamily = "Comfortaa";
                apr.FontFamily = "Comfortaa";
                may.FontFamily = "Comfortaa";
                jun.FontFamily = "Comfortaa";
                jul.FontFamily = "Comfortaa";
                aug.FontFamily = "Comfortaa";
                sep.FontFamily = "Comfortaa";
                oct.FontFamily = "Comfortaa";
                nov.FontFamily = "Comfortaa";
                dec.FontFamily = "Comfortaa";
            }
            jan.Clicked += Months_Clicked;
            feb.Clicked += Months_Clicked;
            mar.Clicked += Months_Clicked;
            apr.Clicked += Months_Clicked;
            may.Clicked += Months_Clicked;
            jun.Clicked += Months_Clicked;
            jul.Clicked += Months_Clicked;
            aug.Clicked += Months_Clicked;
            sep.Clicked += Months_Clicked;
            oct.Clicked += Months_Clicked;
            nov.Clicked += Months_Clicked;
            dec.Clicked += Months_Clicked;

            days.Children.Add(jan, 1, row);
            Grid.SetColumnSpan(jan, 3);

            days.Children.Add(feb, 3, row);
            Grid.SetColumnSpan(feb, 3);

            days.Children.Add(mar, 5, row);
            Grid.SetColumnSpan(mar, 3);

            row++;

            days.Children.Add(apr, 1, row);
            Grid.SetColumnSpan(apr, 3);

            days.Children.Add(may, 3, row);
            Grid.SetColumnSpan(may, 3);

            days.Children.Add(jun, 5, row);
            Grid.SetColumnSpan(jun, 3);

            row++;

            days.Children.Add(jul, 1, row);
            Grid.SetColumnSpan(jul, 3);

            days.Children.Add(aug, 3, row);
            Grid.SetColumnSpan(aug, 3);

            days.Children.Add(sep, 5, row);
            Grid.SetColumnSpan(sep, 3);

            row++;

            days.Children.Add(oct, 1, row);
            Grid.SetColumnSpan(oct, 3);

            days.Children.Add(nov, 3, row);
            Grid.SetColumnSpan(nov, 3);

            days.Children.Add(dec, 5, row);
            Grid.SetColumnSpan(dec, 3);

        }

        private void Months_Clicked(object sender, EventArgs e)
        {
            if (tap)
            {
                tap = false;
                SetupOptionsGridUI(SelectedOption.Day);
                var btn = (sender as Button);
                var bc = Convert.ToInt32(btn.BindingContext);
                currentmonth = bc;
                if (!previousdateselection)
                {
                    if (currentmonth < DateTime.Now.Month && currentyear <= DateTime.Now.Year)
                    {
                        currentmonth = dt.Month;
                        tap = true;
                        return;
                    }
                }
                if (!nextdateselection)
                {
                    if (currentmonth > DateTime.Now.Month && currentyear >= DateTime.Now.Year)
                    {
                        currentmonth = dt.Month;
                        tap = true;
                        return;
                    }
                }
                var monthname = new DateTime(currentyear, currentmonth, 1).ToString("MMMM", culture);
                heading.Text = monthname + " " + currentyear;
                heading.BindingContext = 0;
                leftarrow.BindingContext = 0;
                rightarrow.BindingContext = 0;
                DaysGrid();
                tap = true;
            }
        }

        public enum months
        {
            jan = 1,
            feb = 2,
            mar = 3,
            apr = 4,
            may = 5,
            jun = 6,
            jul = 7,
            aug = 8,
            sep = 9,
            oct = 10,
            nov = 11,
            dec = 12
        }

        public void YearsGrid(int y)
        {
            days.Children.Clear();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 1; j <= 7; j++)
                {
                    y++;
                    CustomLabel year = new CustomLabel { HorizontalTextAlignment = TextAlignment.Center, BindingContext = y, Text = y.ToString(), TextColor = Color.Black, FontSize = 12, BackgroundColor = Color.Transparent }; //BackgroundColor = (y == dt.Year) ? Color.Black : Color.Orange,
                    if (Device.iOS == Device.RuntimePlatform)
                        year.FontFamily = "Comfortaa";
                    var gesture = new TapGestureRecognizer();
                    gesture.Tapped += Year_Clicked;
                    year.GestureRecognizers.Add(gesture);
                    days.Children.Add(year, j, i);
                }
            }

        }

        private void Year_Clicked(object sender, EventArgs e)
        {
            if (tap)
            {
                tap = false;
                var btn = (sender as CustomLabel);
                var bc = Convert.ToInt32(btn.BindingContext);
                currentyear = bc;
                if (!previousdateselection)
                {
                    if (currentyear < DateTime.Now.Year)
                    {
                        currentyear = dt.Year;
                        tap = true;
                        return;
                    }
                }
                if (!nextdateselection)
                {
                    if (currentyear > DateTime.Now.Year)
                    {
                        currentyear = dt.Year;
                        tap = true;
                        return;
                    }
                }
                heading.Text = currentyear.ToString();
                heading.BindingContext = 1;
                leftarrow.BindingContext = 1;
                rightarrow.BindingContext = 1;
                MonthsGrid();
            }
            tap = true;
        }

        ///// <summary>
        ///// Returns the input string with the first character converted to uppercase, or mutates any nulls passed into string.Empty
        ///// </summary>
        //public static string FirstLetterToUpperCaseOrConvertNullToEmptyString(string s)
        //{
        //    if (string.IsNullOrEmpty(s))
        //        return string.Empty;

        //    char[] a = s.ToCharArray();
        //    a[0] = char.ToUpper(a[0]);
        //    return new string(a);
        //}


        public enum SelectedOption
        {
            Day = 1,
            Month = 2,
            Year = 3,
            Default = 4
        }




    }
}