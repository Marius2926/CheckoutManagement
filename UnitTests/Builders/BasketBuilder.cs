using CkeckoutManagement.Core.BasketAggregate;
using CkeckoutManagement.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builders
{
    public class BasketBuilder
    {
        public const int TEST_CUSTOMER_ID = 1;
        public static readonly BasketStatus TEST_BASKET_STATUS = new(false, false);
        public static readonly BasketValue TEST_BASKET_VALUE = new(0, 0, false);

        private Guid _id = Guid.NewGuid();
        private int _customerId;
        private BasketStatus _status;
        private BasketValue _value;

        public BasketBuilder()
        {
        }

        public BasketBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public BasketBuilder WithDefaultValues()
        {
            _id = Guid.NewGuid();
            _customerId = TEST_CUSTOMER_ID;
            _status = TEST_BASKET_STATUS;
            _value = TEST_BASKET_VALUE;
            return this;
        }

        public Basket Build()
        {
            return new Basket(_id, _customerId, _status, _value);
        }
    }
}
