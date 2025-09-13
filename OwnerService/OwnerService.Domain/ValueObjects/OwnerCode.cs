using OwnerService.Domain.Exceptions;

namespace OwnerService.Domain.ValueObjects
{
    public sealed record OwnerCode
    {
        public int Value { get; }

        public OwnerCode(int value)
        {
            if (value < 1000 || value > 999999)
            {
                throw new OwnerDomainException("Invalid owner code.");
            }
            Value = value;
        }

        public static implicit operator int(OwnerCode ownerCode) => ownerCode.Value;
        public static implicit operator OwnerCode(int value) => new(value);
    }
}
