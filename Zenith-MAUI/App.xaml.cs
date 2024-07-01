﻿
namespace Zenith_MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
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
