using MySqlConnector;
using System;
using System.Text.RegularExpressions;
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
            Birthday.MaximumDate = DateTime.Now.AddYears(-14);
            Birthday.MinimumDate = DateTime.Today.AddYears(-100);
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
                try {
                    Seria.Text = ((int)reader[8]).ToString();
                    NumberPas.Text = ((int)reader[9]).ToString();
                }
                catch { 
                
                }
            }
            reader.Close();

        }

        private async void ExitBtn_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            App.Current.MainPage = new NavigationPage(new Autorization());
            ((App)Application.Current).connection.Close();
            ((App)Application.Current).connection.Open();
            await Navigation.PushAsync(new Autorization());

        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Surname.Text) && !string.IsNullOrWhiteSpace(Name.Text)
                    && !string.IsNullOrWhiteSpace(Telephone.Text) && !string.IsNullOrWhiteSpace(Email.Text) && Birthday.Date != null
                    && !string.IsNullOrEmpty(Seria.Text) && !string.IsNullOrWhiteSpace(NumberPas.Text))
                {
                    Validate validate = new Validate();
                    if (validate.ValidateAll(Surname.Text, Name.Text, Patronymic.Text, Telephone.Text, Email.Text, Seria.Text, NumberPas.Text) == true)
                    {
                        bool gender;
                        if (women.IsChecked == true)
                        {
                            gender = true;
                        }
                        else gender = false;
                        string telephone = Regex.Replace(Telephone.Text, "[^0-9#]", "");
                        string sql = $"UPDATE Client SET Surname = '{Surname.Text}', Name = '{Name.Text}', Patronymic = '{Patronymic.Text}', Birthday = '{Birthday.Date.ToString("yyyy-MM-dd")}'," +
                            $"Gender = {gender}, PhoneNumber = '{telephone}', Email = '{Email.Text}', PassportSeries = {Seria.Text}, PassportNumber = {NumberPas.Text} WHERE (ClientID = {_UserId}) ";
                        MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
                        MySqlDataReader reader = command.ExecuteReader();
                        reader.Close();
                        await DisplayAlert("Профиль", "Данные успешно сохранены", "Ok");
                    }
                    else await DisplayAlert("Ошибка", $"{validate.message}", "Ok");
                }
                else await DisplayAlert("Ошибки", "Заполните все обязательные поля!", "Ok");
            }
            catch
            {
                await DisplayAlert("Ошибка", "Что-то пошло не так, попробуйте еще раз", "OK");
            }
        }
    }
}