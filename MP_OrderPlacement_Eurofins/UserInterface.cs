using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP_OrderPlacement_Eurofins
{
    public class UserInterface
    {
        private readonly OrderManager orderManager;
        private readonly KitVariant standardKit;

        public UserInterface(OrderManager orderManager, KitVariant standardKit)
        {
            this.orderManager = orderManager;
            this.standardKit = standardKit;
        }

        public void Run()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("1. Place an order");
                Console.WriteLine("2. List customer orders");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        PlaceOrder();
                        break;

                    case "2":
                        ListCustomerOrders();
                        break;

                    case "3":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please select a valid option.");
                        break;
                }

                Console.WriteLine();
            }
        }

        private void PlaceOrder()
        {
            Console.Write("Enter customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int customerId))
            {
                double desiredAmount = 0;

                while (true)
                {
                    Console.Write("Enter desired amount: ");
                    if (double.TryParse(Console.ReadLine(), out desiredAmount))
                    {
                        if (desiredAmount >= 1 && desiredAmount <= 999 && desiredAmount % 1 == 0)
                        {
                            break; // Valid input, exit the loop
                        }
                        else
                        {
                            Console.WriteLine("Value must be a round number between 1 and 999.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input format. Please enter a valid number.");
                    }
                }

                Console.Write("Enter expected delivery date (YYYY-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime expectedDeliveryDate))
                {
                    try
                    {
                        orderManager.PlaceOrder(customerId, desiredAmount, expectedDeliveryDate, standardKit);
                        Console.WriteLine("Order placed successfully.");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine($"Error placing order: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format.");
                }
            }
            else
            {
                Console.WriteLine("Invalid customer ID format.");
            }
        }

        private void ListCustomerOrders()
        {
            Console.Write("Enter customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int listCustomerId))
            {
                List<Order> customerOrders = orderManager.GetCustomerOrders(listCustomerId);
                if (customerOrders.Count > 0)
                {
                    Console.WriteLine($"Customer {listCustomerId} Orders:");
                    foreach (Order order in customerOrders)
                    {
                        Console.WriteLine();
                        Console.WriteLine("----------");
                        Console.WriteLine($"Customer ID: {order.CustomerId}");
                        Console.WriteLine($"Desired Amount: {order.DesiredAmount}");
                        Console.WriteLine($"Order Price: {order.OrderPrice}");
                        Console.WriteLine($"Expected Delivery Date: {order.ExpectedDeliveryDate}");
                        Console.WriteLine($"Kit Variant: {order.KitVariant.Name}");
                        Console.WriteLine("----------");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"No orders found for customer {listCustomerId}.");
                }
            }
            else
            {
                Console.WriteLine("Invalid customer ID format.");
            }
        }
    }
}
