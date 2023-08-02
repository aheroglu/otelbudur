using System.Collections.Generic;
using System.Web.Mvc;

namespace Entity.Concrete
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Details { get; set; }
        public string CoverImage { get; set; }
        public bool Status { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }   

        public List<Room> Rooms { get; set; }
    }
}
