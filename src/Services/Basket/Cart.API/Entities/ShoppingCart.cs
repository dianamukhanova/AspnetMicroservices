﻿using System;
namespace Cart.API.Entities
{
	public class ShoppingCart
	{
		public string UserName { get; set; }
		public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

		public ShoppingCart()
        {

        }

		public ShoppingCart(string username)
		{
			UserName = username;
		}

		public decimal TotalPrice
        {
			get
            {
				decimal totalPrice = 0;
				foreach (var item in Items)
                {
					totalPrice += item.Price * item.Quantity;
                }
				return totalPrice;
            }
        }
	}
}

