using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class EmailsSent
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string SentFor { get; set; }
        public string Content { get; set; }
    }
}
