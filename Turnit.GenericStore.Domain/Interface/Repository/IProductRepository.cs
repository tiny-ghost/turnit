using System.Collections.Generic;
using System.Threading.Tasks;
using Turnit.GenericStore.Domain.Entity;

namespace Turnit.GenericStore.Domain.Interface.Repository
{
	public interface IProductRepository : IRepository<Product>
	{
		Task<IEnumerable<Product>> GetAllProducts();
	}
}