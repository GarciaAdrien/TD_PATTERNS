using GestionHoteliere.Adapter;
using GestionHoteliere.Builder;
using GestionHoteliere.Singleton;
using GestionHoteliere.Composite;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using GestionHoteliere.Observer;
using GestionHoteliere.Observer.GestionHoteliere.Observer;

namespace GestionHoteliereApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue dans le logiciel de gestion hôtelière simplifiée!");

            HotelDatabase hotelDatabase = HotelDatabase.Instance;
            var roomBuilder = new HotelRoomBuilder();
            // Demande le nombre de chambres à ajouter
            Console.Write("Combien de chambres souhaitez-vous ajouter ? ");
            int numberOfRooms = int.Parse(Console.ReadLine());

            for (int i = 1; i <= numberOfRooms; i++)
            {
                Console.WriteLine($"Chambre {i}:");
                Console.Write("Veuillez saisir le type de chambre (Simple = 1/Double = 2/Suite = 3) : ");
                string roomType = Console.ReadLine();
                int roomNumber = i;

                hotelDatabase.AddRoom(new HotelRoom(roomType, roomNumber));
            }

            // Demande le nombre de clients séjournant dans chaque chambre
            List<int> guestsPerRoom = new List<int>();

            for (int i = 1; i <= numberOfRooms; i++)
            {
                Console.Write($"Combien de clients séjournent dans la chambre {i} ? ");
                int guestsInRoom = int.Parse(Console.ReadLine());
                guestsPerRoom.Add(guestsInRoom);
            }

            // ...

            List<double> finalPrices = new List<double>();

            // Demande les détails pour chaque client
            for (int i = 1; i <= guestsPerRoom.Count; i++)
            {
                Console.WriteLine($"Chambre {i}:");
                int roomNumber = i; // Assuming the rooms are numbered from 1 to numberOfRooms

                // Utilisation du pattern Adapter pour obtenir la chambre en fonction du numéro saisi
                var roomAdapter = hotelDatabase.GetAllRooms().Find(r => r.RoomNumber == roomNumber);
                if (roomAdapter != null)
                {
                    List<string> guestNames = new List<string>();

                    for (int j = 1; j <= guestsPerRoom[i - 1]; j++)
                    {
                        Console.Write($"Nom du client {j} : ");
                        guestNames.Add(Console.ReadLine());
                    }

                    Console.Write($"Souhaitez-vous prendre le petit-déjeuner ? (Oui/Non) : ");
                    string breakfastChoice = Console.ReadLine();
                    bool hasBreakfast = breakfastChoice.Equals("Oui", StringComparison.OrdinalIgnoreCase);

                    Console.Write($"Souhaitez-vous le wifi ? (Oui/Non) : ");
                    string wifiChoice = Console.ReadLine();
                    bool hasWifi = wifiChoice.Equals("Oui", StringComparison.OrdinalIgnoreCase);

                    int numberOfPeople = guestsPerRoom[i - 1];

                    Console.Write($"Taxe (en %) : ");
                    double taxRate = double.Parse(Console.ReadLine());

                    double roomPrice = roomAdapter.CalculateTotalPrice(numberOfPeople, hasWifi, taxRate);
                    if (hasBreakfast)
                    {
                        roomPrice += 7.5 * numberOfPeople;
                    }

                    finalPrices.Add(roomPrice);

                    Console.WriteLine($"Chambre {roomNumber} - {roomAdapter.RoomType} :");
                    Console.WriteLine($"Noms des clients : {string.Join(", ", guestNames)}");
                    Console.WriteLine($"Nombre de personnes : {numberOfPeople}");
                    Console.WriteLine($"Prix de la chambre : {roomAdapter.CalculateTotalPrice(numberOfPeople, hasWifi, taxRate)} euros");
                    // Use the SetHasWifi and SetTaxRate methods to set the properties
                    roomBuilder.SetHasWifi(hasWifi);
                    roomBuilder.SetTaxRate(taxRate);
                    HotelRoom room = roomBuilder.Build(roomAdapter.RoomType, roomNumber);
                    hotelDatabase.AddRoom(room);
                    // Later in the code, you can check if Wi-Fi is included in the room:
                    if (room.HasWifi == true)
                    {
                        Console.WriteLine("WiFi inclus : 4.5€");
                    }
                    if (hasBreakfast == true) {
                        Console.WriteLine($"Petit-déjeuner inclus : 7.5€");
                            }
                    if (taxRate != 0) { 
                    Console.WriteLine($"Taxe : {taxRate}%");
                    }
                    Console.WriteLine($"Prix total pour les clients : {roomPrice} euros");
                }
                else
                {
                    Console.WriteLine($"Chambre {roomNumber} n'existe pas !");
                }
            }
            // Create a new instance of the HotelRoomBuilder
            HotelRoomBuilder builder = new HotelRoomBuilder();
            Hotel hotel = new Hotel();
            // Builder
            HotelRoom room101 = builder.SetRoomType("1").SetRoomNumber(101).SetHasWifi(true).SetTaxRate(10).Build("1", 101);
            HotelRoom room102 = builder.SetRoomType("2").SetRoomNumber(201).SetHasWifi(false).SetTaxRate(12).Build("2", 102);

            Console.WriteLine("Chambre 10 abonnement:");
            Console.WriteLine($"Type de chambre: {room101.RoomType}, Numéro de chambre: {room101.RoomNumber}, Prix Total: {room101.CalculateTotalPrice(1, room101.HasWifi, room101.TaxRate)}");
            hotel.AddRoom(room101);
            Console.WriteLine("Suppression reservation Chambre 101");
            hotel.RemoveRoom(room101);
            Console.WriteLine("Chambre 102 abonnement:");
            Console.WriteLine($"Type de chambre: {room102.RoomType}, Numéro de chambre: {room102.RoomNumber}, Prix Total: {room102.CalculateTotalPrice(2, room102.HasWifi, room102.TaxRate)}");
            hotel.AddRoom(room102);
            Console.WriteLine("Suppression reservation Chambre 102");
            hotel.RemoveRoom(room102);

            double grandTotal = 0;
            foreach (var price in finalPrices)
            {
                grandTotal += price;
            }

            Console.WriteLine($"Nombre de chambres louées : {finalPrices.Count}");
            Console.WriteLine($"Prix total pour tous les clients : {grandTotal} euros");
            Console.ReadLine();

            //Observer
            var guestHotel = new GuestHotel();
            var guest1 = new Guest("Alice");
            var guest2 = new Guest("Bob");

            // Enregistre les utilisateurs dans l'hotel
            guestHotel.RegisterGuest(guest1);
            guestHotel.RegisterGuest(guest2);

            // Signale une chambre dispo
            guestHotel.NotifyGuests("Nouvelle chambre disponible: Chambre 101");

            // Desenregistre l'utilisateur
            guestHotel.UnregisterGuest(guest2);

            // Signale une promo aux utilisateurs de l'hotel
            guestHotel.NotifyGuests("Offre spécial 20% de réduction!");

            Console.ReadLine();
        }

    }
}
