using RestSharp;
using Zenith_MAUI.Business.DTO;
using Zenith_MAUI.Common;

namespace Zenith_MAUI.Pages;

public partial class SingleTrack : ContentPage
{
    public int Id { get; set; }

    public MProp<TrackDTO> Track { get; set; } = new MProp<TrackDTO>();

    public SingleTrack(int id)
	{
		Id = id;
		InitializeComponent();
	}

    public void LoadTrack()
	{

            string token = SecureStorage.Default.GetUser().Token;

            RestRequest request = new RestRequest("track/" + Id);

            var response = Api.Client.Execute<TrackDTO>(request);


            if (response.IsSuccessful)
            {

                Track.Value = response.Data;
            }

    }
}