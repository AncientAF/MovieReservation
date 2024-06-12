namespace CinemaService.Infrastructure.Data.Configurations;

public class HallConfiguration : IEntityTypeConfiguration<Hall>
{
    public void Configure(EntityTypeBuilder<Hall> builder)
    {
        builder.HasKey(h => h.Id);
        builder.Property(hi => hi.Id).HasConversion(
            cinemaId => cinemaId.Value,
            dbId => HallId.Of(dbId));

        builder.Property(h => h.Name).IsRequired();

        builder.HasOne<Cinema>()
            .WithMany()
            .HasForeignKey(h => h.CinemaId)
            .IsRequired();

        builder.HasMany<Seat>()
            .WithOne()
            .HasForeignKey(s => s.HallId)
            .IsRequired();

    }
}