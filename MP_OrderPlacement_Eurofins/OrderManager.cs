using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_OrderPlacement_Eurofins
{
    public class OrderManager
    {
        private List<Order> orders = new List<Order>();


        public void PlaceOrder(int customerId, double desiredAmount, DateTime expectedDeliveryDate, KitVariant kitVariant)
        {
            if (expectedDeliveryDate <= DateTime.Now)
            {
                throw new InvalidOperationException("Delivery date must be in the future.");
            }

            if (desiredAmount <= 0 || desiredAmount > 999 || desiredAmount % 1 != 0)
            {
                throw new InvalidOperationException("Desired amount must be a positive round number between 1 and 999.");
            }

            var order = new Order(customerId, desiredAmount, expectedDeliveryDate, kitVariant);
            order.OrderPrice = OrderPricer.CalculateOrderPrice(order);

            orders.Add(order);
        }

        public List<Order> GetCustomerOrders(int customerId)
        {
            return orders.FindAll(order => order.CustomerId == customerId);
        }
    }
}
