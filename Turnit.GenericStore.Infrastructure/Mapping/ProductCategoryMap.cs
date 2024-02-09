using FluentNHibernate.Mapping;
using Turnit.GenericStore.Domain.Entities;

namespace Turnit.GenericStore.Infrastructure.Mapping
{
	public class ProductCategoryMap : ClassMap<ProductCategory>
	{
		public ProductCategoryMap()
		{
			Schema("public");
			Table("product_category");

			Id(x => x.Id, "id");
			References(x => x.Category, "category_id");
			References(x => x.Product, "product_id");
		}
	}
}