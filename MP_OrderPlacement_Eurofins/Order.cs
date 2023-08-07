using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_OrderPlacement_Eurofins
{
    public class Order
    {
        public int CustomerId { get; set; }
        public double DesiredAmount { get; set; }
        public double OrderPrice { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
        public KitVariant KitVariant { get; set; }

        public Order(int customerId, double desiredAmount, DateTime expectedDeliveryDate, KitVariant kitVariant)
        {
            CustomerId = customerId;
            DesiredAmount = desiredAmount;
            ExpectedDeliveryDate = expectedDeliveryDate;
            KitVariant = kitVariant;
        }
    }
}
