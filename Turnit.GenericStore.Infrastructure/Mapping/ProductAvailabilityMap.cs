using FluentNHibernate.Mapping;
using Turnit.GenericStore.Domain.Entity;

namespace Turnit.GenericStore.Infrastructure.Mapping
{
	public class ProductAvailabilityMap : ClassMap<ProductAvailability>
	{
		public ProductAvailabilityMap()
		{
			Schema("public");
			Table("product_availability");

			Id(x => x.Id, "id");
			Map(x => x.Availability, "availability");
			References(x => x.Store, "store_id").Fetch.Join();
			References(x => x.Product, "product_id").Fetch.Join();
		}
	}
}