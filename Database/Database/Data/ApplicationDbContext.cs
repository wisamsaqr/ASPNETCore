using Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<User1> Users1 { get; set; }
        public DbSet<Department1> Departments1 { get; set; }

        public DbSet<User2> Users2 { get; set; }
        public DbSet<Department2> Departments2 { get; set; }

        public DbSet<User3> Users3 { get; set; }
        public DbSet<Department3> Departments3 { get; set; }
    }
}