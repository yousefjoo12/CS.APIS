using API.DTOs;
using Core.Entities;
using Core.FingerId;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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
        public DbSet<Attendance_T> Attendance { get; set; }   
        public DbSet<Doctors> Doctors { get; set; }  
        public DbSet<Faculty> Faculty { get; set; }  
        public DbSet<FacultyYear> FacultyYear { get; set; }  
        public DbSet<FacultyYearSemister> FacultyYearSemister { get; set; }  
        public DbSet<Lecture_S> Lecture { get; set; }   
        public DbSet<Rooms> Rooms { get; set; }  
        public DbSet<Students> Students { get; set; }  
        public DbSet<Studets_Subject> Studets_Subject { get; set; }  
        public DbSet<Subjects> Subjects { get; set; }  
        public DbSet<SensorData> SensorData { get; set; }  
        public DbSet<Notification> Notification { get; set; }
        public DbSet<LectureResults> LectureResults { get; set; }
        public class SensorDataConfiguration : IEntityTypeConfiguration<SensorData>
        {
            public void Configure(EntityTypeBuilder<SensorData> builder)
            {
                builder.Property(b => b.ID)
                    .HasColumnName("FingerID")
                    .ValueGeneratedNever();
            }
        }
        public class LectureResultsConfiguration : IEntityTypeConfiguration<LectureResults>
        {
            public void Configure(EntityTypeBuilder<LectureResults> builder)
            {
                builder.Property(b => b.ID) 
                    .ValueGeneratedNever();
            }
        }
    }
}
