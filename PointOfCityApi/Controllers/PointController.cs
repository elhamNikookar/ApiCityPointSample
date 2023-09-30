using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointOfCityApi.Context;
using PointOfCityApi.Models;

namespace PointOfCityApi.Controllers
{
    [ApiController]
    [Route("api/point")]
    public class PointController : ControllerBase
    {

        #region Constructor

        private readonly MyContext _context;
        public PointController(MyContext context)
        {
            _context = context;
        }

        #endregion


        [HttpGet("{cityId}")]
        public async Task<ActionResult<IEnumerable<Models.Point>>> GetPoints(int cityId)
        {
            var points = await _context.Points.Where(p => p.CityID == cityId).ToListAsync();

            if (points == null) return NotFound();

            return Ok(points);

        }

        [HttpGet("{cityId}/{pointId}", Name = "getPoint")]
        public async Task<ActionResult<PointViewModel>> GetPoint(int cityId, int pointId)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(c => c.CityID == cityId);

            if (city == null) return NotFound("This City Not Available!");

            var point = await _context.Points
                .FirstOrDefaultAsync(p => p.PointID == pointId && p.CityID == cityId);

            if (point == null) return NotFound("Point Not Found!");

            PointViewModel result = new PointViewModel()
            {
                PointID = point.PointID,
                PointName = point.PointName,
                PointDescription = point.PointDescription,
            };

            return Ok(result);

        }

        [HttpPost("{cityId}")]
        public async Task<ActionResult<Models.Point>> CreatePoint(int cityId,
                                        CreatePointViewModel createPointVM)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var city = await _context.Cities
                .FirstOrDefaultAsync(c => c.CityID == cityId);

            if (city == null) return NotFound("This city not found!");

            Models.Point createPoint = new Models.Point()
            {
                CityID = city.CityID,
                PointName = createPointVM.PointName,
                PointDescription = createPointVM.PointDescription
            };

            _context.Points.Add(createPoint);
            _context.SaveChanges();

            var pointViewModel = new PointViewModel()
            {
                PointID = createPoint.PointID,
                PointName = createPoint.PointName,
                PointDescription = createPoint.PointDescription
            };

            return CreatedAtAction("getPoint", new
            {
                cityId = city.CityID,
                pointId = createPoint.PointID
            }, createPoint
            );
        }

        [HttpPut("{cityId}/{pointId}")]
        public async Task<ActionResult<Models.Point>> UpdatePoint(int cityId, int pointId,
                                                UpdatePointViewModel updatePointVM)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var city = await _context.Cities.FirstOrDefaultAsync(c => c.CityID == cityId);
            if (city == null) return NotFound("That city Not found!");

            var point = await _context.Points.FirstOrDefaultAsync(p => p.PointID == pointId && p.CityID == city.CityID);
            if (point == null) return NotFound("Point Not Found!");

            point.PointName = updatePointVM.PointName; ;
            point.PointDescription = updatePointVM.PointDescription; ;

            _context.Points.Update(point);
            _context.SaveChanges();

            return NoContent();
        }



        [HttpPatch("{cityId}/{pointId}")]
        public async Task<ActionResult<Models.Point>> UpdatePartialPoint(int cityId, int pointId,
                                                JsonPatchDocument<UpdatePointViewModel> patchDocument)
        {

            var city = await _context.Cities.FirstOrDefaultAsync(c => c.CityID == cityId);
            if (city == null) return NotFound("That city Not found!");

            var point = await _context.Points.FirstOrDefaultAsync(p => p.PointID == pointId && p.CityID == city.CityID);
            if (point == null) return NotFound("Point Not Found!");

            var pointForPatch = new UpdatePointViewModel()
            {
                PointName = point.PointName,
                PointDescription = point.PointDescription
            };

            patchDocument.ApplyTo(pointForPatch, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!TryValidateModel(pointForPatch)) return BadRequest(modelState: ModelState);

            point.PointName = pointForPatch.PointName;
            point.PointDescription = pointForPatch.PointDescription;

            _context.Points.Update(point);
            _context.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{cityId}/{pointId}")]
        public async Task<ActionResult<PointViewModel>> DeletePoint(int cityId, int pointId)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(c => c.CityID == cityId);
            if (city == null) return NotFound("That city Not found!");

            var point = await _context.Points.FirstOrDefaultAsync(p => p.PointID == pointId && p.CityID == city.CityID);
            if (point == null) return NotFound("Point Not Found!");

            _context.Points.Remove(point);
            _context.SaveChanges();

            return NoContent();
        }


    }
}
