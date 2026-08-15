public class TransactionCalculations
{
    public static decimal TotalExpenses(TransactionDatabase database)
    {
        decimal totalExpenses = (decimal)0.0;
        foreach (var transaction in database.GetTransactions())
        {
            totalExpenses += transaction.ValueInCents / 100m;
        }

        return totalExpenses;
    }
}