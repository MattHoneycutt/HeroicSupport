using System.Linq;

namespace HeroicSupport.Core.Data
{
	public interface IRepository<TEntity>
	{
		void Add(TEntity entity);
		void Save();
		TEntity Get<TID>(TID id);
		IQueryable<TEntity> Query();
	}
}