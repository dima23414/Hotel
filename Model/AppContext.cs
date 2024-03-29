using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;
using Hotel.Model;
using Microsoft.EntityFrameworkCore;

using Microsoft.Data.Sqlite;

namespace Hotel
{
    class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Warehouse> Warehouse { get; set; }

        public DbSet<Movement> Movement { get; set; }

        public DbSet<Directory> Directory => Set<Directory>();

        public DbSet<ViewWarehouse> ViewWarehouse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=.\hotel.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ViewWarehouse>((pc =>
            {
                pc.HasNoKey();
                pc.ToView("ViewWarehouse");
            }));

            modelBuilder.Entity<Directory>().HasKey(x => x.Id);
        }
        /*{
            base.OnModelCreating(modelBuilder);*/


        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ViewWarehouse>((pc =>
        //    {
        //        pc.HasNoKey();
        //        pc.ToView("View_ProductsByCompany");
        //    }));
        //}
    }

