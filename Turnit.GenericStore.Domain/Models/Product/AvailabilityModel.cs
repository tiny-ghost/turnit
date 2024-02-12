using System;
using Turnit.GenericStore.Domain.Entity;

namespace Turnit.GenericStore.Domain.Models.Product
{
		public class AvailabilityModel
		{
			public Guid StoreId { get; set; }
			public string StoreName { get; set; }

			public int Availability { get; set; }
		}
}