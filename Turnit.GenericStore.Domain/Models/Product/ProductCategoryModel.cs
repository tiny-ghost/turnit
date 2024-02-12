using System;
using System.Collections.Generic;

namespace Turnit.GenericStore.Domain.Models.Product
{
public class ProductCategoryModel
{
    public Guid? CategoryId { get; set; }

    public IList<ProductModel> Products { get; set; } = new List<ProductModel>();
}
}