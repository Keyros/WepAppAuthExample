using Microsoft.EntityFrameworkCore;
using WebApp.Dal.UnitOfWork.Interfaces;

namespace WebApp.Dal.UnitOfWork;

public sealed class WebAppUnitOfWork : IUnitOfWork<DbContext>
{
    private readonly DbContext _context;
    private readonly IRepositoryFactory _repositoryFactory;

    public WebAppUnitOfWork(IDbContextFactory<WebAppDbContext> context, IRepositoryFactory repositoryFactory)
    {
        _context = context.CreateDbContext();
        _repositoryFactory = repositoryFactory;
    }
    

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public IRepository<TR> GetRepository<TR>() where TR : BaseEntity<int>
        => _repositoryFactory.Create<TR>(_context);

    public void Dispose()
    {
        _context.SaveChanges();
        _context.Dispose();
    }
}