using System;

namespace Turnit.GenericStore.Domain.Entity
{
	public class Store
	{
		public virtual Guid Id { get; set; }

		public virtual string Name { get; set; } = string.Empty;
	}
}