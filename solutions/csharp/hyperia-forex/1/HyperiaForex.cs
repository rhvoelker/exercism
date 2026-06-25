public struct CurrencyAmount
{
    private decimal amount;
    private string currency;

    public CurrencyAmount(decimal amount, string currency)
    {
        this.amount = amount;
        this.currency = currency;
    }

    public static bool operator ==(CurrencyAmount a, CurrencyAmount b)
    {
        AssertCurrenciesAreTheSame(a, b);
        return a.amount == b.amount && a.currency == b.currency;
    }

    public static bool operator !=(CurrencyAmount a, CurrencyAmount b) => !(a == b);

    public static bool operator <(CurrencyAmount a, CurrencyAmount b)
    {
        AssertCurrenciesAreTheSame(a, b);
        return a.amount < b.amount;
    }

    public static bool operator >(CurrencyAmount a, CurrencyAmount b)
    {
        AssertCurrenciesAreTheSame(a, b);
        return a.amount > b.amount;
    }

    public static CurrencyAmount operator +(CurrencyAmount a, CurrencyAmount b)
    {
        AssertCurrenciesAreTheSame(a, b);
        return new CurrencyAmount(a.amount + b.amount, a.currency);
    }

    public static CurrencyAmount operator -(CurrencyAmount a, CurrencyAmount b)
    {
        AssertCurrenciesAreTheSame(a, b);
        return new CurrencyAmount(a.amount - b.amount, a.currency);
    }

    public static CurrencyAmount operator *(CurrencyAmount a, decimal b) => new CurrencyAmount(a.amount * b, a.currency);

    public static CurrencyAmount operator /(CurrencyAmount a, decimal b) => new CurrencyAmount(a.amount / b, a.currency);

    public static explicit operator double(CurrencyAmount currency) => (double)currency.amount;

    public static implicit operator decimal(CurrencyAmount currency) => currency.amount;

    private static void AssertCurrenciesAreTheSame(CurrencyAmount a, CurrencyAmount b)
    {
        if (a.currency != b.currency)
        {
            throw new ArgumentException("Currencies must be the same.", nameof(CurrencyAmount.currency));
        }
    }
}
