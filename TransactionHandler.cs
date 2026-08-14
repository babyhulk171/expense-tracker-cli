public class TransactionHandler
{
    public static void HandleDelete(string dbName)
    {
        if (File.Exists(dbName))
        {
            File.Delete(dbName);
        }
        Environment.Exit(1);
    }
}