using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class RoomImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
