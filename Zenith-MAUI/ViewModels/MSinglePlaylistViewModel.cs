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
    public class MSinglePlaylistViewModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public ObservableCollection<TrackDTO> Tracks { get; set; } = new ObservableCollection<TrackDTO>();

        public MProp<string> PlaylistTitle { get; set; } = new MProp<string>();

        //public ICommand RemoveFromPlaylistCommand { get; }

        public MSinglePlaylistViewModel(int id)
        {
            Id = id;
            //RemoveFromPlaylistCommand = new Command<TrackDTO>(RemoveFromPlaylist);
            LoadTracksForPlaylist();

        }

        public void RemoveFromPlaylist(int id)
        {
            try
            {

                if (id != 0 && Tracks.Any(x=>x.Id == id))
                {

                    RestRequest request = new RestRequest("playlists/" + Id + "/track");

                    request.AddJsonBody(new { trackId = id });

                    var deleteResponse = Api.Client.Delete(request);

                    LoadTracksForPlaylist();
                }
            }
            catch (Exception ex)
            {
                var exceptionMessage = ex.Message;
            }

            
        }

        private void LoadTracksForPlaylist()
        {
            RestRequest request = new RestRequest("playlists/" + Id);

            var response = Api.Client.Execute<PlaylistDTO>(request);

            if (response.IsSuccessful)
            {
                var playlist = response.Data;

                PlaylistTitle.Value = playlist.Name;

                Tracks.Clear();

                foreach (var item in playlist.Tracks)
                {
                    Tracks.Add(item);
                }

                //Tracks = new ObservableCollection<TrackDTO>(playlist.Tracks);
                OnPropertyChanged(nameof(Tracks));
                OnPropertyChanged(nameof(PlaylistTitle));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
