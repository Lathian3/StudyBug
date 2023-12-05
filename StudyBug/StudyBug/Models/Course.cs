using SQLite;
using System;

namespace StudyBug.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Goal { get; set; }
        public Double currentTimeStudied { get; set; }
        public string Description { get; set; }

    }
}
