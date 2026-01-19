using Microsoft.EntityFrameworkCore;
using Squares.Domain.Entities;

namespace Squares.Infrastructure.Persistence
{
    public class SquaresDbContext : DbContext
    {
        public SquaresDbContext(DbContextOptions<SquaresDbContext> options) : base(options) { }

        public DbSet<PointList> PointLists => Set<PointList>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SquaresDbContext).Assembly);
        }
    }
}
