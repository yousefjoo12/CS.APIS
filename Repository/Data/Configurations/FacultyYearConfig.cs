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
    public class FacultyYearConfig : IEntityTypeConfiguration<FacultyYear>
    {
        public void Configure(EntityTypeBuilder<FacultyYear> builder)
        {
            builder.HasKey(fy => fy.ID);

            builder.Property(fy => fy.Year)
                .IsRequired()
                .HasMaxLength(4);
        }
    }
}
