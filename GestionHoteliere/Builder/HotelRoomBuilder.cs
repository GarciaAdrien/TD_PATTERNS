using GestionHoteliere.Adapter;
using System;

namespace GestionHoteliere.Builder
{
    public class HotelRoomBuilder
    {
        private string RoomType { get; set; }
        private int RoomNumber { get; set; }
        private bool HasWifi { get; set; }
        private double TaxRate { get; set; }

        public HotelRoomBuilder SetRoomType(string roomType)
        {
            RoomType = roomType;
            return this;
        }

        public HotelRoomBuilder SetRoomNumber(int roomNumber)
        {
            RoomNumber = roomNumber;
            return this;
        }

        public HotelRoomBuilder SetHasWifi(bool hasWifi)
        {
            HasWifi = hasWifi;
            return this;
        }

        public HotelRoomBuilder SetTaxRate(double taxRate)
        {
            TaxRate = taxRate;
            return this;
        }

        public HotelRoom Build(string RoomType,int RoomNumber)
        {
            // Use the builder's internal state to construct the HotelRoom object
            var room = new HotelRoom(RoomType, RoomNumber);
            room.HasWifi = HasWifi;
            room.TaxRate = TaxRate;
            return room;
        }
    }
}
