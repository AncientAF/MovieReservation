using Npgsql;

namespace TicketService.Data;

public static class DbInitializer
{
    public static async Task InitializeDb(NpgsqlConnectionFactory connectionFactory)
    {
        await using var connection = connectionFactory.Create();
        await connection.OpenAsync();

        await CreateTicketsTable(connection);
        await PopulateTicketsTable(connection);

        await connection.CloseAsync();
    }

    private static async Task PopulateTicketsTable(NpgsqlConnection connection)
    {
        const string insertQuery = """
                                   INSERT INTO Tickets (Id, UserId, SessionId, SeatId, Number, Row, Status)
                                   VALUES (@Id, @UserId, @SessionId, @SeatId, @Number, @Row, @Status)
                                   ON CONFLICT (Id) DO NOTHING;
                                   """;
        await connection.ExecuteAsync(insertQuery, InitialData.Tickets);
    }

    private static async Task CreateTicketsTable(NpgsqlConnection connection)
    {
        const string createTableQuery = """
                                        CREATE TABLE IF NOT EXISTS Tickets (
                                            Id UUID PRIMARY KEY,
                                            UserId UUID,
                                            SessionId UUID NOT NULL,
                                            SeatId UUID NOT NULL,
                                            Number INT NOT NULL,
                                            Row INT NOT NULL,
                                            Status INT NOT NULL
                                        );
                                        """;

        await connection.ExecuteAsync(createTableQuery);
    }
}