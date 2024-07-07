using Microsoft.Maui;
using RestSharp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Zenith_MAUI.Business.DTO;
using Zenith_MAUI.Common;

namespace Zenith_MAUI.Pages;

public partial class SingleTrack : ContentPage
{
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
    public MProp<bool> IsLiked { get; set; } = new MProp<bool>();
    public MProp<bool> IsError { get; set; } = new MProp<bool>();
    public MProp<bool> IsConflict { get; set; } = new MProp<bool>();
    public MProp<bool> IsAdded { get; set; } = new MProp<bool>();

    public ObservableCollection<PlaylistDTO> Playlists { get; set; } = new ObservableCollection<PlaylistDTO>();

    public MProp<PlaylistDTO> SelectedPlaylist { get; set; } = new MProp<PlaylistDTO>();


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

        GetTrack();
        GetPlaylistsForUser();
    }

    private void GetTrack()
    {
        RestRequest request = new RestRequest("trackLikes/" + Id.Value);

        var response = Api.Client.Execute<ExistsDTO>(request);


        if (response.IsSuccessful)
        {
            IsLiked.Value = (bool)response.Data.exists;
            OnPropertyChanged(nameof(IsLiked));
        }
    }

    private void GetPlaylistsForUser()
    {
        RestRequest request = new RestRequest("playlists/mine");

        var response = Api.Client.Execute<PagedResponse<PlaylistDTO>>(request);

        if (response.IsSuccessful)
        {
            var playlists = response.Data.Data;

            Playlists.Clear();

            foreach (var item in playlists)
            {
                Playlists.Add(item);
            }

            SelectedPlaylist.Value = Playlists.FirstOrDefault();


            OnPropertyChanged(nameof(Playlists));
            OnPropertyChanged(nameof(SelectedPlaylist));
        }
    }

    public void LikeUndoTrack()
	{
        if (IsLiked.Value)
        {
            LikesCount.Value--;
            IsLiked.Value = false;

            OnPropertyChanged(nameof(IsLiked));

            UndoTrackRequest();
        }
        else
        {
            LikesCount.Value++;
            IsLiked.Value = true;

            OnPropertyChanged(nameof(IsLiked));

            LikeTrackRequest();
        }

       

    }

    private void LikeTrackRequest()
    {
        RestRequest request = new RestRequest("trackLikes");

        request.AddJsonBody(new { trackId = Id.Value });

        var response = Api.Client.Post(request);

    }

    private void UndoTrackRequest()
    {
        RestRequest request = new RestRequest("trackLikes/" + Id.Value);

        var response = Api.Client.Delete(request);

    }


    private void Button_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PopModalAsync();
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        LikeUndoTrack();
    }

    private void Button_Clicked_2(object sender, EventArgs e)
    {
        try
        {
            var playlistId = SelectedPlaylist.Value.Id;

            RestRequest request = new RestRequest("playlists/" + playlistId + "/track");

            request.AddJsonBody(new { trackId = Id.Value });

            var response = Api.Client.Post(request);

            if (response.IsSuccessful)
            {
                IsAdded.Value = true;

                IsError.Value = false;
                IsConflict.Value = false;
            }
            else
            {
                IsAdded.Value = false;
                IsError.Value = true;
            }
        }
        catch (Exception ex)
        {
            var exMessage = ex.Message;

            IsAdded.Value = false;

            if (exMessage == "Request failed with status code Conflict")
            {
                IsConflict.Value = true;
            }
            else
            {
                IsError.Value = true;
            }
        }
        
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}