using FluentNHibernate.Mapping;
using Turnit.GenericStore.Domain.Entities;

namespace Turnit.GenericStore.Infrastructure.Mapping
{
	public class ProductMap : ClassMap<Product>
	{
		public ProductMap()
		{
			Schema("public");
			Table("product");

			Id(x => x.Id, "id");
			Map(x => x.Name, "name");
			Map(x => x.Description, "description");
		}
	}
}