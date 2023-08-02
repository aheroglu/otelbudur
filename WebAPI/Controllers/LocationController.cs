using Business.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_locationService.TGetList());
        }

        [HttpPost]
        public IActionResult Add(Location location)
        {
            _locationService.TInsert(location);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var location = _locationService.TGetById(id);
            _locationService.TDelete(location);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Location location)
        {
            _locationService.TUpdate(location);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var location = _locationService.TGetById(id);
            return Ok(location);
        }
    }
}
