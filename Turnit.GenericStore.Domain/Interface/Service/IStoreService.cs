using System;
using System.Net;
using System.Threading.Tasks;

namespace Turnit.GenericStore.Domain.Interface.Service
{
	public interface IStoreService
	{
		Task BookProduct(Guid productId, Guid storeId, int quantity);
		Task RestockProduct(Guid productId, Guid storeId, int quantity);
	}
}