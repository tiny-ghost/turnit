using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Turnit.GenericStore.Domain.Entity;

namespace Turnit.GenericStore.Domain.Interface.Service
{
	public interface ICategoryService
	{
		Task<IList<Category>> GetAllCategories();
		Task AddProductToCategory(Guid productId, Guid categoryId);
		Task RemoveProductFromCategory(Guid productId, Guid categoryId);
	}
}