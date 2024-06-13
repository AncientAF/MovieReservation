namespace CinemaService.Infrastructure.Data.Configurations;

public class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(si => si.Id).HasConversion(
            seatId => seatId.Value,
            dbId => SeatId.Of(dbId));

        /*builder.Property(si => si.HallId).HasConversion(
            hallId => hallId.Value,
            dbId => HallId.Of(dbId));*/

        builder.Property(s => s.Row).IsRequired();
        builder.Property(s => s.Number).IsRequired();

        builder.HasOne<Hall>()
            .WithMany(h => h.Seats)
            .HasForeignKey(s => s.HallId)
            .IsRequired();
    }
}