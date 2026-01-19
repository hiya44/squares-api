using Squares.Domain.Entities;
using Squares.Domain.Interfaces;

namespace Squares.Application.Services
{
    public class SquareFinder : ISquareFinder
    {
        public IEnumerable<IEnumerable<Point>> FindSquares(IEnumerable<Point> points)
        {
            var pointList = points.Distinct().ToList();
            var pointSet = pointList.ToHashSet();
            var results = new List<List<Point>>();
            var foundSquares = new HashSet<string>();

            for (int i = 0; i < pointList.Count; i++)
            {
                for (int j = i + 1; j < pointList.Count; j++)
                {
                    var p1 = pointList[i];
                    var p2 = pointList[j];

                    int dx = p2.x - p1.x;
                    int dy = p2.y - p1.y;

                    CheckSquare(p1, p2, new Point(p2.x - dy, p2.y + dx), new Point(p1.x - dy, p1.y + dx));
                    CheckSquare(p1, p2, new Point(p2.x + dy, p2.y - dx), new Point(p1.x + dy, p1.y - dx));
                }
            }

            void CheckSquare(Point p1, Point p2, Point p3, Point p4)
            {
                if (pointSet.Contains(p3) && pointSet.Contains(p4))
                {
                    var square = new[] { p1, p2, p3, p4 }.OrderBy(p => p.x).ThenBy(p => p.y).ToList();
                    var key = string.Join("|", square.Select(p => $"{p.x},{p.y}"));

                    if (foundSquares.Add(key))
                    {
                        results.Add(square);
                    }
                }
            }

            return results;
        }
    }
}
