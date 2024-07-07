using FluentValidation.Results;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zenith_MAUI.Business.DTO;
using Zenith_MAUI.Common;
using Zenith_MAUI.Pages;
using Zenith_MAUI.Validators;

namespace Zenith_MAUI.ViewModels
{
    public class MLoginViewModel
    {
        public MProp<string> Email { get; set; } = new MProp<string>();
        public MProp<string> Password { get; set; } = new MProp<string>();
        public MProp<bool> ButtonEnabled { get; set; } = new MProp<bool>();
        public MProp<bool> InvalidCredentials { get; set; } = new MProp<bool>();

        public MLoginViewModel()
        {
            LoginCommand = new Command(Login);
            GoToRegisterCommand = new Command(GoToRegister);

            Email.OnChange = Validate;
            Password.OnChange = Validate;
            ButtonEnabled.Value = true;

            Email.Value = "admin@gmail.com";
            Password.Value = "Admin1234";
        }

        public ICommand LoginCommand { get; }
        public ICommand GoToRegisterCommand { get; }

        private void GoToRegister()
        {
            App.Current.MainPage = new Register();
        }

        private void Validate()
        {
            MLoginViewModelValidator validator = new MLoginViewModelValidator();

            ValidationResult result = validator.Validate(this);

            if (!result.IsValid)
            {
                Email.Error = result.GetError("Email");
                Password.Error = result.GetError("Password");
            }
            else
            {
                Email.Error = null;
                Password.Error = null;

                ButtonEnabled.Value = true;
            }
        }

        private async void Login()
        {
            string email = Email.Value;
            string password = Password.Value;

            RestRequest request = new RestRequest("auth", Method.Post);
            request.AddJsonBody(new { email, password });

            RestResponse<TokenDTO> response = Api.Client.Execute<TokenDTO>(request);

            if (response.IsSuccessful)
            {

                Task t = SecureStorage.Default.SetAsync("token", response.Data.Token);

                await t;

                User u = SecureStorage.Default.GetUser();

                InvalidCredentials.Value = false;

                App.Current.MainPage = new UserPage();
            }
            else
            {
                InvalidCredentials.Value = true;
            }
        }
    }
}
