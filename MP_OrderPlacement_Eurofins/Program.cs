using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_OrderPlacement_Eurofins
{
    internal class Program
    {
        private static readonly OrderManager orderManager = new OrderManager();
        private static readonly KitVariant standardKit = new KitVariant { Name = "Standard", BasePrice = 98.99 };

        static void Main(string[] args)
        {
            UserInterface ui = new UserInterface(orderManager, standardKit);
            ui.Run();
        }
    }
}
