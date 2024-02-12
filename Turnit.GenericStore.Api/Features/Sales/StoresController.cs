using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Turnit.GenericStore.Domain.Interface.Service;
using Turnit.GenericStore.Domain.Models.Product.Request;

namespace Turnit.GenericStore.Api.Features.Sales;

[Route("[controller]")]
public class StoresController : ApiControllerBase
{
	private readonly IStoreService _storeService;
	public StoresController(IStoreService storeService)
	{
		_storeService = storeService;
	}
	
	//probably should be put here as we are updating existing record. 
	[HttpPost]
	[Route("{productId:guid}/book")]
	public async Task<ActionResult> BookProduct(Guid productId, [FromBody] ProductBookingRequestModel request)
	{
		try
		{
			await _storeService.BookProduct(productId, request.StoreId, request.Quantity);
		}
		catch (ApplicationException e)
		{
			return BadRequest(e.Message);
		}

		return NoContent();
	}
	
	//probably should be put here as we are updating existing record.
	[HttpPost]
	[Route("{storeId:guid}/stock")]
	
	public async Task<ActionResult> RestokProduct(Guid storeId, [FromBody] ProductStockRequestModel request)
	{
		try
		{
			await _storeService.RestockProduct(storeId, request.ProductId, request.Quantity);
		}
		catch (ApplicationException e)
		{
			return BadRequest(e.Message);
		}

		return NoContent();
	}
	
}