namespace Turnit.GenericStore.Domain.Interface.Repository
{
	public interface IRepository<T>  where T : class
	{
		T GetById(int id);
		void Save(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}