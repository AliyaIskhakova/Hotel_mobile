using MySqlConnector;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyReservationInfo : ContentPage
    {
        ReservationInfo _reservationInfo;
        int RoomCost;
        int TariffCost;
        public MyReservationInfo(ReservationInfo reservationInfo, int type)
        {
            InitializeComponent();
            MyReservInfo.BindingContext = reservationInfo;
            _reservationInfo = reservationInfo;

            if (type == -1)
            {
                DeleteReservationBtn.IsVisible = false;
                EditReservationBtn.IsVisible = false;
                checkIn.IsEnabled = false;
                checkOut.IsEnabled = false;
                checkIn.TextColor = Color.Black;
                checkOut.TextColor = Color.Black;     
            }
            checkIn.Date = reservationInfo.CheckIn;
            checkOut.Date = reservationInfo.CheckOut;
            checkIn.MinimumDate = reservationInfo.CheckIn;
            checkIn.MaximumDate = reservationInfo.CheckOut.AddDays(-1);
            checkOut.MinimumDate = reservationInfo.CheckIn.AddDays(1);
            checkOut.MaximumDate = reservationInfo.CheckOut;
            string sql = $"SELECT Area, RoomQuantity, PeopleQuantity, Cost FROM Room WHERE RoomID = '{_reservationInfo.RoomID}';";
            MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Area.Text = $"• {(float)reader[0]} м^2";
                RoomQuantity.Text = $"• {(int)reader[1]} комн.";
                PeopleQuantity.Text = $"• до {(int)reader[2]} мест";
                RoomCost = (int)reader[3];
            }
            reader.Close();
            sql = $"SELECT Name, Food, Gym, Transfer, Wifi, Cost FROM Tariff WHERE TariffID = '{_reservationInfo.TariffID}';";
            command = new MySqlCommand(sql, ((App)Application.Current).connection);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                TariffName.Text = (string)reader[0];
                if ((bool)reader[1])
                {
                    Food.Text = "• Питание";
                }
                else Food.Text = "• Без питания";
                if ((bool)reader[2])
                {
                    Gym.Text = "• Спортивный зал";
                }
                else Gym.Text = "• Без спорт зала";
                if ((bool)reader[3])
                {
                    Transfer.Text = "• Трансфер";
                }
                else Transfer.Text = "• Без трансфера";
                if ((bool)reader[4])
                {
                    Wifi.Text = "• Wi-Fi";
                }
                else Wifi.Text = "• Без интернета";
                TariffCost = (int)reader[5];
            }
            reader.Close();

            TimeSpan difference = (checkOut.Date).Subtract(checkIn.Date);
            int numberOfDays = difference.Days;
            ReservCost.Text = $"{(RoomCost + TariffCost) * numberOfDays}";
        }

        private async void DeleteReservationBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool result = await DisplayAlert("Подтвердить действие", "Вы действительно хотите отменить бронирование?", "Да", "Нет");
                if (result)
                {
                    string sql = $"DELETE FROM reservation WHERE ReservationID = '{_reservationInfo.ReservationID}';";
                    MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Close();
                    await Navigation.PushAsync(new Menu());
                    App.Current.MainPage = new NavigationPage(new Menu());
                }
            }
            catch
            {
                await DisplayAlert("Ошибка", "Что-то пошло не так, попробуйте еще раз", "OK");
            }
        }

        private void checkOut_DateSelected(object sender, DateChangedEventArgs e)
        {
            TimeSpan difference = (checkOut.Date).Subtract(checkIn.Date);
            int numberOfDays = difference.Days;
            ReservCost.Text = $"{(RoomCost+TariffCost)*numberOfDays}";
        }

        private void checkIn_DateSelected(object sender, DateChangedEventArgs e)
        {
            TimeSpan difference = (checkOut.Date).Subtract(checkIn.Date);
            int numberOfDays = difference.Days;
            ReservCost.Text = $"{(RoomCost + TariffCost) * numberOfDays}";
        }

        private async void EditReservationBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (checkOut.Date > checkIn.Date)
                {
                    string sql = $"UPDATE reservation SET CheckiInDate = '{checkIn.Date.ToString("yyyy-MM-dd")}', CheckOutDate = '{checkOut.Date.ToString("yyyy-MM-dd")}', " +
                        $"FullCost = '{ReservCost.Text}' WHERE(`ReservationID` = '{_reservationInfo.ReservationID}');";
                    MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Close();
                    await Navigation.PushAsync(new Menu());
                    App.Current.MainPage = new NavigationPage(new Menu());
                }
                else await DisplayAlert("Изменение дат", "Неверные даты!", "Ok");
            }
            catch
            {
                await DisplayAlert("Ошибка", "Что-то пошло не так, попробуйте еще раз", "OK");
            }
        }
    }
}