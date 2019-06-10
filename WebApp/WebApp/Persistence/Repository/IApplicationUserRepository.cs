using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser, int>
    {
        ApplicationUser GetUserById(string userId);
        List<ApplicationUser> GetNotVerifiedUsers();
    }
}
