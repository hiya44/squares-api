using Microsoft.AspNetCore.Mvc;
using Squares.Application.Services;

namespace Squares.WebAPI.Controllers
{
    [ApiController]
    [Route("/v1/pointLists/{listId:int}/squares")]
    public class SquaresController : ControllerBase
    {
        private readonly SquareApplicationService _squareService;

        public SquaresController(SquareApplicationService squareService)
        {
            _squareService = squareService;
        }

        [HttpGet]
        [EndpointSummary(("Identify all Squares"))]
        [EndpointDescription("Analyzes the Point list and returns all valid squares identified.")]
        public async Task<IActionResult> GetSquares(int listId)
        {
            var result = await _squareService.GetIdentifiedSquaresAsync(listId);
            return Ok(result);
        }
    }
}
