using GestionHoteliere.Adapter;
using System;
using System.Collections.Generic;

namespace GestionHoteliere.Composite
{
    public class Hotel
    {
        private List<HotelRoom> _hotelRooms = new List<HotelRoom>();

        public void AddRoom(HotelRoom room)
        {
            _hotelRooms.Add(room);
        }

        public void RemoveRoom(HotelRoom room)
        {
            _hotelRooms.Remove(room);
        }


    }
}
