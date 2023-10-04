using SQLite;
using StudyBug.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace StudyBug.Services
{
    public static class CourseService
    {
        static SQLiteAsyncConnection db;
        static async Task Init() 
        {
            if (db == null)
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Courses.db");

                db = new SQLiteAsyncConnection(databasePath);
            }
            
            await db.CreateTableAsync<Course>();
              
            
        }
        public static async Task AddCourse(string name)
        {
            await Init();
            var course = new Course
            { 
                Name = name
            };

            await db.InsertAsync(course);
        }

        public static async Task RemoveCourse(int id)
        {
            await Init();
            await db.DeleteAsync<Course>(id);
        }
        public static async Task<IEnumerable<Course>> GetCourse()
        {
            await Init();
            var schedule = await db.Table<Course>().ToListAsync();
            return schedule;
        }

        public static async Task Update(IEnumerable objects)
        {
            await Init();
            await db.UpdateAllAsync(objects);
        }

    }
}
