using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Repository;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public class EfRoomImageDal : GenericRepository<RoomImage>, IRoomImageDal
    {
        public List<RoomImage> ImagesByRoom(int id)
        {
            using (var context = new Context())
            {
                return context.RoomImages.Where(x => x.RoomId == id && x.Status == true).ToList();
            }
        }
    }
}
