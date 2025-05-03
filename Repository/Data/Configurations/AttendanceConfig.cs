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
    public class AttendanceConfig : IEntityTypeConfiguration<Attendance_T>
    {
        public void Configure(EntityTypeBuilder<Attendance_T> builder)
        {

            builder.HasOne(A => A.Lecture)
                .WithMany()
                .HasForeignKey(A => A.LectureID)
             .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(A => A.Students)
                .WithMany()
                .HasForeignKey(A => A.St_ID)
             .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
