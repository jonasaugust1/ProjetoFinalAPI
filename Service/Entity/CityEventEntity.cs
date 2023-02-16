using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Entity
{
    public class CityEventsEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateHourEvent { get; set; }
        public string Local { get; set; }
        public string Address { get; set; }
        public Decimal Price { get; set; }
    }
}