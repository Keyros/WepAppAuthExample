using Microsoft.EntityFrameworkCore;
using WebApp.Dal.UnitOfWork.Interfaces;

namespace WebApp.Dal.UnitOfWork;

public class RepositoryFactory : IRepositoryFactory
{
    public RepositoryBase<T> Create<T>(DbContext context) where T : BaseEntity<int>
    {
        return new RepositoryBase<T>(context);
    }
}