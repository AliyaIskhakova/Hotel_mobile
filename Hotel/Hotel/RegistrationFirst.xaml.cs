using MySqlConnector;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationFirst : ContentPage
    {
        public RegistrationFirst()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(login.Text) && !string.IsNullOrWhiteSpace(password.Text))
                {
                    Validate validate = new Validate();
                    if (validate.ValidateAll(login.Text, password.Text))
                    {
                        string sql = $"SELECT ClientID FROM Client WHERE Login='{login.Text}'";
                        MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows == false)
                        {
                            RegistrationSecond registrationSecond = new RegistrationSecond(login.Text, password.Text);
                            await Navigation.PushAsync(registrationSecond);
                        }
                        else await DisplayAlert("Регистрация", "Пользователь с таким логином уже существует", "OK");
                        reader.Close();
                    }
                    else await DisplayAlert("Ошибка", $"{validate.message}", "Ok");
                }
                else await DisplayAlert("Ошибка", "Введите все данные!", "OK");
            }
            catch
            {
                await DisplayAlert("Ошибка", "Что-то пошло не так, попробуйте еще раз", "OK");
            }
        }
    }
}