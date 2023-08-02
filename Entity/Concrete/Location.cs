using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

        public List<Hotel> Hotels { get; set; }
        public List<HotelOwnerRequest> HotelOwnerRequests { get; set; } 
    }
}
