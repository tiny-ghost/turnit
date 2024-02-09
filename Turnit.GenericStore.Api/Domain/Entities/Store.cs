using System;
using FluentNHibernate.Mapping;

namespace Turnit.GenericStore.Api.Domain.Entities;

public class Store
{
    public virtual Guid Id { get; set; }

    public virtual string Name { get; set; }
}

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