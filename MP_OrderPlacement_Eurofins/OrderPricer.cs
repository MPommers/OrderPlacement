using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_OrderPlacement_Eurofins
{
    public class OrderPricer
    {
        public static double CalculateOrderPrice(Order order)
        {
            if (order.DesiredAmount < 1 || order.DesiredAmount > 999 || order.DesiredAmount % 1 != 0)
            {
                throw new InvalidOperationException("Desired amount must be at least 1. Value has to be round number");
            } 

            double orderPrice = order.KitVariant.BasePrice * order.DesiredAmount;

            if (order.DesiredAmount >= 50)
            {
                orderPrice *= 0.85; // 15% discount
            }
            else if (order.DesiredAmount >= 10)
            {
                orderPrice *= 0.95; // 5% discount
            }

            return orderPrice;
        }
    }
}
