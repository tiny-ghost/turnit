using System;
using System.Collections.Generic;

namespace Turnit.GenericStore.Domain.Entity
{
	public class Product
	{
		public virtual Guid Id { get; set; }

		public virtual string Name { get; set; }

		public virtual string Description { get; set; }
	}
}