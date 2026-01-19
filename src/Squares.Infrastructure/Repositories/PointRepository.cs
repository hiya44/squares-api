using Microsoft.EntityFrameworkCore;
using Squares.Application.Interfaces;
using Squares.Domain.Entities;
using Squares.Infrastructure.Persistence;

namespace Squares.Infrastructure.Repositories
{
    public class PointRepository : IPointRepository
    {
        private readonly SquaresDbContext _context;

        public PointRepository(SquaresDbContext context)
        {
            _context = context;
        }

        public async Task<PointList?> GetListWithPointsAsync(int listId)
        {
            return await _context.PointLists
                .Include(pl => pl.Points)
                .FirstOrDefaultAsync(pl => pl.Id == listId);
        }

        public async Task AddPointAsync(int listId, Point point)
        {
            var list = await GetListWithPointsAsync(listId);
            if (list != null)
            {
                list.AddPoint(point);
            }
        }

        public async Task DeletePointAsync(int listId, Point point)
        {
            var list = await GetListWithPointsAsync(listId);
            if (list != null)
            {
                list.RemovePoint(point);
            }
        }

        public async Task CreateListAsync(PointList list)
        {
            await _context.PointLists.AddAsync(list);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
