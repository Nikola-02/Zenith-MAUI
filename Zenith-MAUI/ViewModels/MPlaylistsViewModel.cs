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
using Zenith_MAUI.Pages;

namespace Zenith_MAUI.ViewModels
{
    public class MPlaylistsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PlaylistDTO> Playlists { get; set; } = new ObservableCollection<PlaylistDTO>();

        public MPlaylistsViewModel()
        {
            LoadPlaylists();

            RefreshPageCommand = new Command(LoadPlaylists);
            CloseCommand = new Command(Close);
        }

        public ICommand RefreshPageCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private void Close()
        {
            App.Current.MainPage = new UserPage();
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
                OnPropertyChanged(nameof(Playlists));
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
