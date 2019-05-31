using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser, int>, IApplicationUserRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }

        public ApplicationUserRepository(DbContext context) : base(context)
        {

        }

        public ApplicationUser GetUserById(string userId)
        {
            return AppDbContext.Users.Where(u => u.Id.Equals(userId)).FirstOrDefault();
        }
    }
}