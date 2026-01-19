namespace Squares.Domain.Entities
{
    public class PointList
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        private readonly HashSet<Point> _points = new();

        public IReadOnlyCollection<Point> Points => _points.ToList().AsReadOnly();

        public PointList(string name)
        {
            Name = name;
        }

        public void AddPoint(Point point)
        {
            _points.Add(point);
        }

        public void RemovePoint(Point point)
        {
            _points.Remove(point);
        }

        public void ImportPoints(IEnumerable<Point> points)
        {
            foreach (var point in points)
            {
                _points.Add(point);
            }
        }
    }
}
