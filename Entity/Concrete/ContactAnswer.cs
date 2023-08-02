using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class ContactAnswer
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public DateTime Date { get; set; }

        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
