using System.Xml.Linq;

namespace Zenith_MAUI.Pages;

public partial class Playlists : ContentPage
{
	public Playlists()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PushModalAsync(new AddPlaylistPage());
    }
}