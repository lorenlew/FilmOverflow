using System.Collections.Generic;
using FilmOverflow.Domain.Models;

namespace FilmOverflow.Services.Interfaces
{
    public interface IPaymentMethodService : IService
    {
        void Add(PaymentMethodDomainModel entity);

        IEnumerable<PaymentMethodDomainModel> Read();

        PaymentMethodDomainModel ReadById(int id);

        void Update(PaymentMethodDomainModel entity);

        void Delete(PaymentMethodDomainModel entity);

        void Save();

        void DisableValidationOnSave();

    }
}
