using PropertyService.Domain.Exceptions;
using System;

namespace PropertyService.Domain.ValueObjects
{
    public sealed record Details
    {
        public int Bedrooms { get; init; }
        public int Bathrooms { get; init; }
        public int SquareFootage { get; init; }
        public int YearBuilt { get; init; }

        public Details(int bedrooms, int bathrooms, int squareFootage, int yearBuilt)
        {
            if (bedrooms < 0) throw PropertyDomainException.InvalidNegative(nameof(Bedrooms));
            if (bathrooms < 0) throw PropertyDomainException.InvalidNegative(nameof(Bathrooms));
            if (squareFootage < 0) throw PropertyDomainException.InvalidNegative(nameof(SquareFootage));
            if (yearBuilt < 1800 || yearBuilt > DateTime.UtcNow.Year) throw PropertyDomainException.InvalidYearBuilt();

            Bedrooms = bedrooms;
            Bathrooms = bathrooms;
            SquareFootage = squareFootage;
            YearBuilt = yearBuilt;
        }
    }
}