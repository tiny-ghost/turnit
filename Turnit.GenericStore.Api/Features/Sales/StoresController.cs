using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using Turnit.GenericStore.Api.Domain.Entities;

namespace Turnit.GenericStore.Api.Features.Sales;

[Route("[controller]")]
public class StoresController : ApiControllerBase
{
	private readonly ISession _session;

	public StoresController(ISession session)
	{
		_session = session;
	}

	[HttpPost]
	[Route("{productId:guid}/book")]
	public async Task<IActionResult> BookProduct(Guid productId, [FromBody] ProductBookingRequestModel request)
	{
		var product = await _session.GetAsync<Product>(productId);

		if (product == null)
		{
			return NotFound();
		}

		var availability = await _session.QueryOver<ProductAvailability>()
			.Where(x => x.Product.Id == productId)
			.And(x => x.Store.Id == request.StoreId)
			.SingleOrDefaultAsync();

		if (availability == null)
		{
			return NotFound();
		}

		if (availability.Availability < request.Quantity)
		{
			return BadRequest();
		}

		availability.Availability -= request.Quantity;
		await _session.UpdateAsync(availability);
		await _session.FlushAsync();

		return Ok();
	}
}