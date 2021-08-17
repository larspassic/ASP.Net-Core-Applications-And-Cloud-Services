using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Ziggle.Repository;

namespace Ziggle.Business
{
    public interface IShoppingCartManager
    {
        ShoppingCartModel Add(int userId, int productId, int quantity);
        bool Remove(int userId, int productId);
        ShoppingCartModel[] GetAll(int userId);
    }

    public class ShoppingCartModel
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }


    }
    
    
    public class ShoppingCartManager : IShoppingCartManager
    {
        private readonly IShoppingCartRepository shoppingCartRepository;

        //This might be the default constructor
        public ShoppingCartManager(IShoppingCartRepository shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;

        }

        public ShoppingCartModel Add(int userId, int productId, int quantity)
        {
            var item = shoppingCartRepository.Add(userId, productId, quantity);

            return new ShoppingCartModel
            {
                ProductId = item.ProductId,
                UserId = item.UserId,
                Quantity = item.Quantity
            };
        }

        public ShoppingCartModel[] GetAll(int userId)
        {
            var items = shoppingCartRepository.GetAll(userId)
                .Select(t => new ShoppingCartModel
                {
                    UserId = t.UserId,
                    ProductId = t.ProductId,
                    Quantity = t.Quantity
                }).ToArray();

            return items;
        }

        public bool Remove(int userId, int productId)
        {
            return shoppingCartRepository.Remove(userId, productId);
        }
    }
}
