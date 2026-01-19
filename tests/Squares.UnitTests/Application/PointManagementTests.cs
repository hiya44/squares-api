using NSubstitute;
using Squares.Application.Interfaces;
using Squares.Application.Services;
using Squares.Domain.Entities;
using Squares.Domain.Interfaces;

namespace Squares.UnitTests.Application
{
    public class PointManagementTests
    {
        private readonly IPointRepository _repository = Substitute.For<IPointRepository>();
        private readonly ISquareFinder _finder = Substitute.For<ISquareFinder>();
        private readonly SquareApplicationService _sut;

        public PointManagementTests()
        {
            _sut = new SquareApplicationService(_repository, _finder);
        }

        [Test]
        public async Task GetIdentifiedSquares_SholdReturnEmpty()
        {
            _repository.GetListWithPointsAsync(99).Returns((PointList?)null);

            var result = await _sut.GetIdentifiedSquaresAsync(99);

            Assert.That(result.TotalCount, Is.EqualTo(0));
            Assert.That(result.Squares, Is.Empty);
        }

        [Test]
        public async Task ImportPoints_ShouldCallRepositorySave()
        {
            var existingList = new PointList("Existing");
            _repository.GetListWithPointsAsync(1).Returns(existingList);

            var points = new List<Point>
            {
                new(1, 1), new(2, 2)
            };

            existingList.ImportPoints(points);
            await _repository.SaveChangesAsync();

            await _repository.Received(1).SaveChangesAsync();
        }
    }
}
