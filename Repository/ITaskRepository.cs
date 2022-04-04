using System.Collections.Generic;
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
    }
}
