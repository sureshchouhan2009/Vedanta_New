
using Newtonsoft.Json;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using Vedanta.Constants;
using Vedanta.Models;
using Vedanta.Service;
using Vedanta.Utility;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Vedanta.ViewModel
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        private ICommand loginComand;

        public ICommand LoginComand
        {
            get
            {
                if (loginComand == null)
                {
                    loginComand = new Command(PerformLoginComand);
                }

                return loginComand;
            }
        }

        private async void PerformLoginComand()
        {
           

            if (string.IsNullOrEmpty(EmailText) || string.IsNullOrEmpty(PasswordText))
                await Application.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else
            {
                //if (AppConstansts.IsValidEmail(EmailText) && AppConstansts.IsValidPassword(PasswordText))
                if (0==0)
                {
                    var response = await ApiService.Instance.LoginApiCall("Umesh.ecgit", "abc@1234");
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var LoginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(result);
                        if (RememberMe)
                        {
                            //commented for now

                            //Preferences.Set("UserName", EmailText);
                            //Preferences.Set("Password", PasswordText);
                            //Preferences.Set("IsLoggedIN", true); 
                            
                            Preferences.Set("UserName", "Umesh.ecgit");
                            Preferences.Set("Password", "abc@1234");
                            Preferences.Set("IsLoggedIN", true);
                        }
                        var StartDate = new DateTime(2021, 11, 01).Date.ToString();
                        var EndDate = new DateTime(2021, 12, 21).Date.ToString();
                        Session.Instance.GembaScheduleList = await ApiService.Instance.GembaScheduleListApiCall(StartDate, EndDate);
                       await NavigationService.NavigateAsync("GembaSchedule");
                    }
                    else
                    {

                    }

                }
                else
                    await Application.Current.MainPage.DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
            }
        }








        private ICommand forgotPasswordCommand;

        public ICommand ForgotPasswordCommand
        {
            get
            {
                if (forgotPasswordCommand == null)
                {
                    forgotPasswordCommand = new Command(ForgotPassword);
                }

                return forgotPasswordCommand;
            }
        }

        private async void ForgotPassword()
        {
            await NavigationService.NavigateAsync("ForgotPasswordPage");
        }

        private string emailText;

        public string EmailText
        {
            get => emailText; set
            {

                SetProperty(ref emailText, value);
            }
        }
        private bool _rememberMe=false;

        public bool RememberMe
        {
            get => _rememberMe; 
            set
            {

                SetProperty(ref _rememberMe, value);
            }
        }

        private string passwordText;

        public string PasswordText { get => passwordText; set => SetProperty(ref passwordText, value); }

        private ICommand signUpCommand;

        public ICommand SignUpCommand
        {
            get
            {
                if (signUpCommand == null)
                {
                    signUpCommand = new Command(SignUp);
                }

                return signUpCommand;
            }
        }

        private async void SignUp()
        {
            await NavigationService.NavigateAsync("CreateAccountPage");
        }


        //public async void LoginAPI(string token, string username, string password)
        //{
        //    string ErrorMessage = null;
        //    try
        //    {
        //        var client = new HttpClient();
        //        client.BaseAddress = new Uri("https://vedantaconnect.com/ECGITWEBAPI");// ("");
        //                                                                               //var postData = new List<KeyValuePair<string, string>>();
        //        var authHeader = new AuthenticationHeaderValue("bearer", token);

        //        client.DefaultRequestHeaders.Authorization = authHeader;

        //        var url = "https://vedantaconnect.com/ECGITWEBAPI/Account/ValidateUser?userName=" + username + "&password=" + password + "";

        //        var res = await client.GetAsync(url);
        //        if (res.IsSuccessStatusCode)
        //        {
        //            string result = await res.Content.ReadAsStringAsync();
        //            var userData = JsonConvert.DeserializeObject<Tokens>(result);
                    
        //            Application.Current.Properties["IsLoggedIn"] = true;
        //            Application.Current.Properties["Name"] = userData.Name;
        //            await Application.Current.SavePropertiesAsync();
        //           // await Navigation.PushAsync(new GambaWalkDetails());
        //        }
        //        else
        //        {
        //            ErrorMessage = "Incorrect Username or Password";
        //            //await DisplayAlert("Login Failed", ErrorMessage, "Ok");
        //            //txtUsername.Text = "";
        //            //txtPassword.Text = "";
        //            //Indicator.IsEnabled = false;
        //            //Indicator.IsRunning = false;
        //            //Indicator.IsVisible = false;
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

    }
}
