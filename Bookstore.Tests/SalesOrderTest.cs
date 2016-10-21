using Bookstore.Common;
using Bookstore.SalesOrder;
using NUnit.Framework;

namespace Bookstore.Tests
{
    public class SalesOrderTest
    {
        [Test]
        public void totalCost_ofOrderWithOneItem_isPricePlusShipping()
        {
            var salesOrder = new SalesOrderBuilder().WithItemPriced(100).Build();

            var total = salesOrder.GetTotal();

            Assert.That(total, Is.EqualTo(115));
        }

        [Test]
        public void totalCost_ofOrderWithTwoItems_isSumOfPricesPlusShipping()
        {
            var salesOrder = new SalesOrderBuilder()
                .WithItemPriced(100)
                .WithItemPriced(100)
                .Build();

            var total = salesOrder.GetTotal();

            Assert.That(total, Is.EqualTo(215));
        }

        [Test]
        public void totalCost_ofBooksOnly_hasReducedShipping()
        {
            var salesOrder = new SalesOrderBuilder()
                .WithBookPriced(200)
                .Build();

            var total = salesOrder.GetTotal();

            Assert.That(total, Is.EqualTo(205));
        }

        [Test]
        public void totalCost_ofBooksOnlyWorthMoreThan200_hasFreeShipping()
        {
            var salesOrder = new SalesOrderBuilder()
                .WithBookPriced(100)
                .WithBookPriced(110)
                .Build();

            var total = salesOrder.GetTotal();

            Assert.That(total, Is.EqualTo(210));
        }

        [Test]
        public void totalCost_ofBooksAndOtherItemsWorthMoreThan200_hasNormalShipping()
        {
            var salesOrder = new SalesOrderBuilder()
                .WithItemPriced(100)
                .WithBookPriced(110)
                .Build();

            var total = salesOrder.GetTotal();

            Assert.That(total, Is.EqualTo(225));
        }

        [Test]
        public void totalCost_ofOrderShippedOutsidePoland_includesInternationalShippingCost()
        {
            var salesOrder = new SalesOrderBuilder()
                .DeliverTo(Country.Germany)
                .WithItemPriced(100)
                .Build();

            var total = salesOrder.GetTotal();

            Assert.That(total, Is.EqualTo(150));
        }

        [Test]
        public void totalCost_ofOrderShippedOutsidePolandEligibleForBookPromo_hasInternationalShippingCost()
        {
            var salesOrder = new SalesOrderBuilder()
                .DeliverTo(Country.Germany)
                .WithBookPriced(300)
                .Build();

            var total = salesOrder.GetTotal();

            Assert.That(total, Is.EqualTo(350));
        }

        [Test]
        public void totalCost_ofHeavyItemsOutsidePoland_hasIncreasedShippingCost()
        {
            var salesOrder = new SalesOrderBuilder()
                .DeliverTo(Country.Germany)
                .WithItem(new LineItem {Price = 300, Weight = 15.0})
                .Build();

            var total = salesOrder.GetTotal();

            Assert.That(total, Is.EqualTo(370));
        }

        private class SalesOrderBuilder
        {
            private readonly SalesOrder.SalesOrder salesOrder = new SalesOrder.SalesOrder();

            public SalesOrderBuilder()
            {
                salesOrder.DeliveryCountry = Country.Poland;
            }

            public SalesOrderBuilder WithItemPriced(int price)
            {
                return WithItem(new LineItem {Price = price, Type = ProductType.Calendar});
            }

            public SalesOrderBuilder WithBookPriced(int price)
            {
                return WithItem(new LineItem {Price = price, Type = ProductType.Book});
            }

            public SalesOrderBuilder DeliverTo(Country country)
            {
                salesOrder.DeliveryCountry = country;
                return this;
            }

            public SalesOrderBuilder WithItem(LineItem lineItem)
            {
                salesOrder.AddItem(lineItem);
                return this;
            }

            public SalesOrder.SalesOrder Build()
            {
                return salesOrder;
            }
        }
    }
}