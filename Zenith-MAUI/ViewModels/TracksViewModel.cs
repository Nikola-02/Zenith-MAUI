using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Zenith_MAUI.Business.DTO;
using Zenith_MAUI.Common;

namespace Zenith_MAUI.ViewModels
{
    public class TracksViewModel
    {
        public MProp<string> Keyword { get; set; } = new MProp<string>();
        public static int PerPage => 6;

        public ObservableCollection<TrackDTO> Tracks { get; set; } = new ObservableCollection<TrackDTO>();
        public ResponseObjForPagination ResponseObj { get; set; } = new ResponseObjForPagination();

        public TracksViewModel()
        {
            Keyword.OnChange = LoadTracks;

            LoadTracks();
        }

        public void LoadTracks()
        {
            if (!string.IsNullOrEmpty(Keyword.Value))
            {
                string token = SecureStorage.Default.GetUser().Token;

                RestRequest request = new RestRequest("tracks");
                request.AddParameter("keyword", Keyword.Value, ParameterType.QueryString);
                request.AddParameter("perPage", PerPage, ParameterType.QueryString);

                var response = Api.Client.Execute<PagedResponse<TrackDTO>>(request);

                ResponseObj.CurrentPage = response.Data.CurrentPage;
                ResponseObj.PerPage = response.Data.PerPage;
                ResponseObj.Pages = response.Data.Pages;
                ResponseObj.TotalCount = response.Data.TotalCount;

                Tracks.Clear();

                foreach (var track in response.Data.Data)
                {
                    Tracks.Add(track);
                }
            }
            else
            {
                string token = SecureStorage.Default.GetUser().Token;

                RestRequest request = new RestRequest("tracks");
                request.AddParameter("perPage", PerPage, ParameterType.QueryString);

                var response = Api.Client.Execute<PagedResponse<TrackDTO>>(request);

                ResponseObj.CurrentPage = response.Data.CurrentPage;
                ResponseObj.PerPage = response.Data.PerPage;
                ResponseObj.Pages = response.Data.Pages;
                ResponseObj.TotalCount = response.Data.TotalCount;

                if (response.IsSuccessful)
                {
                    var tracks = response.Data.Data;
                    this.Tracks.Clear();
                    this.Tracks = new ObservableCollection<TrackDTO>(tracks);
                }

            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
