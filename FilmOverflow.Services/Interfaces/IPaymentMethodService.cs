using System.Collections.Generic;
using System.Web.Mvc;
using FilmOverflow.Domain.Models;

namespace FilmOverflow.Services.Interfaces
{
	public interface IPaymentMethodService : IService
	{
		void Add(PaymentMethodDomainModel entity);

		IEnumerable<PaymentMethodDomainModel> Read();

		PaymentMethodDomainModel ReadById(object id);

		void Update(PaymentMethodDomainModel entity);

		void Delete(PaymentMethodDomainModel entity);

		IEnumerable<SelectListItem> GetSelectListItems();
	}
}
