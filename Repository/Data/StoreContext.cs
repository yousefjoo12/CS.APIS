using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Repository.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Doctors> Doctors { get; set; }  
        public DbSet<Faculty> Faculty { get; set; }  
        public DbSet<FacultyYear> FacultyYear { get; set; }  
        public DbSet<FacultyYearSemister> FacultyYearSemister { get; set; }  
        public DbSet<Rooms> Rooms { get; set; }  
        public DbSet<Students> Students { get; set; }  
        public DbSet<Studets_Rooms> Studets_Rooms { get; set; }  
        public DbSet<Instructors> Instructors { get; set; }  
        public DbSet<Studets_Subject> Studets_Subject { get; set; }  
        public DbSet<Subjects> Subjects { get; set; }  

    }
}
