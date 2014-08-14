using System;
using FilmOverflow.DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FilmOverflow.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationDbContext Context { get; }

        UserManager<ApplicationUser> UserManager { get; set; }

        RoleManager<IdentityRole> RoleManager { get; }

        IRepository<Cinema> CinemaRepository { get; }

        IRepository<Film> FilmRepository { get; }

        IRepository<Review> ReviewRepository { get; }

        IRepository<Seance> SeanceRepository { get; }

        IRepository<Ticket> TicketRepository { get; }

        void DisableValidationOnSave();

        void Save();
    }
}
