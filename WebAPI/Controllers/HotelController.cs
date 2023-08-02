using Business.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_hotelService.TGetList());
        }

        [HttpPost]
        public IActionResult Add(Hotel hotel)
        {
            _hotelService.TInsert(hotel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var hotel = _hotelService.TGetById(id);
            _hotelService.TDelete(hotel);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Hotel hotel)
        {
            _hotelService.TUpdate(hotel);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var hotel = _hotelService.TGetById(id);
            return Ok(hotel);
        }
    }
}
