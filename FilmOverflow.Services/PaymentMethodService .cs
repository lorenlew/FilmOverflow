using System;
using System.Collections.Generic;
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
            if (entity == null) throw new ArgumentNullException("entity");
            var paymentMethod = Mapper.Map<PaymentMethodDomainModel, PaymentMethod>(entity);
            Uow.GetRepository<PaymentMethod>().Add(paymentMethod);
        }

        public IEnumerable<PaymentMethodDomainModel> Read()
        {
            var paymentMethods = Uow.GetRepository<PaymentMethod>().Read();
            var paymentMethodsDomain = Mapper.Map<IEnumerable<PaymentMethod>, IEnumerable<PaymentMethodDomainModel>>(paymentMethods);
            return paymentMethodsDomain;
        }

        public PaymentMethodDomainModel ReadById(int id)
        {
            var paymentMethod = Uow.GetRepository<PaymentMethod>().ReadById(id);
            var paymentMethodDomain = Mapper.Map<PaymentMethod, PaymentMethodDomainModel>(paymentMethod);
            return paymentMethodDomain;
        }

        public void Update(PaymentMethodDomainModel entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            var paymentMethod = Mapper.Map<PaymentMethodDomainModel, PaymentMethod>(entity);
            Uow.GetRepository<PaymentMethod>().Update(paymentMethod);
        }

        public void Delete(PaymentMethodDomainModel entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            var paymentMethod = Mapper.Map<PaymentMethodDomainModel, PaymentMethod>(entity);
            Uow.GetRepository<PaymentMethod>().Delete(paymentMethod);
        }

        public void Save()
        {
            Uow.Save();
        }

        public void DisableValidationOnSave()
        {
            Uow.DisableValidationOnSave();
        }
    }
}
