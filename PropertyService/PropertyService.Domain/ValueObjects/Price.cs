using PropertyService.Domain.Exceptions;

namespace PropertyService.Domain.ValueObjects
{
    public sealed record Price
    {
        public decimal Amount { get; init; }
        public string Currency { get; init; } // e.g., "USD", "EUR", "GBP"

        public Price(decimal amount, string currency)
        {
            if (amount < 0) throw PropertyDomainException.InvalidNegative(nameof(Amount));
            if (string.IsNullOrWhiteSpace(currency)) throw PropertyDomainException.InvalidNullOrEmpty(nameof(Currency));

            Amount = amount;
            Currency = currency;
        }
    }
}