using MySqlConnector;
using System;
using System.Text.RegularExpressions;
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
                if (!string.IsNullOrWhiteSpace(Surname.Text) && !string.IsNullOrWhiteSpace(Name.Text)
                && !string.IsNullOrWhiteSpace(Telephone.Text) && !string.IsNullOrWhiteSpace(Email.Text) && !string.IsNullOrEmpty(Seria.Text) && !string.IsNullOrWhiteSpace(NumberPas.Text))
                {
                    string ptr;
                    if (string.IsNullOrWhiteSpace(Patronymic.Text))
                    {
                        ptr = "";
                    }
                    else ptr = Patronymic.Text;
                    Validate validate = new Validate(); 
                    if(validate.ValidateAll(Surname.Text, Name.Text, ptr, Telephone.Text, Email.Text, Seria.Text, NumberPas.Text) == true)
                    {
                        int gender = 0;
                        if (women.IsChecked == true)
                        {
                            gender = 1;
                        }
                        string numbersOnly = Regex.Replace(Telephone.Text, "[^0-9#]", "");
                    try
                    {
                        string sql = $"INSERT Client (Surname, Name, Patronymic, Birthday, Gender, PhoneNumber, PassportSeries, PassportNumber, Email, Login, Password )" +
                                $" VALUES('{Surname.Text}', '{Name.Text}', '{Patronymic.Text}', '{Birthday.Date.ToString("yyyy-MM-dd")}', '{gender}', '{numbersOnly}', {Seria.Text} , {NumberPas.Text} , '{Email.Text}', '{_login}', '{_password}');";
                                MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
                                MySqlDataReader reader = command.ExecuteReader();
                                await Navigation.PopToRootAsync();
                                reader.Close();
                    }
                    catch
                    {
                        await DisplayAlert("Ошибка", "Что-то пошло не так, попробуйте снова!", "Ok");
                    }

                } 
                    else await DisplayAlert("Ошибка", $"{validate.message}", "Ok");

                }
                else await DisplayAlert("Ошибки", "Заполните все обязательные поля!", "Ok");
       }
           catch {
              await DisplayAlert("Ошибка", "Что-то пошло не так, попробуйте снова!", "Ok");
    }
}

    }
}