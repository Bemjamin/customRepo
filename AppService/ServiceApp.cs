using DAL;
using DAL.Entity;

namespace AppService
{
    public class ServiceApp : IServiceApp
    {
        private readonly IAppUow _appUnitOfWork;

        public ServiceApp(IAppUow appUnitOfWork)
        {
            _appUnitOfWork = appUnitOfWork;
        }


        public void DoSomething()
        {
            using (var transaction = _appUnitOfWork.BeginTransaction())
            {
                var employeRepo = _appUnitOfWork.CreateRepository<Employee>();)
                transaction.Commit();
            }
            
        }
    }
}
