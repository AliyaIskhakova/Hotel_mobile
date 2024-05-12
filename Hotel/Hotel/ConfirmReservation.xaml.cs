using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace Hotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmReservation : ContentPage
    {
        DateTime _checkIn;
        DateTime _checkOut;
        int _RoomId;
        int _TariffId;
        int _Cost;
        public ConfirmReservation(DateTime checkIn, DateTime checkOut, int peopleCount, RoomInfo roomInfo, TariffInfo tariffInfo)
        {
            InitializeComponent();
            _checkIn = checkIn;
            _checkOut = checkOut;
            _RoomId = roomInfo.RoomID;
            _TariffId = tariffInfo.TariffID;
            _Cost = tariffInfo.Cost;
            TimeSpan difference = checkOut.Subtract(checkIn);
            int numberOfDays = difference.Days;
            checkInDate.Text = checkIn.ToString("dd-MM-yyyy");
            checkOutDate.Text = checkOut.ToString("dd-MM-yyyy");
            PeopleCount.Text = peopleCount.ToString();
            HightCount.Text = numberOfDays.ToString();
            RoomName.Text = $"Номер {roomInfo.RoomID}";
            Area.Text = $"• {roomInfo.Area} м^2";
            RoomQuantity.Text = $"• {roomInfo.RoomQuantity} комн.";
            PeopleQuantity.Text = $"• до {roomInfo.PeopleQuantity} мест";
            TariffName.Text = tariffInfo.Name.ToString();
            Food.Text = $"{tariffInfo.Food}";
            Gym.Text = $"{tariffInfo.Gym}";
            Transfer.Text = $"{tariffInfo.Transfer}";
            Wifi.Text = $"{tariffInfo.Wifi}";
            Cost.Text = tariffInfo.Cost.ToString();
        }

        private async void SaveReservationBtn_Clicked(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO reservation (ReservationDate, CheckiInDate, CheckOutDate, FullCost, ClientID, RoomID) " +
                $"VALUES ('{DateTime.Now.ToString("yyyy-MM-dd")}', '{_checkIn.ToString("yyyy-MM-dd")}', '{_checkOut.ToString("yyyy-MM-dd")}', '{_Cost}', '{Preferences.Get("UserId",0)}', '{_RoomId}');";
            MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
            sql = $"SELECT ReservationID FROM Reservation ORDER BY ReservationID DESC LIMIT 1;";
            command = new MySqlCommand(sql, ((App)Application.Current).connection);
            reader = command.ExecuteReader();
            reader.Read();
            int ReservationID = (int)reader[0];
            reader.Close();
            sql = $"INSERT INTO reservationtariff (`ReservationID`, `TariffID`) VALUES ('{ReservationID}', '{_TariffId}');";
            command = new MySqlCommand(sql, ((App)Application.Current).connection);
            reader = command.ExecuteReader();
            reader.Close();
            await Navigation.PushAsync(new Menu());
            App.Current.MainPage = new NavigationPage(new Menu());
        }
    }
}