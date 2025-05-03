using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Configurations
{
    public class StudentsConfig : IEntityTypeConfiguration<Students>
    {
        public void Configure(EntityTypeBuilder<Students> builder)
        {
            builder.HasKey(s => s.ID);

            builder.Property(s => s.St_Code).IsRequired().HasMaxLength(50);
            builder.Property(s => s.St_NameAr).IsRequired().HasMaxLength(100);
            builder.Property(s => s.St_NameEn).IsRequired().HasMaxLength(100);
            builder.Property(s => s.St_Image).HasMaxLength(250);
            builder.Property(s => s.Phone).HasMaxLength(15);

            //builder.HasOne(s => s.Faculty)
            //       .WithMany()
            //       .HasForeignKey(s => s.Fac_ID)
            //       .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(s => s.FacultyYearSemister)
            //       .WithMany()
            //       .HasForeignKey(s => s.FacYearSem_ID)
            //       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
