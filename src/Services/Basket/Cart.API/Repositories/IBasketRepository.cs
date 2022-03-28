using System;
using Cart.API.Entities;

namespace Cart.API.Repositories
{
	public interface IBasketRepository
	{
		Task<ShoppingCart> GetBasket(string user);
		Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
		Task DeleteBasket(string username);
	}
}

