using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Vedanta.Constants;
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
                if (AppConstansts.IsValidEmail(EmailText) && AppConstansts.IsValidPassword(PasswordText))
                {
                    await NavigationService.NavigateAsync("HomePage");
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

    }
}
