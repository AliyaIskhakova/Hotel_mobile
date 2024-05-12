using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel
{
    public class ReservationInfo
    {
        public int ReservationID { get; set; }
        public string ReservationDate { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int FullCost { get; set; }
        public int ClientID { get; set; }
        public int RoomID { get; set; }
        public string Img { get; set; }
        public int TariffID { get; set; }

    }
}
