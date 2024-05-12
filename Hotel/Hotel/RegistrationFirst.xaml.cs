using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (!string.IsNullOrEmpty(login.Text.Trim()) && !string.IsNullOrEmpty(password.Text.Trim()))
            {
                RegistrationSecond registrationSecond = new RegistrationSecond(login.Text, password.Text);
                await Navigation.PushAsync(registrationSecond);
            }
            else await DisplayAlert("Ошибка", "Введите все данные!", "OK");
        }
    }
}