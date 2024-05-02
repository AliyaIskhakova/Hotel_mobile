using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;
using Button = Xamarin.Forms.Button;

namespace Hotel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectedRoomInfo : ContentPage
    {
        DateTime _checkIn;
        DateTime _checkOut;
        int _peopleCount;
        RoomInfo _roomInfo;

        public SelectedRoomInfo(DateTime checkIn, DateTime checkOut, int peopleCount, RoomInfo roomInfo)
        {
            InitializeComponent();
            ((App)Application.Current).connection.Close();
            ((App)Application.Current).connection.Open();
            _checkIn = checkIn;
            _checkOut = checkOut;
            _peopleCount = peopleCount;
            _roomInfo = roomInfo;
            RoomImg.Source = $"room{roomInfo.RoomID}.jpg";
            RoomName.Text = $"Номер {roomInfo.RoomID}";
            RoomSummary.Text = roomInfo.Summary;
            Area.Text = $"• {roomInfo.Area} м^2";
            RoomQuantity.Text = $"• {roomInfo.RoomQuantity} комн.";
            PeopleQuantity.Text = $"• до {roomInfo.PeopleQuantity} мест";
            List<TariffInfo> Tariffs = new List<TariffInfo>();
            string sql = "SELECT * FROM Tariff";
            MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TariffInfo item = new TariffInfo();
                item.TariffID = (int)reader[0];
                item.Name = (string)reader[1];
               if ((bool)reader[3])
              {
                  item.Food = "Питание";
              }
                if ((bool)reader[4])
                {
                    item.Gym = "Спорт";
                }
                if ((bool)reader[5])
                {
                    item.Transfer = "Трансфер";
                }
                if ((bool)reader[6])
                {
                    item.Wifi = "Wi-Fi";
                }
                item.Cost = (int)reader[7] + roomInfo.Cost;
                Tariffs.Add(item);
            }
            tariffList.ItemsSource = Tariffs;
            reader.Close();
        }

        private async void SelectTarifBtn_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            TariffInfo tariff = (TariffInfo)btn.BindingContext;
            if (tariff != null)
            {
                ConfirmReservation confirmReservation = new ConfirmReservation(_checkIn,  _checkOut, _peopleCount, _roomInfo, tariff);
                await Navigation.PushAsync(confirmReservation);
            }
        }
    }
}