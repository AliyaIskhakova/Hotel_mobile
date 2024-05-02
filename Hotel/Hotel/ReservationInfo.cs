using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel
{
    public class ReservationInfo
    {
        public int ReservationID { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime CheckiInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int FullCost { get; set; }
        public int ClientID { get; set; }
        public int RoomID { get; set; }

    }
}
