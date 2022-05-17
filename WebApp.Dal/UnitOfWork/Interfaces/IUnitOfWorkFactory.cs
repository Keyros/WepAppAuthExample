using Microsoft.EntityFrameworkCore;

namespace WebApp.Dal.UnitOfWork.Interfaces;

public interface IUnitOfWorkFactory
{
    public IUnitOfWork<DbContext> Create();
}