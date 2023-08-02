using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBookingDal : IGenericDal<Booking>
    {
        public List<Booking> BookingList();
        public List<Booking> LatestBookingsOfTheHotel(int id);
        public int PendingReservationCount(int id);
        public int AcceptedReservationCount(int id);
        public int CancelledReservationCount(int id);
        public int CompletedReservationCount(int id);
    }
}
