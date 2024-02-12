using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using Turnit.GenericStore.Domain.Entity;
using Turnit.GenericStore.Domain.Interface.Service;
using Turnit.GenericStore.Domain.Models.Product;

namespace Turnit.GenericStore.Application.Service
{
	public class ProductService : IProductService
	{
		//Ideally this should be a repository, but for the sake of simplicity I will use session directly
		private readonly ISession _session;

		public ProductService(ISession session)
		{
			_session = session;
		}

		public async Task<IEnumerable<ProductCategoryModel>> GetAllProducts()
		{
			var categorizedProducts = await _session.QueryOver<ProductCategory>().ListAsync();

			var productAvailability = await GetProductsAvailability();

			var availabilityLookUp = productAvailability.ToLookup(x => x.Product.Id);

			var result = new List<ProductCategoryModel>();

			foreach (var product in categorizedProducts)
			{
				var productCategoryModel = new ProductCategoryModel
				{
					CategoryId = product.Category.Id
				};

				productCategoryModel.Products.Add(new ProductModel
				{
					Id = product.Product.Id,
					Name = product.Product.Name,
					Availability = availabilityLookUp[product.Product.Id].Select(x => new AvailabilityModel
					{
						StoreId = x.Store.Id,
						Availability = x.Availability
					}).ToArray()
				});

				result.Add(productCategoryModel);
			}

			var uncategorizedProducts = productAvailability.Select(x => x.Product)
				.Except(categorizedProducts.Select(x => x.Product)).ToList();
			result.Add(new ProductCategoryModel
			{
				Products = uncategorizedProducts
					.Select(x => new ProductModel
					{
						Id = x.Id,
						Name = x.Name,
						Availability = availabilityLookUp[x.Id].Select(y => new AvailabilityModel
						{
							StoreId = y.Store.Id,
							Availability = y.Availability
						}).ToList()
					}).ToList()
			});

			return result;
		}

		public async Task<IEnumerable<ProductModel>> GetProductsByCategory(Guid categoryId)
		{
			var productCategory = await _session.QueryOver<ProductCategory>()
				.Where(x => x.Category.Id == categoryId)
				.ListAsync();

			if (productCategory.Count == 0)
			{
				return Array.Empty<ProductModel>();
			}

			var availability = await GetProductsAvailability();

			var availabilityLookUp = availability.ToLookup(x => x.Product.Id);

			return productCategory.Select(x => new ProductModel
			{
				Id = x.Id,
				Name = x.Product.Name,
				Availability = availabilityLookUp[x.Product.Id].Select(y => new AvailabilityModel
				{
					StoreId = y.Store.Id,
					StoreName = y.Store.Name,
					Availability = y.Availability
				}).ToList()
			}).ToList();
		}

		private async Task<IList<ProductAvailability>> GetProductsAvailability()
		{
			return await _session.QueryOver<ProductAvailability>()
				.ListAsync();
		}
	}
}