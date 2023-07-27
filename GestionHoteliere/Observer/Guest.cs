using System;

namespace GestionHoteliere.Observer
{
    public class Guest
    {
        public string Name { get; private set; }

        public Guest(string name)
        {
            Name = name;
        }

        public void Update(string message)
        {
            Console.WriteLine($"{Name} received a notification: {message}");
        }
    }
}
