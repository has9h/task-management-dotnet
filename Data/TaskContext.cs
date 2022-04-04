using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Data
{
    public class TaskContext : DbContext
    {
        // Pass in options to base class as well
        public TaskContext(DbContextOptions<TaskContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// Add new class to context class using DbSet
        /// Creates the table in Db using the name 
        /// </summary>
        public DbSet<Tasks> Tasks { get; set; }

        // Set up connection string to communicate with DB
        /*
         * Configuration can be done in one of 1 places:
         * 1. Define all settings in the context class; override OnConfiguring()
         * 2. Define connection string in startup class
         */
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=.;Database=TasksDb;Integrated Security=True");
        }*/
    }
}
