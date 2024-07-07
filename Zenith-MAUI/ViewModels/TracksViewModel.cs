using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zenith_MAUI.Business.DTO;
using Zenith_MAUI.Common;

namespace Zenith_MAUI.ViewModels
{
    public class TracksViewModel
    {
        public MProp<string> Keyword { get; set; } = new MProp<string>();
        public static int PerPage => 6;
        public MProp<int> Page { get; set; } = new MProp<int>();

        private ObservableCollection<TrackDTO> tracks = new ObservableCollection<TrackDTO>();

        public ObservableCollection<TrackDTO> Tracks
        {
            get => tracks;
            set
            {
                tracks = value;
                OnPropertyChanged();
            }
        }
        public ResponseObjForPagination ResponseObj { get; set; } = new ResponseObjForPagination();

        public ICommand NextCommand { get; }
        public ICommand PrevCommand { get; }
        public ICommand RefreshPageCommand { get; }

        public TracksViewModel()
        {
            Keyword.OnChange = LoadTracks;

            NextCommand = new Command(Next);
            PrevCommand = new Command(Prev);
            RefreshPageCommand = new Command(RefreshPage);

            Tracks = new ObservableCollection<TrackDTO>();

            Page.Value = 1;

            LoadTracks();
        }

        public void RefreshPage()
        {
            LoadTracks();
        }

        public void Next()
        {
            if (Page.Value < ResponseObj.Pages)
            {
                Page.Value++;
                LoadTracks();
            }
        }

        public void Prev()
        {
            if (Page.Value > 1)
            {
                Page.Value--;
                LoadTracks();
            }
        }

        public void LoadTracks()
        {
            if (!string.IsNullOrEmpty(Keyword.Value))
            {
                string token = SecureStorage.Default.GetUser().Token;

                RestRequest request = new RestRequest("tracks");
                request.AddParameter("keyword", Keyword.Value, ParameterType.QueryString);
                request.AddParameter("perPage", PerPage, ParameterType.QueryString);
                request.AddParameter("page", Page.Value, ParameterType.QueryString);

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
                request.AddParameter("page", Page.Value, ParameterType.QueryString);

                var response = Api.Client.Execute<PagedResponse<TrackDTO>>(request);

                ResponseObj.CurrentPage = response.Data.CurrentPage;
                ResponseObj.PerPage = response.Data.PerPage;
                ResponseObj.Pages = response.Data.Pages;
                ResponseObj.TotalCount = response.Data.TotalCount;



                if (response.IsSuccessful)
                {
                    var tracks = response.Data.Data;
                    Tracks.Clear();
                    foreach (var track in tracks)
                    {
                        Tracks.Add(track);
                    }
                    //this.Tracks = new ObservableCollection<TrackDTO>(tracks);
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
