using DutydoneClient.Services;

namespace DutydoneClient.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        public int? TaskType { get; set; }

        public DateOnly? DueDay { get; set; }

        public int? UserId { get; set; }
        public string? TaskName { get; set; }

        public int? GroupId { get; set; }

        public int? StatusId { get; set; }
        public Task() { }
        public Task(Models.Task task)
        {
            this.TaskName = task.TaskName;
            this.StatusId = task.StatusId;
            this.TaskId = task.TaskId;
            this.DueDay = task.DueDay;
        }
    }
}
