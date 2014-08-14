using System;
using System.Data.Entity;
using FilmOverflow.DAL.Migrations;
using FilmOverflow.DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FilmOverflow.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;

        private IRepository<Cinema> _cinemaRepository;
        private IRepository<Film> _filmRepository;
        private IRepository<Review> _reviewRepository;
        private IRepository<Seance> _seanceRepository;
        private IRepository<Ticket> _ticketRepository;


        public ApplicationDbContext Context { get; private set; }

        public UserManager<ApplicationUser> UserManager { get; set; }

        public RoleManager<IdentityRole> RoleManager { get; private set; }


        public IRepository<Cinema> CinemaRepository
        {
            get
            {
                return _cinemaRepository ?? (_cinemaRepository = new BaseRepository<Cinema>(Context));
            }
        }

        public IRepository<Film> FilmRepository
        {
            get
            {
                return _filmRepository ?? (_filmRepository = new BaseRepository<Film>(Context));
            }
        }

        public IRepository<Review> ReviewRepository
        {
            get
            {
                return _reviewRepository ?? (_reviewRepository = new BaseRepository<Review>(Context));
            }
        }

        public IRepository<Seance> SeanceRepository
        {
            get
            {
                return _seanceRepository ?? (_seanceRepository = new BaseRepository<Seance>(Context));
            }
        }

        public IRepository<Ticket> TicketRepository
        {
            get
            {
                return _ticketRepository ?? (_ticketRepository = new BaseRepository<Ticket>(Context));
            }
        }

        private void Init()
        {
            var dbContext = ApplicationDbContext.Create();
            Context = Context ?? dbContext;
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Context));
            UserManager = UserManager ?? userManager;
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(Context));
            RoleManager = RoleManager ?? roleManager;
        }

        public UnitOfWork()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            Init();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                Context.Dispose();
                UserManager.Dispose();
                RoleManager.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void DisableValidationOnSave()
        {
            Context.Configuration.ValidateOnSaveEnabled = false;
        }

    }
}
