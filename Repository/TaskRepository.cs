using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Data;
using TaskManagement.Model;

namespace TaskManagement.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskContext _context;

        /// <summary>
        /// Instance of DbContext required in all methods,
        /// ergo resolve dependency in constructor
        /// </summary>
        public TaskRepository(TaskContext context)
        {
            _context = context;
        }
        // Get instance of DbContext to read data from database,
        // using DI
        public async Task<List<TaskModel>> GetAllTasksAsync()
        {
            // Convert list of Tasks to list of TaskModel
            var records = await _context.Tasks.Select(x => new TaskModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            }).ToListAsync();

            return records;
        }

        public async Task<TaskModel> GetTaskByIdAsync(int taskId)
        {
            // Convert list of Tasks to list of TaskModel
            var record = await _context.Tasks.Where(item => item.Id == taskId).Select(item => new TaskModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description
                }).FirstOrDefaultAsync();

            return record;
        }

        public async Task<TaskModel> GetTaskByTitleAsync(string title)
        {
            // Convert list of Tasks to list of TaskModel
            var record = await _context.Tasks.Where(item => String.Equals(item.Title.ToLower(), title.ToLower()))
                .Select(item => new TaskModel()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description
            }).FirstOrDefaultAsync();

            return record;
        }

        public async Task<int> AddTaskAsync(TaskModel taskmodel)
        {
            // Convert object from TaskModel to Task
            var task = new Tasks()
            {
                Title = taskmodel.Title,
                Description = taskmodel.Description
            };

            // Tell DbContext to add new record
            _context.Tasks.Add(task);

            // Save changes to update database
            await _context.SaveChangesAsync();

            return task.Id;
        }

        public async Task UpdateTaskAsync(int taskId, TaskModel taskmodel)
        {
            var record = new Tasks()
            {
                Id = taskId,
                Title = taskmodel.Title,
                Description = taskmodel.Description
            };

            // Update if changes exist
            _context.Tasks.Update(record);
            await _context.SaveChangesAsync();
        }

        // PATCH
        public async Task PatchTaskAsync(int taskId, JsonPatchDocument taskModel)
        {
            var task = await _context.Tasks.FindAsync(taskId);

            // If record exists
            if (task != null)
            {
                taskModel.ApplyTo(task);
                await _context.SaveChangesAsync();
            }
        }

        // DELETE
        public async Task DeleteTaskAsync(int taskId)
        {
            // Fetch record by title, or some other value
            // If primary key is unknown
            // var task = _context.Tasks.Where(item => item.Title == "").FirstOrDefault();

            // If primary key is known, single Db query can be made
            var task = new Tasks()
            {
                Id = taskId
            };

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}
