using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Squares.Domain.Entities;

namespace Squares.Infrastructure.Persistence.Configurations
{
    public class PointListConfiguration : IEntityTypeConfiguration<PointList>
    {
        public void Configure(EntityTypeBuilder<PointList> builder)
        {
            builder.HasKey(pointList => pointList.Id);
            builder.Property(pointList => pointList.Name).IsRequired().HasMaxLength(200);

            builder.OwnsMany(pointList => pointList.Points, p =>
            {
                p.ToTable("Points");
                p.Property<int>("Id");
                p.HasKey("Id");
                p.Property(x => x.x).IsRequired();
                p.Property(x => x.y).IsRequired();
            });

            builder.Navigation(pointList => pointList.Points)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
