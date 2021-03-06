﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using FilmOverflow.DAL.Models;
using FilmOverflow.DAL.UnitOfWork;
using FilmOverflow.Domain.Models;
using FilmOverflow.Services.Interfaces;

namespace FilmOverflow.Services
{
	public class PaymentMethodService : BaseService, IPaymentMethodService
	{
		public PaymentMethodService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public void Add(PaymentMethodDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			PaymentMethod paymentMethod = Mapper.Map<PaymentMethodDomainModel, PaymentMethod>(entity);
			Uow.GetRepository<PaymentMethod>().Add(paymentMethod);
			Uow.Save();
		}

		public IEnumerable<PaymentMethodDomainModel> Read()
		{
			IQueryable<PaymentMethod> paymentMethods = Uow.GetRepository<PaymentMethod>().Read();
			IEnumerable<PaymentMethodDomainModel> paymentMethodsDomain = Mapper.Map<IEnumerable<PaymentMethod>,
				IEnumerable<PaymentMethodDomainModel>>(paymentMethods);
			return paymentMethodsDomain;
		}

		public PaymentMethodDomainModel ReadById(object id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			PaymentMethod paymentMethod = Uow.GetRepository<PaymentMethod>().ReadById(id);
			PaymentMethodDomainModel paymentMethodDomain = Mapper.Map<PaymentMethod,
				PaymentMethodDomainModel>(paymentMethod);
			return paymentMethodDomain;
		}

		public void Update(PaymentMethodDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			PaymentMethod paymentMethod = Mapper.Map<PaymentMethodDomainModel, PaymentMethod>(entity);
			Uow.GetRepository<PaymentMethod>().Update(paymentMethod);
			Uow.Save();
		}

		public void Delete(PaymentMethodDomainModel entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			PaymentMethod paymentMethod = Mapper.Map<PaymentMethodDomainModel, PaymentMethod>(entity);
			Uow.GetRepository<PaymentMethod>().Delete(paymentMethod);
			Uow.Save();
		}

		public IEnumerable<SelectListItem> GetSelectListItems()
		{
			IEnumerable<SelectListItem> paymentMethods = from cat in Uow.GetRepository<PaymentMethod>().Read()
														 select (new SelectListItem
														 {
															 Value = cat.Id.ToString(),
															 Text = cat.Name
														 });
			return paymentMethods;
		}
	}
}
