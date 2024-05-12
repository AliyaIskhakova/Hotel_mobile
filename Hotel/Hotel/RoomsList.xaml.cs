using MySqlConnector;
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
    public partial class RoomsList : ContentPage
    {
        DateTime _checkIn;
        DateTime _checkOut;
        int _peopleCount;
        public RoomsList(DateTime checkIn, DateTime checkOut, int peopleCount )
        {
            InitializeComponent();
            ((App)Application.Current).connection.Close();
            ((App)Application.Current).connection.Open();
            _checkIn = checkIn;
            _checkOut = checkOut;   
            _peopleCount = peopleCount;
            GenerateRooms(checkIn, checkOut, peopleCount);
            
         }
        public async void GenerateRooms(DateTime checkIn, DateTime checkOut, int peopleCount)
        {          
            List<RoomInfo> Rooms = new List<RoomInfo>();
            string sql = $"SELECT * FROM Room r WHERE r.PeopleQuantity >= {peopleCount} AND NOT EXISTS " +
                $"(SELECT 1 FROM Reservation b WHERE b.RoomID = r.RoomID AND '{checkIn.ToString("yyyy-MM-dd")}' <= b.CheckOutDate " +
                $"AND '{checkOut.ToString("yyyy-MM-dd")}' >= b.CheckiInDate)";
            MySqlCommand command = new MySqlCommand(sql, ((App)Application.Current).connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    RoomInfo item = new RoomInfo();
                    string img = $"room{reader[0]}.jpg";
                    item.RoomID = (int)reader[0];
                    item.Img = img;
                    item.Summary = (string)reader[1];
                    item.Area = (float)reader[2];
                    item.RoomQuantity = (int)reader[3];
                    item.PeopleQuantity = (int)reader[4];
                    item.Cost = (int)reader[5];
                    Rooms.Add(item);
                }
                roomsList.ItemsSource = Rooms;
                reader.Close();
            }
            else
            {
                await DisplayAlert("Поиск", "Номеров на эти даты нет", "Оk");
                await Navigation.PopToRootAsync();
                reader.Close();
            }
            
        }
        private async void SelectBtn_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            ViewCell viewCell = button.Parent.Parent.Parent.Parent.Parent.Parent.Parent as ViewCell;
            RoomInfo room = (RoomInfo)viewCell.BindingContext;
            SelectedRoomInfo selectedRoomInfo = new SelectedRoomInfo(_checkIn, _checkOut, _peopleCount, room);
            await Navigation.PushAsync(selectedRoomInfo);
        }
    }
}