using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHoteliere.Observer
{
    using System;
    using System.Collections.Generic;

    namespace GestionHoteliere.Observer
    {
        internal class GuestHotel 
        {
            private List<Guest> _guests = new List<Guest>();

            public void RegisterGuest(Guest guest)
            {
                _guests.Add(guest);
            }

            public void UnregisterGuest(Guest guest)
            {
                _guests.Remove(guest);
            }

            public void NotifyGuests(string message)
            {
                foreach (var guest in _guests)
                {
                    guest.Update(message);
                }
            }
        }
    }

}
