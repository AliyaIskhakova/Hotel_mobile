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
    public partial class ConfirmReservation : ContentPage
    {
        public ConfirmReservation(DateTime checkIn, DateTime checkOut, int peopleCount, RoomInfo roomInfo, TariffInfo tariffInfo)
        {
            InitializeComponent();
        }

        private void SaveReservationBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}