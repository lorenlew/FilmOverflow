using System;
using FilmOverflow.DAL.UnitOfWork;
using FilmOverflow.Services.Interfaces;

namespace FilmOverflow.Services
{
	public class BaseService : IService
	{
		protected readonly IUnitOfWork Uow;

		public BaseService(IUnitOfWork unitOfWork)
		{
			Uow = unitOfWork;
		}
	}
}
