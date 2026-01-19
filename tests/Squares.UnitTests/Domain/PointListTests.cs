using Squares.Domain.Entities;

namespace Squares.UnitTests.Domain
{
    public class PointListTests
    {
        [Test]
        public void AddPoint_ShouldIncreaseCount()
        {
            var list = new PointList("Test List");
            var point = new Point(5, 5);

            list.AddPoint(point);

            Assert.That(list.Points.Count, Is.EqualTo(1));
            Assert.That(list.Points.First(), Is.EqualTo(point));
        }

        [Test]
        public void AddPoint_ShouldNotDuplicate()
        {
            var list = new PointList("Test List");
            var point = new Point(10, 10);

            list.AddPoint(point);
            list.AddPoint(new Point(10,10));

            Assert.That(list.Points.Count, Is.EqualTo(1));
        }

        [Test]
        public void RemovePoint_ShouldRemovePoint()
        {
            var list = new PointList("Test List");
            var point = new Point(1, 1);

            list.RemovePoint(point);
            
            Assert.That(list.Points, Is.Empty);
        }
    }
}
