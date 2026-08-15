public class TransactionCalculations
{
    public static decimal TotalExpenses(TransactionDatabase database)
    {
        decimal totalExpenses = (decimal)0.0;
        foreach (var transaction in database.GetTransactions())
        {
            if (transaction.Type == TransactionType.Income) totalExpenses += transaction.ValueInCents / 100m;
            else if (transaction.Type == TransactionType.Expense) totalExpenses -= transaction.ValueInCents / 100m;
        }

        return totalExpenses;
    }
}