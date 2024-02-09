using FluentNHibernate.Mapping;
using Turnit.GenericStore.Domain.Entities;

namespace Turnit.GenericStore.Infrastructure.Mapping
{
	public class StoreMap : ClassMap<Store>
	{
		public StoreMap()
		{
			Schema("public");
			Table("store");

			Id(x => x.Id, "id");
			Map(x => x.Name, "name");
		}
	}
}