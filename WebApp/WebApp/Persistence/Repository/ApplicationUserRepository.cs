using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

        public List<ApplicationUser> GetNotVerifiedUsers()
        {
            IdentityRole userRole = AppDbContext.Roles.Where(r => r.Name.Equals("AppUser")).FirstOrDefault();
            List<ApplicationUser> users = AppDbContext.Users.ToList();
            List<ApplicationUser> resultList = new List<ApplicationUser>();
            foreach (var user in users)
            {
                user.Roles.Any(r => r.RoleId.Equals(userRole.Id));

                if (user.VerificationStatus == Models.Enums.VerificationStatus.Processing &&
                    user.Roles.Any(r => r.RoleId.Equals(userRole.Id)))
                    resultList.Add(user);
            }

            return resultList;
        }
    }
}