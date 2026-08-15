const string DbName = "finance.db";
var databasePath = Path.Combine(Environment.CurrentDirectory, DbName);
var database = new TransactionDatabase(databasePath);
database.Initialize();

if(args.Length == 0) Environment.Exit(1);

if (args[0] == "delete")
{
    TransactionHandler.HandleDelete(DbName);
}
else if (args[0] == "list")
{
    if (args.Length > 1) TransactionHandler.HandleList(database, Enum.Parse<TransactionCategory>(args[1]));
    TransactionHandler.HandleList(database);
}
else if (args[0] == "total")
{
    Console.WriteLine(TransactionCalculations.TotalExpenses(database));
    Environment.Exit(1);
}

TransactionHandler.HandleAddition(args, database);

