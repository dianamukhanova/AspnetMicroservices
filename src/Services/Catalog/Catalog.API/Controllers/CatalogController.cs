using System;
using System.Net;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
	//presentation layer

	[ApiController]
	[Route("api/v1/controller")]
	public class CatalogController: ControllerBase
	{
		private readonly IProductRepository _repository;
		private readonly Microsoft.Extensions.Logging.ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
			var products = await _repository.GetProducts();
			return Ok(products);
        }

		[HttpGet("{id:length(24)",Name ="GetProduct")]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]

		public async Task<ActionResult<IEnumerable<Product>>> GetProductById(string id)
		{
			var product = await _repository.GetProduct(id);
			if (product==null)
            {
				_logger.LogError($"Product with id {id} not found");
				return NotFound();
            }
			return Ok(product);
		}

		[Route("[action]/{category}", Name ="GetProductByCategory")]
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
		{
			var products = await _repository.GetProductByCategory(category);
			return Ok(products);
		}

	}
}

