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
    public class SubjectsConfig : IEntityTypeConfiguration<Subjects>
    {
        public void Configure(EntityTypeBuilder<Subjects> builder)
        {
            builder.HasKey(s => s.ID);

            builder.Property(s => s.Sub_Name)
                .IsRequired()
                .HasMaxLength(100);

            //builder.HasOne(s => s.Doctors)
            //       .WithMany()
            //       .HasForeignKey(s => s.Dr_ID)
            //       .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(s => s.Instructors)
            //       .WithMany()
            //       .HasForeignKey(s => s.Ins_ID)
            //       .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(s => s.FacultyYearSemister)
            //       .WithMany()
            //       .HasForeignKey(s => s.FacYearSem_ID)
            //       .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
