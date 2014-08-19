﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmOverflow.Domain.Models;

namespace FilmOverflow.Services.Interfaces
{
	public interface IReservedSeatService : IService
	{
		void Add(ReservedSeatDomainModel entity);

		IEnumerable<ReservedSeatDomainModel> Read();

		ReservedSeatDomainModel ReadById(object id);

		void Update(ReservedSeatDomainModel entity);

		void Delete(ReservedSeatDomainModel entity);

		void DisableValidationOnSave();
	}
}