using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpotifyApp
{
    class User
    {
        private PrivateUser RawProfile;
        private readonly IList<SimplePlaylist> RawPlaylists;
        public BitmapImage ProfilePicture;
        public string Name;
        public string Email;
        public string Country;
        public List<Playlist> Playlists;

        public User(PrivateUser profile, IList<SimplePlaylist> playlists)
        {
            RawProfile = profile;
            RawPlaylists = playlists;
            Init();
        }

        private void Init()
        {
            GetProfilePicture();
            Playlists = new List<SimplePlaylist>();
            GetPlaylists();
            Name = RawProfile.DisplayName;
            Email = RawProfile.Email;
            Country = RawProfile.Country;

        }

        private void GetProfilePicture()
        {
            ProfilePicture = new BitmapImage();
            ProfilePicture.BeginInit();
            ProfilePicture.UriSource = new Uri(RawProfile.Images[0].Url);
            ProfilePicture.EndInit();
        }

        private void GetPlaylists()
        {
            foreach (var item in RawPlaylists)
            {
                Playlist temp = new Playlist(item);
                Playlists.Add(temp);
            }
        }
    }
}
