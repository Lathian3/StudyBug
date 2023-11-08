using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace StudyBug.Models
{
    public class User
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public double WeeklyGoal { get; set; }
        public int break_frequency { get; set; }
        public int break_length { get; set; }
        public long creationDate { get; set; }
        public long nextResetDate { get; set; }
        public long lastLoginDate { get; set; }

    }
}
