using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace KhaoSat.Models
{
    public class KhaoSatDbContext : DbContext
    {
        private static readonly MethodInfo _propertyMethod = typeof(EF).GetMethod(nameof(EF.Property), BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(typeof(bool));
        public KhaoSatDbContext(DbContextOptions<KhaoSatDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // modelBuilder.HasDefaultSchema("orcl");
        }
        
        public DbSet<Users> Users { get; set; }
        public DbSet<UserSurveys> UserSurveys { get; set; }
        public DbSet<UserAnswers> UserAnswers { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Surveys> Surveys { get; set; }


       
    }
}
