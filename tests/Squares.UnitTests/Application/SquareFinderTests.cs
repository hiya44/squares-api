using Squares.Application.Services;
using Squares.Domain.Entities;

namespace Squares.UnitTests.Application
{
    public class SquareFinderTests
    {
        private readonly SquareFinder _sut;

        public SquareFinderTests()
        {
            _sut = new SquareFinder();
        }

        [Test]
        public void FindSquares_WithSimpleSquare()
        {
            var points = new List<Point>
            {
                new(-1, 1), new(1, 1), new(1, -1), new(-1, -1)
            };

            var result = _sut.FindSquares(points).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void FindSquares_WithRotatedSquare()
        {
            var points = new List<Point>
            {
                new(0, 1), new(1, 0), new(0, -1), new(-1, 0)
            };

            var result = _sut.FindSquares(points).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void FindSquares_WithGridOfPoints()
        {
            var points = new List<Point>
            {
                new(0, 1), new(1, 1), new(0, 0), new(1, 0)
            };

            var result = _sut.FindSquares(points).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void FindSquares_WithIncompletePoints()
        {
            var points = new List<Point>
            {
                new(0, 0), new(0, 1), new(1, 0)
            };

            var result = _sut.FindSquares(points);

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void FindSquares_WithDuplicatePoints()
        {
            var points = new List<Point>
            {
                new(0, 0), new(0, 1), new(1, 0), new(1, 1),
                new(0, 0), new(0, 1)
            };

            var result = _sut.FindSquares(points).ToList();

            Assert.That(result.Count, Is.EqualTo(1));
        }
    }
}
