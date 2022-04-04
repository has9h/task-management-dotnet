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

        // Get instance of DbContext to read data from database, using DI
        // GET : ALL
        public async Task<List<TaskModel>> GetAllTasksAsync()
        {
            // Convert list of Tasks to list of TaskModel
            var records = await _context.Tasks.Select(x => new TaskModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Progress = x.Progress,
                Status = (Model.Status) x.Status,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated
            }).ToListAsync();

            return records;
        }

        // GET : ID
        public async Task<TaskModel> GetTaskByIdAsync(int taskId)
        {
            // Convert list of Tasks to list of TaskModel
            var record = await _context.Tasks.Where(item => item.Id == taskId).Select(item => new TaskModel()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Progress = item.Progress,
                Status = (Model.Status) item.Status,
                DateCreated = item.DateCreated,
                DateUpdated = item.DateUpdated
            }).FirstOrDefaultAsync();

            return record;
        }
        
        // GET : TITLE
        public async Task<TaskModel> GetTaskByTitleAsync(string title)
        {
            // Convert list of Tasks to list of TaskModel
            var record = await _context.Tasks.Where(item => String.Equals(item.Title.ToLower(), title.ToLower()))
                .Select(item => new TaskModel()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Status = (Model.Status) item.Status,
                Progress = item.Progress,
                DateCreated = item.DateCreated,
                DateUpdated = item.DateUpdated
            }).FirstOrDefaultAsync();

            return record;
        }

        // POST
        public async Task<int> AddTaskAsync(TaskModel taskmodel)
        {
            DateTime currentTime = DateTime.Now;
            // Convert object from TaskModel to Task
            var task = new Tasks()
            {
                Title = taskmodel.Title,
                Description = taskmodel.Description,
                Progress = taskmodel.Progress,
                Status = (Data.Status) taskmodel.Progress,
                DateCreated = currentTime,
                DateUpdated = currentTime
            };

            // Tell DbContext to add new record
            _context.Tasks.Add(task);

            // Save changes to update database
            await _context.SaveChangesAsync();

            return task.Id;
        }

        // PUT
        public async Task UpdateTaskAsync(int taskId, TaskModel taskmodel)
        {
            /// <remarks>
            /// Not optimized, as two queries are made here
            /// </remarks>
            // Fetch data using Id, then update required fields
            var record = await _context.Tasks.FindAsync(taskId);
            
            // Id exists
            if (record != null)
            {
                record.Title = taskmodel.Title;
                record.Description = taskmodel.Description;
                record.Progress = taskmodel.Progress;
                record.Status = (Data.Status) taskmodel.Status;
                record.DateCreated = record.DateCreated;
                record.DateUpdated = DateTime.Now;

                await _context.SaveChangesAsync();
            }
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
