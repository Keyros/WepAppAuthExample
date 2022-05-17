namespace WebApp.Dal.UnitOfWork.Interfaces;

public interface IRepository<T> where T : BaseEntity<int>
{
    IQueryable<T> GetAll();
    T GetById(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}