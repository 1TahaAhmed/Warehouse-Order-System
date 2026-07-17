using Microsoft.EntityFrameworkCore;
using Warehouse___Order_System.Models;

namespace Warehouse___Order_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var Context = new ApplicationDbContext();

            Context.Database.EnsureDeleted(); 
            Context.Database.EnsureCreated();

            Console.WriteLine("🔄 Checking database for existing customer...");

         
            var customer = Context.Customers
                .Include(c => c.profile)
                .FirstOrDefault(c => c.Email == "taha@senior.com");

            if (customer == null)
            {
                customer = new Customer
                {
                    FirstName = "Taha",
                    LastName = "Ahmed",
                    Email = "taha@senior.com",
                    profile = new CustomerProfile
                    {
                        Address = "شبين الكوم، المنوفية",
                        PhoneNumber = "01000000000"
                    }
                };
                Context.Customers.Add(customer);
                Context.SaveChanges(); 
                Console.WriteLine("✔️ New customer added successfully!");
            }
            else
            {
                Console.WriteLine($"ℹ️ Customer already exists (ID: {customer.Id}). Using existing record.");
            }

            var physicalProduct = new PhysicalProduct
            {
                ProductName = "Keyboard Mechanical Keychron K2",
                ProductPrice = 3500,
                Weight = 1.2,
                ShippingCost = 50
            };
            physicalProduct.Restock(physicalProduct, 15);

            var digitalProduct = new Digital
            {
                ProductName = "Mastering EF Core Course",
                ProductPrice = 500,
                Size = 2400,
                URL = "https://taha-courses.com/files/ef-pro.zip"
            };
            digitalProduct.Restock(digitalProduct, 100);

            var order = new Order
            {
                Id = customer.Id,
                OrderDate = DateTime.UtcNow,
                Status = Status.PENDING,
                TotalPrice = 7550,
                orderProduct = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        product = physicalProduct,
                        Quantity = 2,
                        UnitPrice = 3500
                    },
                    new OrderProduct
                    {
                        product = digitalProduct,
                        Quantity = 1,
                        UnitPrice = 500
                    }
                }
            };

            Context.Orders.Add(order);
            Context.SaveChanges();
            Console.WriteLine("✔️ Order and Products saved successfully to Database!");


            Console.WriteLine("\n=================== 🧪 STARTING SYSTEM TESTS ===================");

            Console.WriteLine("\n[TEST 1] Testing Memory Cache Lookup...");
            var cacheService = new ProductCache.ProductCacheService();

            var dbProducts = Context.Products.ToList();
            cacheService.Initialize(dbProducts);

            var cachedItem = cacheService.GetById(physicalProduct.ProductId);
            if (cachedItem != null)
            {
                Console.WriteLine($"✔️ SUCCESS: Found product in cache! Name: {cachedItem.ProductName} | Stock: {cachedItem.StockQuantity}");
            }
            else
            {
                Console.WriteLine("❌ FAILED: Product not found in cache.");
            }

            Console.WriteLine("\n[TEST 2] Testing Inventory Deduction...");
            int qtyToBuy = 2;
            bool isDeducted = cacheService.UpdateStock(physicalProduct.ProductId, qtyToBuy);
            if (isDeducted)
            {
                Console.WriteLine($"✔️ SUCCESS: Stock reduced by {qtyToBuy}. New stock in cache: {cacheService.GetById(physicalProduct.ProductId)?.StockQuantity}");
            }
            else
            {
                Console.WriteLine("❌ FAILED: Could not reduce stock (Insufficient stock or product not found).");
            }

            Console.WriteLine("\n[TEST 3] Testing Shipping Queue & Profile Resolution...");
            var dispatcher = new OrderDispatcher();

            var testOrder = Context.Orders
                .Include(o => o.Customer)
                .ThenInclude(c => c.profile)
                .Include(o => o.orderProduct)
                .FirstOrDefault(o => o.Id == order.Id);

            if (testOrder != null)
            {
                dispatcher.AddOrderToQueue(testOrder);
                Console.WriteLine("✔️ SUCCESS: Order added to Shipping Queue.");

                Console.WriteLine("\n--- Dispatching Next Order ---");
                dispatcher.ProcessNextOrder();
                Console.WriteLine($"📦 Shipping Details Resolved:");
                Console.WriteLine($"   Customer: {testOrder.Customer.FirstName} {testOrder.Customer.LastName}");
                Console.WriteLine($"   Address : {testOrder.Customer.profile?.Address}");
                Console.WriteLine($"   Phone   : {testOrder.Customer.profile?.PhoneNumber}");
            }
            else
            {
                Console.WriteLine("❌ FAILED: Test order not found in Database.");
            }

            Console.WriteLine("\n=================== 🧪 TESTS COMPLETED ===================");
        }
    }
}
