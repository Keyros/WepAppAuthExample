using Microsoft.EntityFrameworkCore;
using WebApp.Dal.UnitOfWork.Interfaces;

namespace WebApp.Dal.UnitOfWork;

public class RepositoryBase<T> : IRepository<T> where T : BaseEntity<int>
{
    public DbSet<T> DbSet { get; set; }

    public RepositoryBase(DbContext context)
    {
        DbSet = context.Set<T>();
    }

    public IQueryable<T> GetAll() => DbSet;

    public T GetById(int id)
    {
        return DbSet.First(x => x.Id == id);
    }

    public void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(T entity)
    {
        throw new NotImplementedException();
    }
}