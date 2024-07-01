
using Zenith_MAUI.Common;
using Zenith_MAUI.Pages;

namespace Zenith_MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var user = SecureStorage.Default.GetUser();

            if (user != null)
            {
                MainPage = new UserPage();
            }
            else
            {
                MainPage = new Login();
            }
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);

            window.Width = 400;

            return window;
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}
