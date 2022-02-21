using CkeckoutManagement.Core.BasketAggregate;
using CkeckoutManagement.Core.Events;
using CkeckoutManagement.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Core.BasketTests
{
    public class Basket_AddNewArticleLine
    {
        private readonly Guid _basketId = Guid.Parse("4a17e702-c20e-4b87-b95b-f915c5a794f7");
        private readonly int _customerId = 1;
        private readonly BasketStatus _basketStatus = new(false, false);
        private readonly BasketValue _basketValue = new(0, 0, false);

        [Fact]
        public void AddsAppointmentScheduledEvent()
        {
            var basket = new Basket(_basketId, _customerId, _basketStatus, _basketValue);


            var tomato = new ArticleLine(_basketId, "tomato", 20);
            basket.AddArticleLine(tomato);

            Assert.Single(basket.ArticleLines);
            Assert.Contains(basket.Events, x => x.GetType() == typeof(ArticleLineAddedEvent));

        }
    }
}
