using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zenith_MAUI.Common;
using Zenith_MAUI.Pages;

namespace Zenith_MAUI.ViewModels
{
    public class MProfileViewModel
    {
        public MProp<string> Username { get; set; } = new MProp<string>();

        public MProfileViewModel()
        {
            var user = SecureStorage.Default.GetUser();

            Username.Value = user.Username;

            LogoutCommand = new Command(Logout);
        }

        public ICommand LogoutCommand { get; }

        public void Logout()
        {
            SecureStorage.Default.Remove("token");
            App.Current.MainPage = new Login();
        }
    }
}
