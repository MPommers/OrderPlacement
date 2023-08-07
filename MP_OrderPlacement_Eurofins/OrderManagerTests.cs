using System;
using NUnit.Framework;

namespace MP_OrderPlacement_Eurofins.Tests
{
    public class OrderPricerTests
    {
        [Test]
        public void CalculateOrderPrice_NoDiscount() // No discount
        {
            KitVariant kitVariant = new KitVariant { Name = "Standard", BasePrice = 98.99 };
            Order order = new Order(1, 5, DateTime.Now.AddDays(7), kitVariant);

            double orderPrice = OrderPricer.CalculateOrderPrice(order);

            Assert.AreEqual(98.99 * 5, orderPrice, 0.01);
        }

        [Test]
        public void CalculateOrderPrice_MaximumDiscountApplied() // 15% discount
        {
            KitVariant kitVariant = new KitVariant { Name = "Standard", BasePrice = 98.99 };
            Order order = new Order(1, 100, DateTime.Now.AddDays(7), kitVariant);

            double orderPrice = OrderPricer.CalculateOrderPrice(order);

            double expectedOrderPrice = 98.99 * 100 * 0.85; 
            Assert.AreEqual(expectedOrderPrice, orderPrice, 0.01);
        }

        [Test]
        public void CalculateOrderPrice_ExactDiscountAmount_15PercentDiscount() // 15% discount range starting
        {
            KitVariant kitVariant = new KitVariant { Name = "Standard", BasePrice = 98.99 };
            Order order = new Order(1, 50, DateTime.Now.AddDays(7), kitVariant);

            double orderPrice = OrderPricer.CalculateOrderPrice(order);

            double expectedOrderPrice = 98.99 * 50 * 0.85;
            Assert.AreEqual(expectedOrderPrice, orderPrice, 0.01);
        }

        [Test]
        public void CalculateOrderPrice_MinimumDiscountApplied() // 5% discount
        {
            KitVariant kitVariant = new KitVariant { Name = "Standard", BasePrice = 98.99 };
            Order order = new Order(1, 14, DateTime.Now.AddDays(7), kitVariant);

            double orderPrice = OrderPricer.CalculateOrderPrice(order);

            double expectedOrderPrice = 98.99 * 14 * 0.95;
            Assert.AreEqual(expectedOrderPrice, orderPrice, 0.01);
        }

        [Test]
        public void CalculateOrderPrice_ExactDiscountAmount_5PercentDiscount() // 5% discount range starting
        {
            KitVariant kitVariant = new KitVariant { Name = "Standard", BasePrice = 98.99 };
            Order order = new Order(1, 10, DateTime.Now.AddDays(7), kitVariant);

            double orderPrice = OrderPricer.CalculateOrderPrice(order);

            double expectedOrderPrice = 98.99 * 10 * 0.95;
            Assert.AreEqual(expectedOrderPrice, orderPrice, 0.01);
        }

        [Test]
        public void CalculateOrderPrice_DesiredAmountBelowMinimum() // 0 desired amount value exception
        {
            KitVariant kitVariant = new KitVariant { Name = "Standard", BasePrice = 98.99 };
            Order order = new Order(1, 0, DateTime.Now.AddDays(7), kitVariant);

            Assert.Throws<InvalidOperationException>(() =>
            {
                OrderPricer.CalculateOrderPrice(order);
            });
        }

        [Test]
        public void CalculateOrderPrice_DesiredAmountAboveMaximum() // Out of range for desired amount
        {
            KitVariant kitVariant = new KitVariant { Name = "Standard", BasePrice = 98.99 };
            Order order = new Order(1, 1000, DateTime.Now.AddDays(7), kitVariant);

            Assert.Throws<InvalidOperationException>(() =>
            {
                OrderPricer.CalculateOrderPrice(order);
            });
        }

        [Test]
        public void CalculateOrderPrice_DeliveryDateInPast() // Past delivery date
        {
            KitVariant kitVariant = new KitVariant { Name = "Standard", BasePrice = 98.99 };
            OrderManager orderManager = new OrderManager(); // Create an instance of OrderManager

            Assert.Throws<InvalidOperationException>(() =>
            {
                orderManager.PlaceOrder(1, 10, DateTime.Now.AddDays(-1), kitVariant); // Try to place an order with a past delivery date
            });
        }

    }
}
