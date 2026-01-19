using Microsoft.AspNetCore.Mvc;
using Squares.Application.DTOs;
using Squares.Application.Interfaces;
using Squares.Domain.Entities;

namespace Squares.WebAPI.Controllers
{
    [ApiController]
    [Route("/v1/pointLists/")]
    public class PointsController : ControllerBase
    {
        private readonly IPointRepository _repository;

        public PointsController(IPointRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("{listId:int}/points")]
        [EndpointSummary("Add new Point")]
        [EndpointDescription("Adds new Point to specified Point List.")]
        public async Task<IActionResult> AddPoint(int listId, [FromBody] PointDto dto)
        {
            var point = new Point(dto.x, dto.y);
            await _repository.AddPointAsync(listId, point);
            await _repository.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{listId:int}/points/{x:int}/{y:int}")]
        [EndpointSummary("Delete specific Point")]
        [EndpointDescription("Deletes specified Point from Point List.")]
        public async Task<IActionResult> DeletePoint(int listId, int x, int y)
        {
            var point = new Point(x, y);
            await _repository.DeletePointAsync(listId, point);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("import")]
        [EndpointSummary("Import new Point list.")]
        [EndpointDescription("Import new Point List to database.")]
        public async Task<IActionResult> ImportPoints([FromBody] CreateListRequest request)
        {
            var newList = new PointList(request.Name);

            var domainPoints = request.Points.Select(p => new Point(p.x, p.y));
            newList.ImportPoints(domainPoints);

            await _repository.CreateListAsync(newList);
            await _repository.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetListById),
                new { listId = newList.Id },
                new { Id = newList.Id, newList.Name, PointCount = newList.Points.Count, }
            );
        }

        [HttpGet("{listId:int}", Name = "GetListById")]
        public async Task<IActionResult> GetListById(int listId)
        {
            var list = await _repository.GetListWithPointsAsync(listId);
            return list == null ? NotFound() : Ok(list);
        }
    }
}
