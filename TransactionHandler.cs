public class TransactionHandler
{
    // Delete all the db file and exits.
    public static void HandleDelete(string dbName)
    {
        if (File.Exists(dbName))
        {
            File.Delete(dbName);
        }
        Environment.Exit(1);
    }

    // Iterate over the DB list and write all the transactions and exits.
    public static void HandleList(TransactionDatabase database)
    {
        foreach (var transaction in database.GetTransactions())
        {
            string sign = transaction.Type == TransactionType.Income ? "+" : "-";

            Console.WriteLine(
                $"{transaction.Date:dd/MM/yyyy} | " +
                $"{transaction.Description,-20} | " +
                $"{sign} R$ {transaction.ValueInCents / 100m:N2} | " +
                $"{transaction.Category ?? TransactionCategory.Home}");
        }
        Environment.Exit(1);
    }

    // Parse the command line arguments and add a transaction

    public static void HandleAddition(string[] arguments, TransactionDatabase database)
    {
        string description = arguments[0] ?? "";
        decimal value = decimal.Parse(arguments[1] ?? "0");
        TransactionCategory? category = Enum.Parse<TransactionCategory>(arguments[2]);

        long id = database.AddTransaction(
            type: TransactionType.Expense,
            category: category,
            description: description,
            value: value,
            date: DateOnly.FromDateTime(DateTime.Today));


        Console.WriteLine($"\nTransaction #{id} saved.\n");
    }
}