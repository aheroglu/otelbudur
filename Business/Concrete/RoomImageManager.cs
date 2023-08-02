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
    public class RoomImageManager : IRoomImageService
    {
        private readonly IRoomImageDal _roomImageDal;

        public RoomImageManager(IRoomImageDal roomImageDal)
        {
            _roomImageDal = roomImageDal;
        }

        public void TDelete(RoomImage t)
        {
            _roomImageDal.Delete(t);
        }

        public RoomImage TGetById(int id)
        {
            return _roomImageDal.GetById(id);
        }

        public List<RoomImage> TGetList()
        {
            return _roomImageDal.GetList();
        }

        public List<RoomImage> TImagesByRoom(int id)
        {
            return _roomImageDal.ImagesByRoom(id);
        }

        public void TInsert(RoomImage t)
        {
            _roomImageDal.Insert(t);
        }

        public void TUpdate(RoomImage t)
        {
            _roomImageDal.Update(t);
        }
    }
}
