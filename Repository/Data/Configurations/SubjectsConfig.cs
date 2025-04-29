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
            builder.Property(S => S.Sub_Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(S => S.Doctors)
                   .WithMany()
                   .HasForeignKey(S => S.Dr_ID);

            builder.HasOne(S => S.Instructors)
                   .WithMany()
                   .HasForeignKey(S => S.Ins_ID);
            
            builder.HasOne(S => S.FacultyYearSemister)
                   .WithMany()
                   .HasForeignKey(S => S.FacYearSem_ID);

        }
    }
}
