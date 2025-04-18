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
            builder.Property(P => P.Sem_Code)
                 .IsRequired()
                 .HasMaxLength(30);
        }
    }
}
