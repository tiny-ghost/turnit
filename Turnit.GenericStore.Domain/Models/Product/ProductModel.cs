using System;
using System.Collections.Generic;

namespace Turnit.GenericStore.Domain.Models.Product
{
	public class ProductModel
	{
		public Guid Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public IList<AvailabilityModel> Availability { get; set; } = new List<AvailabilityModel>();

	}
}