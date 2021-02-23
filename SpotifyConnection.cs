using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpotifyApp
{
    class SpotifyConnection
    {
        private static SpotifyClient spotifyClient;
        private static EmbedIOAuthServer _server;
        public FullTrack track;

        private static CancellationTokenSource tokenSource = new CancellationTokenSource();
        private CancellationToken ct = tokenSource.Token;
        private bool IsRunning;

        public SpotifyConnection()
        {

        }

        public async Task Init()
        {
            await StartClient();
            while (IsRunning != true)
            {
                await Task.Run(() => { Task.Delay(10000); }, ct);
            }
        }

         private async Task StartClient()
        {
            // Make sure "http://localhost:5000/callback" is in your spotify application as redirect uri!
            _server = new EmbedIOAuthServer(new Uri("http://localhost:5000/callback"), 5000);
            await _server.Start();

            _server.AuthorizationCodeReceived += OnAuthorizationCodeReceived;

            var request = new LoginRequest(_server.BaseUri, "18dbc5b5f8f541668f4c66cc8788a7b4", LoginRequest.ResponseType.Code)
            {
                Scope = new List<string> { Scopes.UgcImageUpload, Scopes.UserReadRecentlyPlayed, Scopes.UserTopRead, Scopes.UserReadPlaybackPosition, Scopes.UserReadPlaybackState, Scopes.UserModifyPlaybackState, Scopes.UserReadCurrentlyPlaying, Scopes.AppRemoteControl, Scopes.Streaming, Scopes.PlaylistModifyPublic, Scopes.PlaylistModifyPrivate, Scopes.PlaylistReadPrivate, Scopes.PlaylistReadCollaborative, Scopes.UserFollowModify, Scopes.UserFollowRead, Scopes.UserLibraryModify, Scopes.UserLibraryRead, Scopes.UserReadEmail, Scopes.UserReadPrivate  }
            };
            BrowserUtil.Open(request.ToUri());
        }

        async Task OnAuthorizationCodeReceived(object sender, AuthorizationCodeResponse response)
        {
            await _server.Stop();

            var config = SpotifyClientConfig.CreateDefault();
            var tokenResponse = await new OAuthClient(config).RequestToken(
              new AuthorizationCodeTokenRequest(
                "18dbc5b5f8f541668f4c66cc8788a7b4", "8376387e01cd4acb8672bf416611d871", response.Code, new Uri("http://localhost:5000/callback")
              )
            );
            spotifyClient = new SpotifyClient(tokenResponse.AccessToken);
            IsRunning = true;
        }

        public async Task<PrivateUser> GetCurrentUser()
        {
            return await spotifyClient.UserProfile.Current();
        }

        public async Task<IList<SimplePlaylist>> GetUserPlaylists()
        {
            var page = await spotifyClient.Playlists.CurrentUsers();
            return await spotifyClient.PaginateAll<SimplePlaylist>(page);
            
        }







    }
}
