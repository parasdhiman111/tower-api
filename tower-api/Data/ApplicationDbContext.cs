
using System;
using Microsoft.EntityFrameworkCore;
using tower_api.Repositories.Models;

namespace credit_work_app.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

