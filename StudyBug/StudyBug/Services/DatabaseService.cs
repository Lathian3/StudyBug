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
    public static class DatabaseService
    {
        static SQLiteAsyncConnection db;
        public static async Task Init() 
        {
            if (db == null)
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Courses.db");

                db = new SQLiteAsyncConnection(databasePath);
            }
            
            await db.CreateTableAsync<Course>();
            await db.CreateTableAsync<User>();
            await db.CreateTableAsync<Reminder>();
            
        }
        public static async Task AddCourse(string name)
        {
            var course = new Course
            { 
                Name = name
            };

            await db.InsertAsync(course);
        }
        public static async Task AddUser()
        {
            await db.InsertAsync(App.ActiveUser);
        }

        public static async Task RemoveCourse(int id)
        {
            await db.DeleteAsync<Course>(id);
        }
        public static async Task<IEnumerable<Course>> GetCourse()
        {
            return await db.Table<Course>().ToListAsync();
        }
        public static async Task<IEnumerable<User>> GetUsers()
        {
            return await db.Table<User>().ToListAsync();
        }
        public static async Task GetUser() 
        {
            User user = await db.Table<User>().FirstOrDefaultAsync();
            App.ActiveUser = user;
        }

        public static async Task Update(IEnumerable objects)
        {
            await db.UpdateAllAsync(objects);
        }

    }
}
