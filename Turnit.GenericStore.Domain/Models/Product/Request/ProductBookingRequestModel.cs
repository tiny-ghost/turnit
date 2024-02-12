using System;

namespace Turnit.GenericStore.Domain.Models.Product.Request
{
	public class ProductBookingRequestModel
	{
		public int Quantity { get; set; }
		public Guid StoreId { get; set; }
	}
}