using Business.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_roomService.TGetList());
        }

        [HttpPost]
        public IActionResult Add(Room room)
        {
            _roomService.TInsert(room);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var room = _roomService.TGetById(id);
            _roomService.TDelete(room);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Room room)
        {
            _roomService.TUpdate(room);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var room = _roomService.TGetById(id);
            return Ok(room);
        }
    }
}
