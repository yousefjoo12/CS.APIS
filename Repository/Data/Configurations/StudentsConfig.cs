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
            builder.Property(S => S.St_Code)
                  .IsRequired()
                  .HasMaxLength(30); 

            builder.Property(S => S.St_NameAr)
                  .IsRequired();

            builder.Property(S => S.St_NameEn)
                 .IsRequired();

            builder.Property(S => S.St_Image)
                 .IsRequired();

            builder.Property(P => P.Phone) 
                 .HasMaxLength(20);

            builder.HasOne(S => S.Faculty)
                .WithMany()
                .HasForeignKey(S=>S.Fac_ID);

            builder.HasOne(S => S.FacultyYearSemister)
               .WithMany()
               .HasForeignKey(S => S.FacYearSem_ID);

        }
    }
}
