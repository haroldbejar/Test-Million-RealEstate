namespace PropertyService.Domain.Exceptions
{
    public sealed class PropertyDomainException : Exception
    {
        public string Code { get; }

        internal PropertyDomainException(string code, string message) : base(message)
        {
            Code = code;
        }

        public static PropertyDomainException InvalidNullOrEmpty(string field)
        {
            return new PropertyDomainException("INVALID_FIELD", $"{field} cannot be null or empty.");
        }

        public static PropertyDomainException InvalidNegative(string field)
        {
            return new PropertyDomainException("INVALID_NEGATIVE_VALUE", $"{field} cannot be negative.");
        }

        public static PropertyDomainException InvalidYearBuilt()
        {
            return new PropertyDomainException("INVALID_YEAR_BUILT", "Invalid year built.");
        }
    }
}