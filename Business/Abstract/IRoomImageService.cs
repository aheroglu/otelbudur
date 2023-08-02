using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRoomImageService : IGenericService<RoomImage>
    {
        public List<RoomImage> TImagesByRoom(int id);
    }
}
