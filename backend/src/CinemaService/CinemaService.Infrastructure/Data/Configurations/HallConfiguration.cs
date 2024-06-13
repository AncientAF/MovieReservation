namespace CinemaService.Infrastructure.Data.Configurations;

public class HallConfiguration : IEntityTypeConfiguration<Hall>
{
    public void Configure(EntityTypeBuilder<Hall> builder)
    {
        builder.HasKey(h => h.Id);
        builder.Property(hi => hi.Id).HasConversion(
            halId => halId.Value,
            dbId => HallId.Of(dbId));

        /*builder.Property(hi => hi.CinemaId).HasConversion(
            cinemaId => cinemaId.Value,
            dbId => CinemaId.Of(dbId));*/

        builder.Property(h => h.Name).IsRequired();

        builder.HasOne<Cinema>()
            .WithMany(c => c.Halls)
            .HasForeignKey(h => h.CinemaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        ;

        builder.HasMany(h => h.Seats)
            .WithOne()
            .HasForeignKey(s => s.HallId)
            .IsRequired();
    }
}