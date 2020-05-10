using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Repositories;
using NG.DBManager.Infrastructure.Contracts.UnitsOfWork;
using NG.DBManager.Infrastructure.Impl.EF.Repositories;

namespace NG.DBManager.Infrastructure.Impl.EF.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NgContext _context;


        private ITourRepository _tourRepository;

        public ITourRepository TourRepository
        {
            get
            {
                return _tourRepository ?? (_tourRepository = new TourRepository(_context));
            }
        }

        public UnitOfWork(NgContext context)
        {
            _context = context;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
