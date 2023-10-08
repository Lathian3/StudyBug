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
        public int WeeklyGoal { get; set; }

    }
}
