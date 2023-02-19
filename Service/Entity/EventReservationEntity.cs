using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Entity
{
    public class EventReservationEntity
    {
        public long ReservationId { get; set; }
        public long EventId { get; set; }
        public string PersonName { get; set; }
        public int Quantity { get; set; }

    }
}