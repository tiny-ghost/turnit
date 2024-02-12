using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NHibernate;
using Turnit.GenericStore.Domain.Entity;
using Turnit.GenericStore.Domain.Interface.Service;

namespace Turnit.GenericStore.Application.Service
{
	public class CategoryService : ICategoryService
	{
		private readonly ISession _session;

		public CategoryService(ISession session)
		{
			_session = session;
		}

		public async Task<IList<Category>> GetAllCategories()
		{
			return await _session.QueryOver<Category>().ListAsync();
		}
		
		//This should be probably Post as we actually creating new record and deleting new record.
		//will be used by Category controller
		public async Task AddProductToCategory(Guid productId,  Guid categoryId)
		{
			var product =  await _session.GetAsync<Product>(productId);
			var category = await _session.GetAsync<Category>(categoryId);
			
			if(product is null || category is null)
			{
				throw new ApplicationException("Product or category not found");
			}

			var existingProductCategory = await _session.QueryOver<ProductCategory>()
				.Where(x => x.Category.Id == categoryId)
				.And(x => x.Product.Id == productId)
				.RowCountAsync();
			
			if( existingProductCategory > 0)
			{
				throw new ApplicationException("Product already exists in the category");
			}
			
			var newProductCategory = new ProductCategory
			{
				Product = product,
				Category = category
			};
			
			await SaveAsync(newProductCategory);
		}

		//This should be probably Post as we actually creating new record and deleting new record.
		//will be used by Category controller
		public async Task RemoveProductFromCategory(Guid productId, Guid categoryId)
		{
			var existingProductCategory = await _session.QueryOver<ProductCategory>()
				.Where(x => x.Category.Id == categoryId)
				.And(x => x.Product.Id == productId)
				.ListAsync();
			if (existingProductCategory is null)
			{
				throw new ApplicationException("Product not found in the category");
			}

			await DeleteAsync(existingProductCategory);

		}

		private Task SaveAsync(object entity)
		{
			_session.SaveAsync(entity);
			return _session.FlushAsync();
			
		}
		
		private Task DeleteAsync(object entity)
		{
			_session.DeleteAsync(entity);
			return _session.FlushAsync();
			
		}
	}
}