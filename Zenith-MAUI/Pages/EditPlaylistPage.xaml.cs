using RestSharp;
using Zenith_MAUI.Common;

namespace Zenith_MAUI.Pages;

public partial class EditPlaylistPage : ContentPage
{
    public int Id { get; set; }
    public MProp<string> Name { get; set; } = new MProp<string>();
    public MProp<string> Error { get; set; } = new MProp<string>();

    public EditPlaylistPage()
    {
        Name.OnChange = Validate;

        InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PopModalAsync();
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        var name = Name.Value;

        try
        {
            RestRequest request = new RestRequest("playlists/");

            request.AddJsonBody(new { name });

            var response = Api.Client.Post(request);

            if (response.IsSuccessful)
            {
                App.Current.MainPage.Navigation.PopModalAsync();
                //App.Current.MainPage.Navigation.PushAsync(new Playlists());
            }
            else
            {
                Error.Value = response.ErrorMessage;
            }
        }
        catch (Exception ex)
        {
            Error.Value = "Naziv je zauzet.";
        }

    }

    private void Validate()
    {
        MAddEditPlaylistValidator validator = new MAddEditPlaylistValidator();

        ValidationResult result = validator.Validate(this);

        if (!result.IsValid)
        {
            Name.Error = result.GetError("Name");
        }
        else
        {
            Name.Error = null;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {

    }
}