using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;
using TaskManagement.Model;

namespace TaskManagement.Repository
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>> GetAllTasksAsync();
        Task<TaskModel> GetTaskByTitleAsync(string title);
        Task<int> AddTaskAsync(TaskModel taskmodel);
        Task<TaskModel> GetTaskByIdAsync(int id);
        Task UpdateTaskAsync(int taskId, TaskModel taskmodel);
        Task PatchTaskAsync(int taskId, JsonPatchDocument taskModel);
        Task DeleteTaskAsync(int taskId);
    }
}
