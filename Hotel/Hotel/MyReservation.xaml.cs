using MySqlConnector;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyReservation : ContentPage
    {
        public MyReservation()
        {
            InitializeComponent();
            ((App)Application.Current).connection.Close();
            ((App)Application.Current).connection.Open();
            GenerateActual();
            GenerateLast();
        }
        public async void GenerateActual()
        {
            try
            {
                List<ReservationInfo> Reservations = new List<ReservationInfo>();
                string sql = $"SELECT R.ReservationID, R.ReservationDate, R.CheckiInDate, R.CheckOutDate, R.FullCost, R.ClientID, R.RoomID, RT.TariffID FROM Reservation R " +
                    $"LEFT JOIN ReservationTariff RT ON R.ReservationID = RT.ReservationID " +
                    $"WHERE R.ClientID = {Preferences.Get("UserId", 0)} AND R.CheckiInDate > CURDATE(); ";
                MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ReservationInfo item = new ReservationInfo();
                    string img = $"room{reader[0]}.jpg";
                    item.ReservationID = (int)reader[0];
                    DateTime date = (DateTime)reader[1];
                    item.ReservationDate = date.ToString("d");
                    date = (DateTime)reader[2];
                    item.CheckIn = date;
                    item.CheckInDate = date.ToString("d");
                    date = (DateTime)reader[3];
                    item.CheckOut = date;
                    item.CheckOutDate = date.ToString("d");
                    item.FullCost = (int)reader[4];
                    item.ClientID = (int)reader[5];
                    item.RoomID = (int)reader[6];
                    item.Img = $"room{item.RoomID}.jpg";
                    try
                    {
                        item.TariffID = (int)reader[7];
                    }
                    catch { }
                    Reservations.Add(item);
                }
                actualReservList.ItemsSource = Reservations;
                reader.Close();
            }
            catch
            {
                await DisplayAlert("Ошибка", "Что-то пошло не так, попробуйте еще раз", "OK");
            }
        }
        public async void GenerateLast()
        {
            try
            {
                List<ReservationInfo> Reservations = new List<ReservationInfo>();
                string sql = $"SELECT R.ReservationID, R.ReservationDate, R.CheckiInDate, R.CheckOutDate, R.FullCost, R.ClientID, R.RoomID, RT.TariffID FROM Reservation R " +
                    $"LEFT JOIN ReservationTariff RT ON R.ReservationID = RT.ReservationID " +
                    $"WHERE R.ClientID = {Preferences.Get("UserId", 0)} AND R.CheckiInDate <= CURDATE(); ";
                MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ReservationInfo item = new ReservationInfo();
                    string img = $"room{reader[0]}.jpg";
                    item.ReservationID = (int)reader[0];
                    DateTime date = (DateTime)reader[1];
                    item.ReservationDate = date.ToString("d");
                    date = (DateTime)reader[2];
                    item.CheckIn = date;
                    item.CheckInDate = date.ToString("d");
                    date = (DateTime)reader[3];
                    item.CheckOut = date;
                    item.CheckOutDate = date.ToString("d");
                    item.FullCost = (int)reader[4];
                    item.ClientID = (int)reader[5];
                    item.RoomID = (int)reader[6];
                    item.Img = $"room{item.RoomID}.jpg";
                    try
                    {
                        item.TariffID = (int)reader[7];
                    }
                    catch { }
                    Reservations.Add(item);
                }
                lastReservList.ItemsSource = Reservations;
                reader.Close();
            }
            catch
            {
                await DisplayAlert("Ошибка", "Что-то пошло не так, попробуйте еще раз", "OK");
            }
        }

        private void LastReservBtn_Clicked(object sender, EventArgs e)
        {
            lastReserv.IsVisible = true;
            actualReserv.IsVisible= false;
            LastReservBtn.BackgroundColor = Color.FromRgb(144, 123, 122);
            ActualReservBtn.BackgroundColor = Color.FromRgb(33, 14, 13);
        }

        private void ActualReservBtn_Clicked(object sender, EventArgs e)
        {
            lastReserv.IsVisible = false;
            actualReserv.IsVisible = true;
            ActualReservBtn.BackgroundColor = Color.FromRgb(144, 123, 122);
            LastReservBtn.BackgroundColor = Color.FromRgb(33, 14, 13);
        }

        private async void actualReservList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                ReservationInfo reservationInfo = e.Item as ReservationInfo;
                MyReservationInfo myReservationInfo = new MyReservationInfo(reservationInfo, 0);
                await Navigation.PushAsync(myReservationInfo);
            }
            catch
            {
                await DisplayAlert("Ошибка", "Что-то пошло не так, попробуйте еще раз", "OK");
            }
        }

        private async void lastReservList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                ReservationInfo reservationInfo = e.Item as ReservationInfo;
                MyReservationInfo myReservationInfo = new MyReservationInfo(reservationInfo, -1);
                await Navigation.PushAsync(myReservationInfo);
            }
            catch
            {
                await DisplayAlert("Ошибка", "Что-то пошло не так, попробуйте еще раз", "OK");
            }
        }

        private void SwipeGestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            GenerateActual();
            GenerateLast();
        }
    }
}