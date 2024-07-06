using FluentValidation;
using FluentValidation.Results;
using RestSharp;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Zenith_MAUI.Common;
using Zenith_MAUI.Validators;

namespace Zenith_MAUI.Pages;

public partial class EditPlaylistPage : ContentPage
{
    public int Id { get; set; }
    public MProp<string> Name { get; set; } = new MProp<string>();
    public MProp<string> Error { get; set; } = new MProp<string>();

    public EditPlaylistPage(int id, string name)
    {
        Id = id;
        Name.Value = name;

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
            MEditPlaylistValidator validator = new MEditPlaylistValidator();

            ValidationResult result = validator.Validate(this);

            if (result.IsValid)
            {
                RestRequest request = new RestRequest("playlists/" + Id);

                request.AddJsonBody(new { name });

                var response = Api.Client.Put(request);

                if (response.IsSuccessful)
                {
                    App.Current.MainPage.Navigation.PopModalAsync();
                }
                else
                {
                    Error.Value = response.ErrorMessage;
                }
            }
            
        }
        catch (Exception ex)
        {
            Error.Value = "Naziv je zauzet.";
        }

    }

    private void Validate()
    {
        MEditPlaylistValidator validator = new MEditPlaylistValidator();

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
}