using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZstdSharp.Unsafe;

namespace Hotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomsSearch : ContentPage
    {
        public RoomsSearch()
        {
            InitializeComponent();
            CheckIn.MinimumDate = DateTime.Now.AddDays(1);
            CheckIn.MaximumDate = DateTime.Now.AddYears(1);
            CheckOut.MinimumDate = DateTime.Now.AddDays(2);
            CheckOut.MaximumDate = DateTime.Now.AddYears(2).AddDays(1);
            ((App)Application.Current).connection.Close();
            ((App)Application.Current).connection.Open();
        }
        private async void SearchBtn_Clicked(object sender, EventArgs e)
        {
            if (CheckIn.Date != null && CheckOut.Date != null && CheckIn.Date < CheckOut.Date && int.TryParse(PeopleCount.Text, out int peopleC) && peopleC >= 1)
            {
                RoomsList roomsList = new RoomsList(CheckIn.Date, CheckOut.Date, peopleC);
                await Navigation.PushAsync(roomsList);
            }
            else await DisplayAlert("Ошибка", "Введите ве данные!", "Оk");
        }
    }
}