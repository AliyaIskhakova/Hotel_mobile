using MySqlConnector;
using System;
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
                if (!string.IsNullOrWhiteSpace(Login.Text) && !string.IsNullOrWhiteSpace(Password.Text))
                {
                    string sql = $"SELECT ClientID, Password FROM Client WHERE Login='{Login.Text}'";
                    MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        int userId = (int)reader[0];
                        string password = (string)reader[1];
                        if (password == Password.Text)
                        {
                            Preferences.Set("UserId", userId);
                            Preferences.Set("IsLoggedIn", true);
                            await Navigation.PushAsync(new Menu());
                            App.Current.MainPage = new NavigationPage(new Menu());
                        }
                        else await DisplayAlert("Ошибка", "Неверный пароль", "OK");
                    }
                    else await DisplayAlert("Ошибка", "Неверный логин", "OK");
                    reader.Close();
                }
                else await DisplayAlert("Ошибка", "Введите данные для входа!", "OK");
            }
            catch 
            {
                await DisplayAlert("Ошибка", "Что-то пошло не так, попробуйте еще раз", "OK");
            }
        }

        private async void ForgotPasswordBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPassword());
        }
    }
}
