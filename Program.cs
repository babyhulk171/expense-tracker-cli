const string DbName = "finance.db";

if (args[0] == "delete")
{
    if (File.Exists(DbName))
    {
        File.Delete(DbName);
    }
    Environment.Exit(1);
}
else if (args[0] == "list")
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
}

var databasePath = Path.Combine(Environment.CurrentDirectory, DbName);

var database = new TransactionDatabase(databasePath);

database.Initialize();

string description = args[0] ?? "";
decimal value = decimal.Parse(args[1] ?? "0");
TransactionCategory? category = Enum.Parse<TransactionCategory>(args[2]);

long id = database.AddTransaction(
    type: TransactionType.Expense,
    category: category,
    description: description,
    value: value,
    date: DateOnly.FromDateTime(DateTime.Today));


Console.WriteLine($"\nTransaction #{id} saved.\n");

