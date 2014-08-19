using System;
using FilmOverflow.DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FilmOverflow.DAL.UnitOfWork
{
	public interface IUnitOfWork 
	{
		ApplicationDbContext Context { get; }

		UserManager<ApplicationUser> UserManager { get; set; }

		RoleManager<IdentityRole> RoleManager { get; }

		IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;

		void DisableValidationOnSave();

		void Save();
	}
}
