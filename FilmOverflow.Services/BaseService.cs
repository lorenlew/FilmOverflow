using System;
using FilmOverflow.DAL.UnitOfWork;
using FilmOverflow.Services.Interfaces;

namespace FilmOverflow.Services
{
    public class BaseService : IService
    {
        protected readonly IUnitOfWork Uow;
        private bool _disposed;

        public BaseService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            Uow = unitOfWork;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                Uow.Dispose();
            }
            _disposed = true;
        }
    }
}
