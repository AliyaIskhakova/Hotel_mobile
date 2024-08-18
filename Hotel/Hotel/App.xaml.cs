using MySqlConnector;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hotel
{
    public partial class App : Application
    {
        public MySqlConnection connection = new MySqlConnection(@"Server=192.168.74.234;Port=3306;Uid=aliya;Pwd=12345;Database=hotel;");
        public MySqlCommand command;
        public MySqlDataReader datareader;
        public App()
        {
            InitializeComponent();
            connection.Close();
            connection.Open();
            if (Preferences.Get("IsLoggedIn", false))
            {
                MainPage = new NavigationPage(new Menu());
            }
            else
            {
                MainPage = new NavigationPage(new Autorization());
            }
        }

        protected override void OnStart()
        {
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
    }
}
