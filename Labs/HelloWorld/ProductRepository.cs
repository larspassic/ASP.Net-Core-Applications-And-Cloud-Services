using System.Collections.Generic;
using System.Linq;

namespace HelloWorld
{
    public interface IProductRepository
    {
        IEnumerable<Models.XProduct> Products { get; }
    }

    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Models.XProduct> Products
        {
            get
            {
                IEnumerable<Models.XProduct> items;

                var database = new helloworldEntities();

                items = database.Products
                .Select(t => new Models.XProduct
                {
                    ProductId = t.ProductId,
                    Description = t.Description,
                    Name = t.Name,
                    Price = t.Price
                })
                .ToArray();

                return items;
            }
        }
    }
}