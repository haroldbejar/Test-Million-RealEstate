using PropertyService.Domain.Exceptions;

namespace PropertyService.Domain.ValueObjects
{
    public sealed record Location
    {
        public string City { get; init; }
        public string State { get; init; }
        public string Country { get; init; }

        public Location(string city, string state, string country)
        {

            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City cannot be null or empty.", nameof(city));
            if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException("State cannot be null or empty.", nameof(state));
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country cannot be null or empty.", nameof(country));

            City = city;
            State = state;
            Country = country;
        }


    }
}