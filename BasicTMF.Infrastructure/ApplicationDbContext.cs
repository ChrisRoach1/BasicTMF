using BasicTMF.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTMF.Infrastructure
{
    public sealed class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Study> Study { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Study>(study =>
            {
                study.HasKey(x => x.ID);
                study.Property(x => x.ID).ValueGeneratedOnAdd();

            });
        }

    }
}
