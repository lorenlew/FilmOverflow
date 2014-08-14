using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FilmOverflow.Services.Interfaces
{
    public interface IRoleManagerService : IService
    {
        RoleManager<IdentityRole> RoleManager { get; }
    }
}
