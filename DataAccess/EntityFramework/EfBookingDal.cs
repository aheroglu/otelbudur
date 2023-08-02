using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Repository;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public class EfBookingDal : GenericRepository<Booking>, IBookingDal
    {
        public int AcceptedReservationCount(int id)
        {
            using (var context = new Context())
            {
                return context.Bookings.Where(x => x.Room.HotelId == id && x.ReservationStatus == "Accepted").Count();
            }
        }

        public List<Booking> BookingList()
        {
            using (var context = new Context())
            {
                return context.Bookings.Include(x => x.Room.Hotel).Include(x => x.User).OrderByDescending(x => x.Id).ToList();
            }
        }

        public int CancelledReservationCount(int id)
        {
            using (var context = new Context())
            {
                return context.Bookings.Where(x => x.Room.HotelId == id && x.ReservationStatus == "Cancelled").Count();
            }
        }

        public int CompletedReservationCount(int id)
        {
            using (var context = new Context())
            {
                return context.Bookings.Where(x => x.Room.HotelId == id && x.ReservationStatus == "Completed").Count();
            }
        }

        public List<Booking> LatestBookingsOfTheHotel(int id)
        {
            using (var context = new Context())
            {
                return context.Bookings.Include(x => x.Room.Hotel).Include(x => x.User).Where(x => x.Room.HotelId == id).OrderByDescending(x => x.Id).ToList();
            }
        }

        public int PendingReservationCount(int id)
        {
            using (var context = new Context())
            {
                return context.Bookings.Where(x => x.Room.HotelId == id && x.ReservationStatus == "Pending").Count();
            }
        }
    }
}
