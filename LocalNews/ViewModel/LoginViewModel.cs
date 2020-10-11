using LocalNews.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LocalNews.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string  name;

        public string  Name
        {
            get { return name; }
            set 
            { 
                name = value; 
                OnPropertyChanged("Name");
                //added below code to evaluate the below propeties when "Name" property is changed.
                OnPropertyChanged("CanRegister");
            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set 
            { 
                email = value; 
                OnPropertyChanged("Email");
                OnPropertyChanged("CanRegister");
                OnPropertyChanged("CanLogin");  
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set 
            { 
                password = value; 
                OnPropertyChanged("Password");
                OnPropertyChanged("CanRegister");
                OnPropertyChanged("CanLogin");
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set 
            { 
                confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
                OnPropertyChanged("CanRegister");
            }
        }

        public bool CanLogin
        {
            get
            {
                return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
            }
            
        }
        public bool CanRegister
        {
            get
            {
                return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(ConfirmPassword);
            }
            
        }

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public LoginViewModel()
        {
            LoginCommand = new Command(Login, LoginCanExecute);
            RegisterCommand = new Command(Register, RegisterCanExcecute);
        }

        private bool RegisterCanExcecute(object parameter)
        {
            return this.CanRegister;
        }

        private async void Register(object obj)
        {
            if (Password != ConfirmPassword)
                await App.Current.MainPage.DisplayAlert("Error", "Passwords don't match.", "Ok");
            else
            {
                var result= await Auth.RegisterUser(Name, Email, Password);
                if (result)
                    await App.Current.MainPage.Navigation.PopAsync();
            }
                
        }

        private bool LoginCanExecute(object parameter)
        {
            return this.CanLogin;
        }

        private async void Login(object parameter)
        {
            var result = await Auth.AuthenticateUser(Email, Password);
            if (result)
                await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
