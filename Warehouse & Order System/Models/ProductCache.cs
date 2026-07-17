using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse___Order_System.Models
{
    public class ProductCache
    {
        public interface IProductCacheService
        {
            void Initialize(IEnumerable<Product> products);
            Product? GetById(int productId);
            bool UpdateStock(int productId, int quantityToSubtract);
        }

        public class ProductCacheService : IProductCacheService
        {
            private readonly Dictionary<int, Product> _productCache = new();
            private readonly object _lock = new object();
            public void Initialize(IEnumerable<Product> products)
            {
                lock (_lock)
                {
                    _productCache.Clear();
                    foreach (var product in products)
                    {
                        if (product.ProductId > 0)
                        {
                            _productCache[product.ProductId] = product;
                        }
                    }
                }
            }
            public Product? GetById(int productId)
            {
                lock (_lock)
                {
                    return _productCache.TryGetValue(productId, out var product) ? product : null;
                }
            }
            public bool UpdateStock(int productId, int quantityToSubtract)
            {
                lock (_lock)
                {
                    if (!_productCache.TryGetValue(productId, out var product))
                        return false;

                    if (product.StockQuantity < quantityToSubtract)
                        return false;

                    product.StockQuantity -= quantityToSubtract;
                    return true;
                }
            }
        }
    }
}
