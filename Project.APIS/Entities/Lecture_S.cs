﻿using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{ 
    public class Lecture_S : BaseEntity
    {
        public string? Lecture_Name { get; set; }

        [ForeignKey(nameof(Subjects))] 
        public int Sub_ID { get; set; }
        public Subjects Subjects { get; set; } 
        public Days day { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}
