using Microsoft.Data.Sqlite;

public class TransactionDatabase
{
    private readonly string _connectionString;


    public TransactionDatabase(string databasePath)
    {
        _connectionString = $"Data Source={databasePath}";
    }
    public void Initialize()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText =
            """
            CREATE TABLE IF NOT EXISTS transactions
            (
                id              INTEGER PRIMARY KEY AUTOINCREMENT,
                type            TEXT NOT NULL CHECK (type IN ('Income', 'Expense')),
                category        TEXT,
                description     TEXT NOT NULL,
                value_in_cents  INTEGER NOT NULL CHECK (value_in_cents > 0),
                date            TEXT NOT NULL
            );
            """;
        
        command.ExecuteNonQuery();
    }

    public long AddTransaction(
        TransactionType type,
        TransactionCategory? category,
        string description,
        decimal value,
        DateOnly date)
    {
        long valueInCents = checked(
            (long)decimal.Round(
                value * 100,
                0,
                MidpointRounding.AwayFromZero));

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText =
            """
            INSERT INTO transactions
                (type, category, description, value_in_cents, date)
            VALUES
                ($type, $category, $description, $value, $date);

            SELECT last_insert_rowid();
            """;

        command.Parameters.AddWithValue("$type", type.ToString());
        command.Parameters.AddWithValue(
            "$category",
            category is null ? DBNull.Value : category);
        command.Parameters.AddWithValue("$description", description);
        command.Parameters.AddWithValue("$value", valueInCents);
        command.Parameters.AddWithValue(
            "$date",
            date.ToString("dd-MM-yyyy"));

        return Convert.ToInt64(command.ExecuteScalar());
    }

    public List<FinanceTransaction> GetTransactions()
    {
        var transactions = new List<FinanceTransaction>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText =
            """
            SELECT
                id,
                type,
                category,
                description,
                value_in_cents,
                date
            FROM transactions
            ORDER BY date DESC, id DESC;
            """;

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var transaction = new FinanceTransaction(
                Id: reader.GetInt64(0),
                Type: Enum.Parse<TransactionType>(reader.GetString(1)),
                Category: reader.IsDBNull(2) ? null : Enum.Parse<TransactionCategory>(reader.GetString(2)),
                Description: reader.GetString(3),
                ValueInCents: reader.GetInt64(4),
                Date: DateOnly.ParseExact(
                    reader.GetString(5),
                    "dd-MM-yyyy")
            );

            transactions.Add(transaction);
        }

        return transactions;
    }
}