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
    public class RoomManager : IRoomService
    {
        private readonly IRoomDal _roomDal;

        public RoomManager(IRoomDal roomDal)
        {
            _roomDal = roomDal;
        }

        public void TDelete(Room t)
        {
            _roomDal.Delete(t);
        }

        public Room TGetById(int id)
        {
            return _roomDal.GetById(id);
        }

        public List<Room> TGetList()
        {
            return _roomDal.GetList();
        }

        public void TInsert(Room t)
        {
            _roomDal.Insert(t);
        }

        public List<Room> TRoomDetails(int id)
        {
            return _roomDal.RoomDetails(id);
        }

        public List<Room> TRoomsByHotel(int id)
        {
            return _roomDal.RoomsByHotel(id);
        }

        public void TUpdate(Room t)
        {
            _roomDal.Update(t);
        }
    }
}
