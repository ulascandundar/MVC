using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class ERYAZContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-2IVVGDBJ\SQLEXPRESS01;Database=AdminPanel;user id=sa;password=8066806619651998;");
        }
        public DbSet<OperationClaim> OperationClaims { get; set; }

        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

    }
}
