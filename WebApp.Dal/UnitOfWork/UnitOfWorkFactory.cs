using Microsoft.EntityFrameworkCore;
using WebApp.Dal.UnitOfWork.Interfaces;

namespace WebApp.Dal.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<WebAppDbContext> _contextFactory;
    private readonly IRepositoryFactory _repositoryFactory;

    public UnitOfWorkFactory(IDbContextFactory<WebAppDbContext> contextFactory, IRepositoryFactory repositoryFactory)
    {
        _contextFactory = contextFactory;
        _repositoryFactory = repositoryFactory;
    }
    
    public IUnitOfWork<DbContext> Create()
    {
        return new WebAppUnitOfWork(_contextFactory, _repositoryFactory);
    }
}