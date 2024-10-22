using ChatAPI.Domain.Entities;

namespace ChatAPI.Test.UseCases.Stubs
{
    public class TestRepository<TEntity> where TEntity : IEntity
    {
        public List<TEntity> Entities { get; set; } = [];

        public Guid Add(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            lock (Entities)
            {
                Entities.Add(entity);
            }
            return entity.Id;
        }

        public TEntity? GetById(Guid id)
        {
            return Entities.FirstOrDefault(e => e.Id.Equals(id));
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entities;
        }

        public bool ExistsById(Guid id)
        {
            return Entities.Any(e => e.Id.Equals(id));
        }

        public TEntity? GetByProperty<TProp>(Func<TEntity, TProp> predicate, TProp value)
        {
            return Entities.FirstOrDefault(e => value != null && value.Equals(predicate(e)));
        }
    }
}
