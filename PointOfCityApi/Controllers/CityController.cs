using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfCityApi.Context;
using PointOfCityApi.Models;

namespace PointOfCityApi.Controllers
{
    [ApiController]
    [Route("api/city")]
    public class CityController : ControllerBase
    {
        #region Constructor

        private readonly MyContext _context;
        public CityController(MyContext context)
        {
            _context = context;
        }

        #endregion

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            var cities = await _context.Cities.ToListAsync();
            List<CityViewModel> result = new List<CityViewModel>();
            foreach (var city in cities)
            {
                result.Add(new CityViewModel()
                {
                    CityID = city.CityID,
                    Name = city.Name,
                    Description = city.Description,
                    CountPoints = _context.Points.Count(c => c.CityID == city.CityID)
                });
            }

            return Ok(result);
        }

        [HttpGet("{cityId}")]
        public async Task<ActionResult<City>> GetCity(int cityId, bool isExistPoints = false)
        {
            CityViewModel? city = await _context.Cities
                .Select(c => new CityViewModel()
                {
                    CityID = c.CityID,
                    Name = c.Name,
                    Description = c.Description,
                    CountPoints = _context.Points.Count(p => p.CityID == c.CityID)
                })
                .FirstOrDefaultAsync(c => c.CityID == cityId);

            if (city == null) return NotFound();

            if (!isExistPoints) return Ok(city);

            var cityWithPoints = await _context.Cities.Include(c => c.Points)
                .FirstOrDefaultAsync(city => city.CityID == cityId);

            return Ok(cityWithPoints);
        }


    }
}
