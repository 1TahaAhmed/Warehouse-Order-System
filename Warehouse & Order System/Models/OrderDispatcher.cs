using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Warehouse___Order_System.Models
{
    public class OrderDispatcher
    {
        // 👈 تأكد من إضافة = new Queue<Order>() هنا
        private readonly Queue<Order> _queue = new Queue<Order>();

        public void AddOrderToQueue(Order order)
        {
            _queue.Enqueue(order); // ✔️ دلوقتي هيشتغل بدون أي مشاكل
        }

        public void ProcessNextOrder()
        {
            if (_queue.Count > 0)
            {
                var order = _queue.Dequeue();
                Console.WriteLine($"🚚 Dispatching order {order.Id}...");
            }
            else
            {
                Console.WriteLine("⚠️ No orders in queue to dispatch.");
            }
        }
    }
}
