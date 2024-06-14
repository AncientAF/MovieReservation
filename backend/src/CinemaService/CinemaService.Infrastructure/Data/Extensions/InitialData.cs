namespace CinemaService.Infrastructure.Data.Extensions;

public static class InitialData
{
    public static IEnumerable<Cinema> Cinemas
    {
        get
        {
            var address1 = Address.Of("Юг", "Россия", "Московская область", "77777");
            var address2 = Address.Of("Север", "Россия", "Московская область", "77777");


            var cinemas = new List<Cinema>
            {
                Cinema.Create(CinemaId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), "Костино", address1),
                Cinema.Create(CinemaId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), "Сатурн", address2)
            };

            var hall1 = Hall.Create(HallId.Of(new Guid("BC913787-3F3C-4325-8146-9DFF95F388AB")), cinemas[0].Id,
                "1-й этаж");
            var hall2 = Hall.Create(HallId.Of(new Guid("BA074F75-B0B0-476E-B578-13F36D74F834")), cinemas[0].Id,
                "2-й этаж");

            var hall3 = Hall.Create(HallId.Of(new Guid("BE218873-A7FF-446C-99D2-EF010AB3959F")), cinemas[1].Id,
                "1-й этаж");
            var hall4 = Hall.Create(HallId.Of(new Guid("30D655E8-7655-4CDC-B208-6726EDE91D22")), cinemas[1].Id,
                "2-й этаж");
            
            //seats for ticketService initial data
            hall1.Add(SeatId.Of(new Guid("D2123185-8167-4D2A-B604-3E7469B08CC8")),33, 44 );
            hall2.Add(SeatId.Of(new Guid("8C5456B4-935E-44FA-9F53-738FB1ED0BB1")),55, 66 ); 
            hall3.Add(SeatId.Of(new Guid("40F8C713-6C6D-4CA6-91B8-8B2E7DC9F09C")),77, 88 ); 

            
            
            CreateSeatsForHall(hall1);
            CreateSeatsForHall(hall2);
            CreateSeatsForHall(hall3);
            CreateSeatsForHall(hall4);

            cinemas[0].Add(hall1);
            cinemas[0].Add(hall2);

            cinemas[1].Add(hall3);
            cinemas[1].Add(hall4);


            return cinemas;
        }
    }

    private static void CreateSeatsForHall(Hall hall, int rows = 4, int maxNumberOfSeatsInRow = 10)
    {
        for (var i = 1; i <= rows; i++)
        for (var j = 1; j <= maxNumberOfSeatsInRow; j++)
            hall.Add(SeatId.Of(Guid.NewGuid()), i, j);
    }
}