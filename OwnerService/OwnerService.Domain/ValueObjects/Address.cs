using OwnerService.Domain.Exceptions;

public sealed record Address
{
    public string Value { get; }

    public Address(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new OwnerDomainException("Address cannot be empty.");
        }
        if (value.Length > 200)
        {
            throw new OwnerDomainException("Address cannot be longer than 200 characters.");
        }
        Value = value;
    }

    public static implicit operator string(Address address) => address.Value;
    public static implicit operator Address(string value) => new(value);
}