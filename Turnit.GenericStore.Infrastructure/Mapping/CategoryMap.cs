using FluentNHibernate.Mapping;
using Turnit.GenericStore.Domain.Entities;

namespace Turnit.GenericStore.Infrastructure.Mapping
{

public class CategoryMap : ClassMap<Category>
{
    public CategoryMap()
    {
        Schema("public");
        Table("category");

        Id(x => x.Id, "id");
        Map(x => x.Name, "name");
    }
}
}