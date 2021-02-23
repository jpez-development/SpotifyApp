using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpotifyApp
{
    public partial class MainWindow
    {
        private readonly SpotifyConnection SpotifyClient;
        private User CurrentUser;

        public MainWindow()
        {
            InitializeComponent();
            SpotifyClient = new SpotifyConnection();
        }

        private async void Startup(object sender, RoutedEventArgs e)
        {
            await SpotifyClient.Init();
            CurrentUser = new User(await SpotifyClient.GetCurrentUser(), await SpotifyClient.GetUserPlaylists());
            FillListBox();
            UserStartup();
            
        }

        private void UserStartup()
        {
            Profile_Image.Source = CurrentUser.ProfilePicture;
            Profile_Name.Text = CurrentUser.Name;
            Profile_Email.Text = CurrentUser.Email;
            Profile_Country.Text = CurrentUser.Country;
        }

        private void FillListBox()
        {
            foreach (var item in CurrentUser.Playlists)
            {
                Playlist_List.Items.Add(item.Name);
            }
        }

        private void Playlist_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in CurrentUser.Playlists)
            {
                if(item.Name == Playlist_List.SelectedItem.ToString())
                {
                    Playlist_Name.Text = item.Name;
                    Playlist_Description.Text = item.Description;
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.UriSource = new Uri(item.Images[0].Url);
                    bmp.EndInit();
                    Playlist_Image.Source = bmp;
                }
            }
        }
    }
}
