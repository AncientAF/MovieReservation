namespace CinemaService.Application.Cinemas.Commands;

public static class CinemaCreation
{
    public static Cinema CreateCinema(Guid cinemaId, AddressDto addressDto, string name, List<HallDto>? hallDtos)
    {
        var address = CreateAddress(addressDto);
        var cinemaToCreate = Cinema.Create(CinemaId.Of(cinemaId), name, address);

        if (hallDtos != null)
            AddHalls(cinemaToCreate, hallDtos);

        return cinemaToCreate;
    }

    public static Cinema CreateCinema(AddressDto addressDto, string name, List<HallDto>? hallDtos)
    {
        return CreateCinema(Guid.NewGuid(), addressDto, name, hallDtos);
    }

    private static void AddHalls(Cinema cinema, List<HallDto> hallDtos)
    {
        foreach (var newHall in hallDtos.Select(hallDto => CreateHall(cinema.Id, hallDto)))
            cinema.Add(newHall);
    }

    private static Hall CreateHall(CinemaId cinemaId, HallDto hallDto)
    {
        var hall = Hall.Create(HallId.Of(Guid.NewGuid()), cinemaId, hallDto.Name);

        foreach (var seatDto in hallDto.Seats)
        {
            var seatId = SeatId.Of(Guid.NewGuid());
            hall.Add(seatId, seatDto.Row, seatDto.Number);
        }

        return hall;
    }

    private static Address CreateAddress(AddressDto addressDto)
    {
        return Address.Of(addressDto.AddressLine, addressDto.Country, addressDto.State, addressDto.ZipCode);
    }
}