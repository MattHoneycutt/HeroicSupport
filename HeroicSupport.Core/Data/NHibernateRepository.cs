using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace HeroicSupport.Core.Data
{
	public class NHibernateRepository<TEntity> : IRepository<TEntity>
	{
		private readonly ISession _session;

		public NHibernateRepository(ISession session)
		{
			_session = session;
		}

		public TEntity Get<TID>(TID id)
		{
			return _session.Get<TEntity>(id);
		}

		public void Add(TEntity entity)
		{
			_session.Save(entity);
		}

		public void Save()
		{
			_session.Flush();
		}

		public IQueryable<TEntity> Query()
		{
			return _session.Query<TEntity>();
		}
	}
}