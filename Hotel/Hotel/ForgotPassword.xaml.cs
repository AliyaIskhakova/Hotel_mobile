using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;
using System.Reflection;

namespace Hotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPassword : ContentPage
    {
        int userId = 0;
        int newPassword;
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private async void NextBtn_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Email.Text))
            {
                string sql = $"SELECT ClientID FROM Client WHERE Email='{Email.Text}'";
                MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    userId  = (int)reader[0];
                    EmailEntry.IsVisible = false;
                    CodeEntry.IsVisible = true;
                    MailAddress from = new MailAddress("aliya_iskhakova12@mail.ru", "Hotel");
                    MailAddress to = new MailAddress(Email.Text);
                    MailMessage m = new MailMessage(from, to); 
                    Random rnd = new Random();
                    newPassword = rnd.Next(1000, 9999);
                    m.Subject = "Восстановление пароля";               
                    m.Body = "<h1>Код для восстановлеия пароля: " + newPassword + "</h1>";
                    m.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                    smtp.Credentials = new NetworkCredential("aliya_iskhakova12@mail.ru", "9GUmsMKvG87QcwdJgdMB");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                }
                else await DisplayAlert("Ошибка", "Пользователя с такой почтой не существует", "OK");
                reader.Close();
            }
            else await DisplayAlert("Ошибка", "Введите почту!","ОК");
        }

        private async void NextBtn2_Clicked(object sender, EventArgs e)
        {
            if (Code.Text == newPassword.ToString())
            {
                CodeEntry.IsVisible = false;
                SavePassword.IsVisible = true;
            }
            else await DisplayAlert("Ошибка", "Неверный код!", "ОК");
        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(Password.Text))
            {
                string sql = $"UPDATE Client SET Password = '{Password.Text}' WHERE (ClientID = {userId}) ";
                MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
                MySqlDataReader reader = command.ExecuteReader();
                reader.Close();
                await Navigation.PopAsync();
            }
            else await DisplayAlert("Ошибка", "Введите пароль!", "ОК");
        }
    }
}