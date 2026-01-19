using Squares.Domain.Entities;

namespace Squares.Application.Interfaces
{
    public interface IPointRepository
    {
        Task<PointList?> GetListWithPointsAsync(int listId);
        Task AddPointAsync(int listId, Point point);
        Task DeletePointAsync(int listId, Point point);
        Task CreateListAsync(PointList list);
        Task SaveChangesAsync();
    }
}
