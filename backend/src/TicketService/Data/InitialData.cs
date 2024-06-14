namespace TicketService.Data;

public class InitialData
{
    public static IEnumerable<Ticket> Tickets => new List<Ticket>
    {
        new()
        {
            Id = new Guid("AB8AE34B-13AA-4C26-9196-1EBA244FB8B5"),
            UserId = Guid.Empty,
            SessionId = new Guid("1D6ECB7C-5583-4C7A-ACD1-2E732CEB3198"),
            SeatId = new Guid("D2123185-8167-4D2A-B604-3E7469B08CC8"),
            Number = 33,
            Row = 44,
            Status = TicketStatus.Unreserved
        },
        new()
        {
            Id = new Guid("1237673E-6CF4-47E6-B098-AA789ECB27D8"),
            UserId = Guid.Empty,
            SessionId = new Guid("1AF5FDC1-6AEA-495E-B78D-6342203D2A73"),
            SeatId = new Guid("8C5456B4-935E-44FA-9F53-738FB1ED0BB1"),
            Number = 55,
            Row = 66,
            Status = TicketStatus.Unreserved
        },
        new()
        {
            Id = new Guid("758842AB-58E2-4E13-97F0-ECAB8472E7CD"),
            UserId = Guid.Empty,
            SessionId = new Guid("900E9D2D-3492-4682-A952-97101C248AA6"),
            SeatId = new Guid("40F8C713-6C6D-4CA6-91B8-8B2E7DC9F09C"),
            Number = 77,
            Row = 88,
            Status = TicketStatus.Unreserved
        }
    };
}