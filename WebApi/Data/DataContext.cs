

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Feedback;
using WebApi.User;
using WebApi.userInfo;

namespace WebApi.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions options) : base (options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<feedBack> feedBacks { get; set; }
        public DbSet<publicInfo> publicInfos { get; set; }
    }
}
