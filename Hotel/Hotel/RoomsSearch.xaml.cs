using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            try
            {
                if (CheckIn.Date != null && CheckOut.Date != null && !string.IsNullOrWhiteSpace(PeopleCount.Text))
                {
                    if (CheckIn.Date < CheckOut.Date)
                    {
                        if (int.TryParse(PeopleCount.Text, out int peopleC) && peopleC >= 1)
                        {
                            RoomsList roomsList = new RoomsList(CheckIn.Date, CheckOut.Date, peopleC);
                            await Navigation.PushAsync(roomsList);
                        }
                        else await DisplayAlert("Ошибка", "Некорректные данные о количестве гостей!", "Оk");
                    }
                    else await DisplayAlert("Ошибка", "Некорректные даты!", "Оk");
                }
                else await DisplayAlert("Ошибка", "Введите все данные!", "Оk");
            }
            catch
            {
                await DisplayAlert("Ошибка", "Что-то пошло не так, попробуйте еще раз", "OK");
            }
        }
    }
}