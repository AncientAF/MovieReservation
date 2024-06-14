namespace TicketService.Models;

public class Ticket
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SessionId { get; set; }
    public Guid SeatId { get; set; }
    public int Number { get; set; }
    public int Row { get; set; }
    public TicketStatus Status { get; set; }
}