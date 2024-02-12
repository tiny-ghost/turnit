using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using NHibernate;
using Turnit.GenericStore.Domain.Entity;
using Turnit.GenericStore.Domain.Interface.Service;

namespace Turnit.GenericStore.Application.Service
{
	public class StoreService : IStoreService
	{
	private readonly ISession _session;

	public StoreService(ISession session)
	{
		_session = session;
	}
		//This is not correct way to do it here. Normally, something like GenericStoreException will be created
		//and it will be rerouted to "/error" route when exception is thrown by this method. 
		//Also, app.UseExceptionHandler("/error") should be added to Startup.cs and all error handling will be done there
		public async Task BookProduct(Guid productId, Guid storeId, int quantity)
		{
			var product = await _session.GetAsync<Product>(productId);

			if (product is null)
			{
				throw new ApplicationException("Product not found");
			}

			var availability = await _session.QueryOver<ProductAvailability>()
				.Where(x => x.Product.Id == productId)
				.And(x => x.Store.Id == storeId)
				.SingleOrDefaultAsync();

			if (availability is null)
			{
				throw new ApplicationException("No product availability found for the store");
			}

			if (availability.Availability < quantity)
			{
				throw new ApplicationException("Amount exceeds availability");
			}

			availability.Availability -= quantity;
			await SaveChangesAsync(availability);
		}

		public async Task RestockProduct(Guid storeId, Guid productId, int quantity)
		{
			var store = await _session.GetAsync<Store>(storeId);
			
			if (store is null)
			{
				throw new ApplicationException("Store not found");
			}
			
			var availability = await _session.QueryOver<ProductAvailability>()
				.Where(x => x.Product.Id == productId)
				.And(x => x.Store.Id == storeId)
				.SingleOrDefaultAsync();
			
			if (availability is null)
			{
				throw new ApplicationException("No product availability found for the store");
			}
			
			availability.Availability += quantity;
			await SaveChangesAsync(availability);
		}

		private Task SaveChangesAsync(object entity)
		{
			_session.UpdateAsync(entity);
			return _session.FlushAsync();
		}
	}
}