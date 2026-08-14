public enum TransactionCategory
{
    Home,
    Food,
    Transportation,
    Debt
}

public record FinanceTransaction
(
    int Id,
    TransactionCategory Category,
    string Description,
    float Value,
    DateOnly date
);