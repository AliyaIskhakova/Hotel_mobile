using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        int _UserId;
        public Profile()
        {
            InitializeComponent();
            _UserId = Preferences.Get("UserId", 0);
            ((App)Application.Current).connection.Close();
            ((App)Application.Current).connection.Open();
            string sql = $"SELECT * FROM Client WHERE ClientID = {_UserId} ";
            MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Surname.Text = (string)reader[1];
                Name.Text = (string)reader[2];
                Patronymic.Text = (string)reader[3];
                Birthday.Date = (DateTime)reader[4];
                if ((bool)reader[5]==true)
                {
                    women.IsChecked = true;
                }
                else men.IsChecked = true;
                Telephone.Text = (string)reader[6];
                Email.Text = (string)reader[7];
                Seria.Text = ((int)reader[8]).ToString();
                NumberPas.Text = ((int)reader[9]).ToString();
            }
            reader.Close();

        }

        private async void ExitBtn_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            App.Current.MainPage = new NavigationPage(new Autorization());
            await Navigation.PushAsync(new Autorization());
        }

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            bool gender;
            if( women.IsChecked == true )
            {
                gender = true;
            }
            else gender = false;
            string sql = $"UPDATE Client SET Surname = '{Surname.Text}', Name = '{Name.Text}', Patronymic = '{Patronymic.Text}', Birthday = '{Birthday.Date.ToString("yyyy-MM-dd")}'," +
                $"Gender = {gender}, PhoneNumber = '{Telephone.Text}', Email = '{Email.Text}', PassportSeries = {Seria.Text}, PassportNumber = {NumberPas.Text} WHERE (ClientID = {_UserId}) ";
            MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();

        }
    }
}