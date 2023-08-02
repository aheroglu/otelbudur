using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class RoomRating
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public bool Status { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }
    }
}
