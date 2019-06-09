using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApp.Persistence.Repository
{
    public class IdentityRoleRepository : Repository<IdentityRole, int>, IIdentityRoleRepository
    {
        protected ApplicationDbContext AppDbContext { get { return context as ApplicationDbContext; } }
        public IdentityRoleRepository(DbContext context) : base(context)
        {
        }
    }
}