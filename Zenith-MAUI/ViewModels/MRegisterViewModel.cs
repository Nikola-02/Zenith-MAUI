using FluentValidation.Results;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zenith_MAUI.Common;
using Zenith_MAUI.Pages;
using Zenith_MAUI.Validators;

namespace Zenith_MAUI.ViewModels
{
    public class MRegisterViewModel
    {
        public MProp<string> Username { get; set; } = new MProp<string>();
        public MProp<string> Email { get; set; } = new MProp<string>();
        public MProp<string> FirstName { get; set; } = new MProp<string>();
        public MProp<string> LastName { get; set; } = new MProp<string>();
        public MProp<string> Password { get; set; } = new MProp<string>();
        public MProp<bool> ButtonEnabled { get; set; } = new MProp<bool>();
        public MProp<bool> ErrorOccurred { get; set; } = new MProp<bool>();

        public MRegisterViewModel()
        {
            RegisterCommand = new Command(Register);
            GoToLoginCommand = new Command(GoToLogin);

            Username.OnChange = Validate;
            Email.OnChange = Validate;
            FirstName.OnChange = Validate;
            LastName.OnChange = Validate;
            Password.OnChange = Validate;
            ButtonEnabled.Value = true;

        }

        public ICommand RegisterCommand { get; }
        public ICommand GoToLoginCommand { get; }

        private void GoToLogin()
        {
            App.Current.MainPage = new Login();
        }

        private void Validate()
        {
            MRegisterViewModelValidator validator = new MRegisterViewModelValidator();

            ValidationResult result = validator.Validate(this);

            if (!result.IsValid)
            {
                Username.Error = result.GetError("Username");
                Email.Error = result.GetError("Email");
                FirstName.Error = result.GetError("FirstName");
                LastName.Error = result.GetError("LastName");
                Password.Error = result.GetError("Password");
            }
            else
            {
                Username.Error = null;
                FirstName.Error = null;
                LastName.Error = null;
                Email.Error = null;
                Password.Error = null;

                ButtonEnabled.Value = true;
            }
        }

        private async void Register()
        {
            string username = Username.Value;
            string email = Email.Value;
            string firstName = FirstName.Value;
            string lastName = LastName.Value;
            string password = Password.Value;

            RestRequest request = new RestRequest("users");

            request.AddJsonBody(new { username, email, password, firstName, lastName });

            var response = Api.Client.Post(request);

            if (response.IsSuccessful)
            {
                App.Current.MainPage = new Login();
            }
            else
            {
                ErrorOccurred.Value = true;
            }
        }
    }
}
