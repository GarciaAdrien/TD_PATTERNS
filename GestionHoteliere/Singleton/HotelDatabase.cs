using GestionHoteliere.Adapter;
using System;
using System.Collections.Generic;

namespace GestionHoteliere.Singleton
{
    public class HotelDatabase
    {
        private static HotelDatabase _instance;
        private List<HotelRoom> _rooms;

        private HotelDatabase()
        {
            _rooms = new List<HotelRoom>();
        }

        public static HotelDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HotelDatabase();
                }
                return _instance;
            }
        }

        public void AddRoom(HotelRoom room)
        {
            _rooms.Add(room);
        }

        public List<HotelRoom> GetAllRooms()
        {
            return _rooms;
        }
    }
}
