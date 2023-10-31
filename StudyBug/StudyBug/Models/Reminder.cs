﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyBug.Models
{
    public class Reminder
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Content { get; set; }
    }
}
