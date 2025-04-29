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
    public class AttendanceConfig : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {

            builder.Property(A => A.Sub_Name)
                 .IsRequired();

            builder.HasOne(A => A.Lecture)
                .WithMany()
                .HasForeignKey(A => A.LectureID);

            builder.HasOne(A => A.Students)
                .WithMany()
                .HasForeignKey(A => A.St_ID);

            builder.HasOne(A => A.Subjects)
                .WithMany()
                .HasForeignKey(A => A.Sub_ID);  
        }
    }
}
