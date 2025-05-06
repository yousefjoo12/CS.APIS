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
            builder.HasKey(f => f.ID);

            builder.Property(f => f.Sem_Code).HasMaxLength(50);
            builder.Property(f => f.Sem_Name).HasMaxLength(100);

            //builder.HasOne(f => f.FacultyYear)
            //       .WithMany()
            //       .HasForeignKey(f => f.FacYear_Id)
            //       .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
