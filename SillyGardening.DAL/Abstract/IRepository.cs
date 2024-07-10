namespace SillyGardening.DAL.Abstract
{
    public interface IRepository
    {
        public TEntity Add<TEntity>(TEntity entity) where TEntity : class;

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;

        public TEntity GetById<TEntity>(Guid id) where TEntity : class;

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        public TEntity Remove<TEntity>(TEntity entity) where TEntity : class;

        public TEntity Update<TEntity>(TEntity entity) where TEntity : class;
    }
}