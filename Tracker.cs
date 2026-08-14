public enum TransactionCategory
{
    Home,
    Food,
    Transportation,
    Debt
}

public enum TransactionType
{
    Income,
    Expense
}

public record FinanceTransaction
(
    long Id,
    TransactionType Type,
    TransactionCategory? Category,
    string Description,
    long ValueInCents,
    DateOnly Date
);