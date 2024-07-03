using RestSharp;
using System.ComponentModel;
using Zenith_MAUI.Business.DTO;
using Zenith_MAUI.Common;

namespace Zenith_MAUI.Pages;

public partial class SingleTrack : ContentPage

{
    //public int Id { get; set; }
    //public string Name { get; set; }
    //public string Description { get; set; }
    //public int Duration { get; set; }
    //public double Price { get; set; }
    //public string Image { get; set; }
    //public string Artist { get; set; }
    //public string Album { get; set; }
    //public string Genre { get; set; }
    //public int LikesCount { get; set; }

    public MProp<int> Id { get; set; } = new MProp<int>();
    public MProp<string> Name { get; set; } = new MProp<string>();
    public MProp<double> Price { get; set; } = new MProp<double>();
    public MProp<string> Description { get; set; } = new MProp<string>();
    public MProp<string> Image { get; set; } = new MProp<string>();
    public MProp<int> Duration { get; set; } = new MProp<int>();
    public MProp<string> Artist { get; set; } = new MProp<string>();
    public MProp<string> Album { get; set; } = new MProp<string>();
    public MProp<string> Genre { get; set; } = new MProp<string>();
    public MProp<int> LikesCount { get; set; } = new MProp<int>();

    //public static BindableProperty TrackProperty =
    //   BindableProperty.Create(nameof(Track), typeof(TrackDTO), typeof(SingleTrack), new TrackDTO(), BindingMode.OneWay);


    //public TrackDTO Track
    //{
    //    get => (TrackDTO)GetValue(TrackProperty);
    //    set => SetValue(TrackProperty, value);
    //}

    //public MProp<TrackDTO> Track { get; set; } = new MProp<TrackDTO>();

    public SingleTrack(int id, string name, string description, int duration, double price, string image, string artist, string album, string genre, int likesCount)
    {

        InitializeComponent();

        Id.Value = id;
        Name.Value = name;
        Description.Value = description;
        Duration.Value = duration;
        Price.Value = price;
        Image.Value = image;
        Artist.Value = artist;
        Album.Value = album;
        Genre.Value = genre;
        LikesCount.Value = likesCount;
    }

    public void LoadTrack()
	{

            //RestRequest request = new RestRequest("tracks/" + Id);

            //var response = Api.Client.Execute<TrackDTO>(request);


            //if (response.IsSuccessful)
            //{
            //    Track = response.Data;

            //    BindingContext = Track;

            //    OnPropertyChanged(nameof(Track));
            //}

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PopModalAsync();
    }
}