using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

    public class DemoDatabaseContext : DbContext
    {
        public DemoDatabaseContext (DbContextOptions<DemoDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<WebApi.Models.Carts> Carts { get; set; }

        public DbSet<WebApi.Models.Orders> Orders { get; set; }
    }
