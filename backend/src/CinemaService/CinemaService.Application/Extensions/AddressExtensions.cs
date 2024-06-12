namespace CinemaService.Application.Extensions;

public static class AddressExtensions
{
    public static AddressDto ToDto(this Address address)
    {
        return new AddressDto(address.AddressLine, address.Country, address.State, address.ZipCode);
    }
}