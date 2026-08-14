using System.Transactions;

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
    TransactionHandler.HandleList(database);
}

TransactionHandler.HandleAddition(args, database);

