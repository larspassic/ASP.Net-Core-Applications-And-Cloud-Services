using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Ziggle.Repository
{
    public interface IShoppingCartRepository
    {
        ShoppingCartModel Add(int userId, int ProductId, int quantity);
        bool Remove(int userId, int productId);
        ShoppingCartModel[] GetAll(int userId);
    }

    public class ShoppingCartModel
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }






    public class ShoppingCartRepository : IShoppingCartRepository
    {
        public ShoppingCartModel Add(int userId, int productId, int quantity)
        {
            var item = DatabaseAccessor.Instance.ShoppingCartItem.Add(
                new Ziggle.ProductDatabase.ShoppingCartItem
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = quantity
                });

            DatabaseAccessor.Instance.SaveChanges();

            return new ShoppingCartModel
            {
                UserId = item.Entity.UserId,
                ProductId = item.Entity.ProductId,
                Quantity = item.Entity.Quantity
            };
            
        }

        public ShoppingCartModel[] GetAll(int userId)
        {
            var items = DatabaseAccessor.Instance.ShoppingCartItem.Where(t => t.UserId == userId).Select(t => new ShoppingCartModel
            {
                UserId = t.UserId,
                ProductId = t.ProductId,
                Quantity = t.Quantity
            }).ToArray();

            return items;
        }

        //Pass in the user and the product that you want to remove
        public bool Remove(int userId, int productId)
        {
            
            //Do a query for the cart item where the userid and the productid match
            var items = DatabaseAccessor.Instance.ShoppingCartItem.Where(t => t.UserId == userId && t.ProductId == productId);

            if (items.Count() == 0)
            {
                return false;

            }

            //Use entity framework remove method?
            DatabaseAccessor.Instance.ShoppingCartItem.Remove(items.First());

            DatabaseAccessor.Instance.SaveChanges();

            //Happy path so return true
            return true;
        }
    }


}
