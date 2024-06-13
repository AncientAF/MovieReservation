namespace CinemaService.Infrastructure.Data.Configurations;

public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
{
    public void Configure(EntityTypeBuilder<Cinema> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(ci => ci.Id).HasConversion(
            cinemaId => cinemaId.Value,
            dbId => CinemaId.Of(dbId));

        builder.Property(c => c.Name).IsRequired();

        builder.HasMany(c => c.Halls)
            .WithOne()
            .HasForeignKey(h => h.CinemaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.ComplexProperty(c => c.Address, addressBuilder =>
        {
            addressBuilder.Property(a => a.AddressLine).IsRequired();
            addressBuilder.Property(a => a.Country).IsRequired();
            addressBuilder.Property(a => a.State).IsRequired();
            addressBuilder.Property(a => a.ZipCode).IsRequired();
        });
    }
}