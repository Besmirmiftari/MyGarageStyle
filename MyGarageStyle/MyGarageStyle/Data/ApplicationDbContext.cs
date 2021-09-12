using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyGarageStyle.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyGarageStyle.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PVC> PVCs { get; set; }
        public DbSet<PVCImages> PVCImages { get; set; }
    }
}
