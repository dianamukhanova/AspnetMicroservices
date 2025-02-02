﻿using System;
using System.Net;
using Cart.API.Entities;
using Cart.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Controllers
{
	[ApiController]
	[Route("api/v1/controller")]
	public class BasketController: ControllerBase
	{

		private readonly IBasketRepository _repository;

		public BasketController(IBasketRepository repository)
        {
			_repository = repository;
        }

		[HttpGet("{username}", Name ="GetBasket")]
		[ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<ShoppingCart>> GetBasket(string username)
        {
			var basket = await _repository.GetBasket(username);
			return Ok(basket ?? new ShoppingCart(username));

        }

		[HttpPost]
		[ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
			return Ok(await _repository.UpdateBasket(basket));

        }
		[HttpDelete("{username}", Name ="DeleteBasket")]
		[ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> DeleteBasket(string username)
        {
			await _repository.DeleteBasket(username);
			return Ok();
        }
		
	}
}

