using System;
using FluentNHibernate.Mapping;

namespace Turnit.GenericStore.Api.Domain.Entities;

public class Product
{
    public virtual Guid Id { get; set; }

    public virtual string Name { get; set; }

    public virtual string Description { get; set; }
}

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