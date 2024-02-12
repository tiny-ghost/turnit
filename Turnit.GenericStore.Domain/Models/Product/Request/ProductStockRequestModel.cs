using System;

namespace Turnit.GenericStore.Domain.Models.Product.Request
{
	public class ProductStockRequestModel
	{
		public Guid ProductId { get; set; }
		public int Quantity { get; set; }
	}
}