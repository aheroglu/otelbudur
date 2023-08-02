using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BookingManager : IBookingService
    {
        private readonly IBookingDal _bookingDal;

        public BookingManager(IBookingDal bookingDal)
        {
            _bookingDal = bookingDal;
        }

        public int TAcceptedReservationCount(int id)
        {
            return _bookingDal.AcceptedReservationCount(id);
        }

        public List<Booking> TBookingList()
        {
            return _bookingDal.BookingList();
        }

        public int TCancelledReservationCount(int id)
        {
            return _bookingDal.CancelledReservationCount(id);
        }

        public int TCompletedReservationCount(int id)
        {
            return _bookingDal.CompletedReservationCount(id);
        }

        public void TDelete(Booking t)
        {
            _bookingDal.Delete(t);
        }

        public Booking TGetById(int id)
        {
            return _bookingDal.GetById(id);
        }

        public List<Booking> TGetList()
        {
            return _bookingDal.GetList();
        }

        public void TInsert(Booking t)
        {
            _bookingDal.Insert(t);
        }

        public List<Booking> TLatestBookingsOfTheHotel(int id)
        {
            return _bookingDal.LatestBookingsOfTheHotel(id);
        }

        public int TPendingReservationCount(int id)
        {
            return _bookingDal.PendingReservationCount(id);
        }

        public void TUpdate(Booking t)
        {
            _bookingDal.Update(t);
        }
    }
}
