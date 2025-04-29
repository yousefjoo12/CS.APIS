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
    public class InstructorsConfig : IEntityTypeConfiguration<Instructors>
    {
        public void Configure(EntityTypeBuilder<Instructors> builder)
        {
            builder.Property(P => P.Ins_Code)
                  .IsRequired(); 

            builder.Property(D => D.Ins_NameAr)
               .IsRequired();

            builder.Property(D => D.Ins_NameEn)
              .IsRequired();

            builder.Property(P => P.Phone)
               .HasMaxLength(20);
        }
    }
}
