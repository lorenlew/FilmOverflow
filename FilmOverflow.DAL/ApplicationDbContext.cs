using System.Data.Entity;
using FilmOverflow.DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FilmOverflow.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Film> Films { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Seance> Seances { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public ApplicationDbContext()
        // while using localDB
        //: base("", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}