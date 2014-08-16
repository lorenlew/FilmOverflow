﻿using System.Collections.Generic;
using FilmOverflow.Domain.Models;

namespace FilmOverflow.Services.Interfaces
{
    public interface ISeanceService : IService
    {
        void Add(SeanceDomainModel entity);

        IEnumerable<SeanceDomainModel> Read();

        SeanceDomainModel ReadById(int id);

        void Update(SeanceDomainModel entity);

        void Delete(SeanceDomainModel entity);

        void Save();

        void DisableValidationOnSave();

    }
}