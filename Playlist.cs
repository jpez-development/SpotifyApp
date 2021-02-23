using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApp
{
    class Playlist
    {
        public string ID;

        public Playlist(SimplePlaylist playlist)
        {
            ID = playlist.Id;
        }
    }
}
