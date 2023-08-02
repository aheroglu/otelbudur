using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class HotelOwnerRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string HotelName { get; set; }
        public DateTime Date { get; set; }
        public string RequestStatus { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
