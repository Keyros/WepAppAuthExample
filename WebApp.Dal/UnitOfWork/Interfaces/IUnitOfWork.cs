using Microsoft.EntityFrameworkCore;

namespace WebApp.Dal.UnitOfWork.Interfaces;

public interface IUnitOfWork<T> : IDisposable where T : DbContext
{
    public void SaveChanges();
    
    public IRepository<TR> GetRepository<TR>() where TR : BaseEntity<int>;
}