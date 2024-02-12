using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Turnit.GenericStore.Domain.Entity;
using Turnit.GenericStore.Domain.Models.Product;

namespace Turnit.GenericStore.Domain.Interface.Service
{
	public interface IProductService
	{
		Task<IEnumerable<ProductCategoryModel>> GetAllProducts();
		Task<IEnumerable<ProductModel>> GetProductsByCategory(Guid categoryId);
	}
}