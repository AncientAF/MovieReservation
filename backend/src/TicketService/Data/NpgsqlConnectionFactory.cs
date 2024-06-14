using Npgsql;

namespace TicketService.Data;

public class NpgsqlConnectionFactory(string connectionString)
{
    public NpgsqlConnection Create()
    {
        return new NpgsqlConnection(connectionString);
    }
}