using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hotel
{
    public partial class Autorization : ContentPage
    {
        
        public Autorization()
        {
            InitializeComponent();
            ((App)Application.Current).connection.Close();
            ((App)Application.Current).connection.Open();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationFirst());
        }

        private async void SignInBtn_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Login.Text.Trim()) && !string.IsNullOrEmpty(Login.Text.Trim()))
                {
                    string sql = $"SELECT ClientID FROM Client WHERE Client.Login={Login.Text} and Client.Password={Password.Text}";
                    MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        int userId = (int)reader[0];
                        Preferences.Set("UserId", userId);
                        Preferences.Set("IsLoggedIn", true);
                        await Navigation.PushAsync(new Menu());
                        App.Current.MainPage = new NavigationPage(new Menu());
                    }
                    else await DisplayAlert("Ошибка", "Неверный логин или пароль", "OK");
                    reader.Close();
                }
                else await DisplayAlert("Ошибка", "Введите данные для входа!", "OK");
            }
            catch 
            {
                await DisplayAlert("Ошибка", "Произошла ошибка! Попробуйте еще раз", "OK");
            }
        }
    }
}
