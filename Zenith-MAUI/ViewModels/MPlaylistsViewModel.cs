using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playlists;
using Zenith_MAUI.Business.DTO;

namespace Zenith_MAUI.ViewModels
{
    public class MPlaylistsViewModel
    {
        public ObservableCollection<PlaylistDTO> Playlists { get; set; } = new ObservableCollection<PlaylistDTO>();

        public MPlaylistsViewModel()
        {
            LoadPlaylists();
        }

        private void LoadPlaylists()
        {
            RestRequest request = new RestRequest("playlists/mine");

            var response = Api.Client.Execute<PagedResponse<PlaylistDTO>>(request);

            if (response.IsSuccessful)
            {
                var playlists = response.Data.Data;
                Playlists.Clear();
                Playlists = new ObservableCollection<PlaylistDTO>(playlists);
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
