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
    public class FacultyYearSemisterConfig : IEntityTypeConfiguration<FacultyYearSemister>
    {
        public void Configure(EntityTypeBuilder<FacultyYearSemister> builder)
        {
            builder.Property(FYS => FYS.Sem_Code)
                 .IsRequired();

            builder.Property(FYS => FYS.Sem_Name)
                .IsRequired();

            builder.HasOne(S => S.FacultyYear)
              .WithMany()
              .HasForeignKey(S => S.FacultyYearId);

        }
    }
}
