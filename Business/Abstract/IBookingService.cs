using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBookingService : IGenericService<Booking>
    {
        public List<Booking> TBookingList();
        public List<Booking> TLatestBookingsOfTheHotel(int id);
        public int TPendingReservationCount(int id);
        public int TAcceptedReservationCount(int id);
        public int TCancelledReservationCount(int id);
        public int TCompletedReservationCount(int id);
    }
}
