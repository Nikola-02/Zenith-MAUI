using System.Diagnostics;
using Zenith_MAUI.Pages;

namespace Zenith_MAUI.Components;

public partial class TrackComponent : ContentView
{
    public static BindableProperty NameProperty =
        BindableProperty.Create(nameof(Name), typeof(string), typeof(TrackComponent), "/", BindingMode.TwoWay);

    public static BindableProperty IdProperty =
        BindableProperty.Create(nameof(Id), typeof(int), typeof(TrackComponent), 0, BindingMode.OneWay);

    public static BindableProperty DescriptionProperty =
        BindableProperty.Create(nameof(Description), typeof(string), typeof(TrackComponent), "/", BindingMode.OneWay);

    public static BindableProperty ImageProperty =
        BindableProperty.Create(nameof(Image), typeof(string), typeof(TrackComponent), "http://localhost:5001/tracks/images/1cdbfe37-880a-4b09-896d-cf900d056a37.jpg", BindingMode.OneWay);

    public static BindableProperty PriceProperty =
        BindableProperty.Create(nameof(Price), typeof(double), typeof(TrackComponent), 0.00, BindingMode.OneWay);

    public static BindableProperty DurationProperty =
        BindableProperty.Create(nameof(Duration), typeof(int), typeof(TrackComponent), 0, BindingMode.OneWay);

    public static BindableProperty ArtistProperty =
       BindableProperty.Create(nameof(Artist), typeof(string), typeof(TrackComponent), "/", BindingMode.OneWay);

    public static BindableProperty AlbumProperty =
      BindableProperty.Create(nameof(Album), typeof(string), typeof(TrackComponent), "/", BindingMode.OneWay);
    
    public static BindableProperty GenreProperty =
      BindableProperty.Create(nameof(Genre), typeof(string), typeof(TrackComponent), "/", BindingMode.OneWay);

    public static BindableProperty LikesCountProperty =
       BindableProperty.Create(nameof(LikesCount), typeof(int), typeof(TrackComponent), 0, BindingMode.OneWay);

    public TrackComponent()
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

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public string Image
    {
        get => (string)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    public double Price
    {
        get => (double)GetValue(PriceProperty);
        set => SetValue(PriceProperty, value);
    }

    public int Duration
    {
        get => (int)GetValue(DurationProperty);
        set => SetValue(DurationProperty, value);
    }

    public string Artist
    {
        get => (string)GetValue(ArtistProperty);
        set => SetValue(ArtistProperty, value);
    }

    public string Album
    {
        get => (string)GetValue(AlbumProperty);
        set => SetValue(AlbumProperty, value);
    }

    public string Genre
    {
        get => (string)GetValue(GenreProperty);
        set => SetValue(GenreProperty, value);
    }

    public int LikesCount
    {
        get => (int)GetValue(LikesCountProperty);
        set => SetValue(LikesCountProperty, value);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PushModalAsync(new SingleTrack(Id, Name, Description, Duration, Price, Image, Artist, Album, Genre, LikesCount));
    }
}