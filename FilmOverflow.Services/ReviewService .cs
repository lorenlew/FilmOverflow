using System;
using System.Collections.Generic;
using AutoMapper;
using FilmOverflow.DAL.Models;
using FilmOverflow.DAL.UnitOfWork;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;

namespace FilmOverflow.Services
{
	public class ReviewService : BaseService, IReviewService
	{
		public ReviewService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public void Add(ReviewDomainModel entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			var review = Mapper.Map<ReviewDomainModel, Review>(entity);
			Uow.GetRepository<Review>().Add(review);
			Uow.Save();
		}

		public IEnumerable<ReviewDomainModel> Read()
		{
			var reviews = Uow.GetRepository<Review>().Read();
			var reviewsDomain = Mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDomainModel>>(reviews);
			return reviewsDomain;
		}

		public ReviewDomainModel ReadById(object id)
		{
			var review = Uow.GetRepository<Review>().ReadById(id);
			var reviewDomain = Mapper.Map<Review, ReviewDomainModel>(review);
			return reviewDomain;
		}

		public void Update(ReviewDomainModel entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			var review = Mapper.Map<ReviewDomainModel, Review>(entity);
			Uow.GetRepository<Review>().Update(review);
			Uow.Save();
		}

		public void Delete(ReviewDomainModel entity)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			var review = Mapper.Map<ReviewDomainModel, Review>(entity);
			Uow.GetRepository<Review>().Delete(review);
			Uow.Save();
		}
	}
}
