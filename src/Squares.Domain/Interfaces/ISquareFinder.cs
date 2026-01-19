using Squares.Domain.Entities;

namespace Squares.Domain.Interfaces
{
    public interface ISquareFinder
    {
        IEnumerable<IEnumerable<Point>> FindSquares(IEnumerable<Point> points);
    }
}
