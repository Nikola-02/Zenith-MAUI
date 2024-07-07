
using Zenith_MAUI.ViewModels;

namespace Zenith_MAUI.Pages;

public partial class SinglePlaylist : ContentPage
{
    public int Id { get; set; }

    public MSinglePlaylistViewModel SinglePlaylistViewModel { get; set; }

    public SinglePlaylist(int id)
    {
        Id = id;
        var viewModel = new MSinglePlaylistViewModel(Id);
        BindingContext = viewModel;
        SinglePlaylistViewModel = viewModel;

        InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PopAsync();
        App.Current.MainPage.Navigation.PopModalAsync();
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        int trackId = (int)button.CommandParameter;

        SinglePlaylistViewModel.RemoveFromPlaylist(trackId);
    }
}