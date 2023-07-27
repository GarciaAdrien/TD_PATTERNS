using System;

namespace GestionHoteliere.Adapter
{
    public class HotelRoom
    {
        public string RoomType { get; private set; }
        public double BasePrice { get; private set; }
        public int Capacity { get; private set; }
        public bool HasWifi { get;  set; }
        public double TaxRate { get;  set; }
        public int RoomNumber { get; private set; }

        public HotelRoom(string roomType, int roomNumber)
        {
            RoomType = roomType;
            RoomNumber = roomNumber;

            //en fonction du nombre de clients le prix change
            switch (roomType.ToLower())
            {
                case "1":
                    BasePrice = 80;
                    Capacity = 1;
                    break;
                case "2":
                    BasePrice = 100;
                    Capacity = 2;
                    break;
                case "3":
                    BasePrice = 150;
                    Capacity = 3;
                    break;
                default:
                    throw new ArgumentException("Invalid room type.");
            }


            HasWifi = false;

        }

        public double CalculateTotalPrice(int numberOfPeople, bool hasWifi, double taxRate)
        {
            double totalPrice = BasePrice ;

            if (hasWifi)
            {
                totalPrice += 5; 
            }

            totalPrice *= (1 + taxRate / 100);

            return totalPrice;
        }

}

}
