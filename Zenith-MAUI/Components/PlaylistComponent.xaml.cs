using RestSharp;
using System.Diagnostics;
using System.Xml.Linq;
using Zenith_MAUI.Common;
using Zenith_MAUI.Pages;

namespace Zenith_MAUI.Components;

public partial class PlaylistComponent : ContentView
{
    public static BindableProperty NameProperty =
        BindableProperty.Create(nameof(Name), typeof(string), typeof(PlaylistComponent), "/", BindingMode.TwoWay);

    public static BindableProperty IdProperty =
        BindableProperty.Create(nameof(Id), typeof(int), typeof(PlaylistComponent), 0, BindingMode.OneWay);

    public static BindableProperty CreatedAtProperty =
        BindableProperty.Create(nameof(CreatedAt), typeof(DateTime), typeof(PlaylistComponent), DateTime.MinValue, BindingMode.OneWay);

    public static BindableProperty TracksCountProperty =
       BindableProperty.Create(nameof(TracksCount), typeof(int), typeof(PlaylistComponent), 0, BindingMode.OneWay);

    public MProp<string> Error { get; set; } = new MProp<string>();

    public PlaylistComponent()
	{
		InitializeComponent();
	}

    public string Name
    {
        get => (string)GetValue(NameProperty);
        set => SetValue(NameProperty, value);
    }

    public int Id
    {
        get => (int)GetValue(IdProperty);
        set => SetValue(IdProperty, value);
    }

    public DateTime CreatedAt
    {
        get => (DateTime)GetValue(CreatedAtProperty);
        set => SetValue(CreatedAtProperty, value);
    }

    public int TracksCount
    {
        get => (int)GetValue(TracksCountProperty);
        set => SetValue(TracksCountProperty, value);
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PushModalAsync(new EditPlaylistPage(Id, Name));
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        try
        {
            RestRequest request = new RestRequest("playlists/" + Id);

            var response = Api.Client.Delete(request);

            if (!response.IsSuccessful)
            {
                Error.Error = response.ErrorMessage;
            }
            else
            {
                App.Current.MainPage = new NavigationPage(new Playlists());
            }
        }
        catch (Exception ex)
        {
            Error.Error = "Desila se greska";
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PopToRootAsync();
        App.Current.MainPage.Navigation.PushModalAsync(new SinglePlaylist(Id));
    }
}