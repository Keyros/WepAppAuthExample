using Microsoft.EntityFrameworkCore;

namespace WebApp.Dal.UnitOfWork.Interfaces;

public interface IRepositoryFactory
{
    public RepositoryBase<T> Create<T>(DbContext context) where T : BaseEntity<int>;
}