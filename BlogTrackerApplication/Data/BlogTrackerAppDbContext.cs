using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogTrackerApplication.Models;

namespace BlogTrackerApplication.Data
{
    public class BlogTrackerAppDbContext : DbContext
    {
        public BlogTrackerAppDbContext (DbContextOptions<BlogTrackerAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<BlogTrackerApplication.Models.BlogInfo> BlogInfo { get; set; } = default!;

        public DbSet<BlogTrackerApplication.Models.AdminInfo>? AdminInfo { get; set; }

        public DbSet<BlogTrackerApplication.Models.EmpInfo>? EmpInfo { get; set; }
    }
}
