using System;

namespace Turnit.GenericStore.Domain.Entity
{
	public class ProductCategory
	{
		public virtual Guid Id { get; set; }

		public virtual Product Product { get; set; }

		public virtual Category Category { get; set; }
	}
}