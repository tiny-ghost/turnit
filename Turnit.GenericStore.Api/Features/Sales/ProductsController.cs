using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Turnit.GenericStore.Domain.Interface.Service;
using Turnit.GenericStore.Domain.Models.Product;

namespace Turnit.GenericStore.Api.Features.Sales;

[Route("[controller]")]
public class ProductsController : ApiControllerBase
{
	private readonly IProductService _productService;

	public ProductsController(IProductService productService)
	{
		_productService = productService;
	}


	[HttpGet]
	[Route("by-category/{categoryId:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<List<ProductModel>>> ProductsByCategory(Guid categoryId)
	{
		var result = await _productService.GetProductsByCategory(categoryId);

		if (!result.Any())
		{
			return NotFound();
		}
		
		return Ok(result);
	}


	[HttpGet]
	[Route("")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<List<ProductCategoryModel>>> AllProducts()
	{
		var result = await _productService.GetAllProducts();

		if (!result.Any())
		{
			return NotFound();
		}

		return Ok(result);
	}
	
}