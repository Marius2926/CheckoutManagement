using CkeckoutManagement.Core.BasketAggregate;
using CkeckoutManagement.Core.ValueObjects;
using System;
using Xunit;

namespace UnitTests.Core.BasketTests
{
    public class Basket_Constructor
    {
        private readonly Guid _basketId = Guid.Parse("4a17e702-c20e-4b87-b95b-f915c5a794f7");
        private readonly int _customerId = 1;
        private readonly BasketStatus _basketStatus = new(false, false);
        private readonly BasketValue _basketValue = new(0, 0, false);

        [Fact]
        public void CreateConstructor()
        {
            var basket = new Basket(_basketId, _customerId);

            Assert.Equal(_basketId, basket.Id);
            Assert.Equal(_customerId, basket.CustomerId);
        }

        [Fact]
        public void CreateConstructorWithStatusAndValue()
        {
            var basket = new Basket(_basketId, _customerId, _basketStatus, _basketValue);

            Assert.Equal(_basketId, basket.Id);
            Assert.Equal(_customerId, basket.CustomerId);
            Assert.Equal(_basketStatus, basket.Status);
            Assert.Equal(_basketValue, basket.Value);
        }
    }
}

