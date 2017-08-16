using DalFramework;

namespace DAL
{
    public class AppUow : UnitOfWork<AppDbContext>, IAppUow
    {
    }
}
