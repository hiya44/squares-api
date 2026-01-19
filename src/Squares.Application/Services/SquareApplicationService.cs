using Squares.Application.DTOs;
using Squares.Application.Interfaces;
using Squares.Domain.Entities;
using Squares.Domain.Interfaces;

namespace Squares.Application.Services
{
    public class SquareApplicationService
    {
        private readonly IPointRepository _repository;
        private readonly ISquareFinder _squareFinder;

        public SquareApplicationService(IPointRepository repository, ISquareFinder squareFinder)
        {
            _repository = repository;
            _squareFinder = squareFinder;
        }

        public async Task<SquareResponseDto> GetIdentifiedSquaresAsync(int listId)
        {
            var pointList = await _repository.GetListWithPointsAsync(listId);

            if (pointList == null) return new SquareResponseDto(0, new());

            var squares = _squareFinder.FindSquares(pointList.Points);

            var result = squares.Select((s => s.Select(p => new PointDto(p.x, p.y)).ToList()))
                .ToList();

            return new SquareResponseDto(result.Count, result);
        }
    }
}
