using MySqlConnector;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationSecond : ContentPage
    {
        string _login;
        string _password;
        public RegistrationSecond(string login, string password)
        {
            InitializeComponent();
            _login = login;
            _password = password;
            Birthday.MaximumDate = DateTime.Now.AddYears(-14);
            Birthday.MinimumDate = DateTime.Today.AddYears(-100);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
           try
           {
                if (_login != null && _password != null && !string.IsNullOrEmpty(Surname.Text.Trim()) && !string.IsNullOrEmpty(Name.Text.Trim())
                && !string.IsNullOrEmpty(Telephone.Text.Trim()) && !string.IsNullOrEmpty(Email.Text.Trim()) && Birthday.Date != null && Telephone.Text.Length==17
                && !string.IsNullOrEmpty(Seria.Text.Trim()) && !string.IsNullOrEmpty(NumberPas.Text.Trim()))
                {
                    int gender = 0;
                    if (women.IsChecked == true)
                    {
                        gender = 1;
                    }
                    string numbersOnly = Regex.Replace(Telephone.Text, "[^0-9#]", "");
                    try {
                        string sql = $"INSERT Client (Surname, Name, Patronymic, Birthday, Gender, PhoneNumber, PassportSeries, PasswordNumber, Email, Login, Password )" +
                        $" VALUES('{Surname.Text}', '{Name.Text}', '{Patronymic.Text}', '{Birthday.Date.ToString("yyyy-MM-dd")}', '{gender}', {Seria.Text} , {NumberPas.Text} , '{numbersOnly}', '{Email.Text}', '{_login}', '{_password}');";
                        MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
                        MySqlDataReader reader = command.ExecuteReader();
                        await Navigation.PopToRootAsync();
                        reader.Close();
                    } 
                    catch {
                        await DisplayAlert("Ошибка", "Что-то пошло не так, попробуйье снова!", "Ok");
                    }
                }
                else await DisplayAlert("Ошибки", "Заполните все обязательные поля!", "Ok");
           }
           catch {
              await DisplayAlert("Ошибка", "Заполните все обязательные поля!", "Ok");
           }
        }

    }
}