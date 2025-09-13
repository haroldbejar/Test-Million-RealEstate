using PropertyService.Domain.Exceptions;
using PropertyService.Domain.ValueObjects;
using Xunit;

namespace PropertyService.Test.UseCases.Domain.ValueObjects
{
    public class PriceTests
    {
        [Fact]
        public void Create_Price_With_Valid_Data_Should_Succeed()
        {
            // Arrange
            var amount = 100000;
            var currency = "USD";

            // Act
            var price = new Price(amount, currency);

            // Assert
            Assert.NotNull(price);
            Assert.Equal(amount, price.Amount);
            Assert.Equal(currency, price.Currency);
        }

        [Theory]
        [InlineData(-1, "USD")]
        [InlineData(-0.01, "EUR")]
        public void Create_Price_With_Negative_Amount_Should_Throw_PropertyDomainException(decimal invalidAmount, string currency)
        {
            // Act & Assert
            var exception = Assert.Throws<PropertyDomainException>(() => new Price(invalidAmount, currency));
            Assert.Equal("INVALID_NEGATIVE_VALUE", exception.Code);
        }

        [Theory]
        [InlineData(100, null)]
        [InlineData(100, "")]
        [InlineData(100, "   ")]
        public void Create_Price_With_Invalid_Currency_Should_Throw_PropertyDomainException(decimal amount, string invalidCurrency)
        {
            // Act & Assert
            var exception = Assert.Throws<PropertyDomainException>(() => new Price(amount, invalidCurrency));
            Assert.Equal("INVALID_FIELD", exception.Code);
        }
    }
}
