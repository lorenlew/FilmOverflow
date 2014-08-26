using System.Data.Entity;
using FilmOverflow.DAL.Migrations;
using FilmOverflow.DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FilmOverflow.DAL.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		public ApplicationDbContext Context { get; private set; }

		public UserManager<ApplicationUser> UserManager { get; set; }

		public RoleManager<IdentityRole> RoleManager { get; private set; }

		public UnitOfWork(ApplicationDbContext applicationDbContext)
		{
			Context = applicationDbContext;
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
			Init();
		}

		private void Init()
		{
			var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Context));
			UserManager = UserManager ?? userManager;
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Context));
			RoleManager = RoleManager ?? roleManager;
		}

		public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity
		{
			return new BaseRepository<TEntity>(Context);
		}

		public void Save()
		{
			Context.SaveChanges();
		}
	}
}
