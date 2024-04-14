using Application.Services;
using Domain.Entities;

namespace UnitTests
{
    public class PaymentTransactionValidation
    {
        [Fact]
        public void Valid()
        {
            //Arrange
            var service = new PaymentTransactionServiceValidation(default, default);
            var user = new User
            {
                Id = 2,
                Name = "Test",
                LastName = "TestLN"
            };
            var currency = new Currency
            {
                Code = "RSD"
            };
            int amount = 1000;

            //Act
            var result = service.ValidateRequest(user, currency, amount, out var response);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void InvalidUser()
        {
            var service = new PaymentTransactionServiceValidation(default, default);
            User user = default;

            var currency = new Currency
            {
                Code = "RSD"
            };
            int amount = 1000;

            //Act
            var result = service.ValidateRequest(user, currency, amount, out var response);

            //Assert
            Assert.False(result);
            Assert.Equal("Non-existing player", response.Description);
        }

        [Fact]
        public void InvalidCurrency()
        {
            var service = new PaymentTransactionServiceValidation(default, default);

            var user = new User
            {
                Id = 2,
                Name = "Test",
                LastName = "TestLN"
            };
            int amount = 1000;

            //Act
            var result = service.ValidateRequest(user, null, amount, out var response);

            //Assert
            Assert.False(result);
            Assert.Equal("Invalid Currency", response.Description);
        }

        [Fact]
        public void InvalidAmount()
        {
            var service = new PaymentTransactionServiceValidation(default, default);

            var user = new User
            {
                Id = 2,
                Name = "Test",
                LastName = "TestLN"
            };
            var currency = new Currency
            {
                Code = "RSD"
            };
            int amount = 0;

            //Act
            var result = service.ValidateRequest(user, currency, amount, out var response);

            //Assert
            Assert.False(result);
            Assert.Equal("Amount of money in transaction is not bigger than zero", response.Description);
        }
    }
}