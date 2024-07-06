using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Zenith_MAUI.Pages;

public partial class AddPlaylistPage : ContentPage
{
	public AddPlaylistPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PopModalAsync();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}