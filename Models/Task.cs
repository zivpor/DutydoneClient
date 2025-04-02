using DutydoneClient.Services;

namespace DutydoneClient.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        public int? TaskType { get; set; }

        public DateOnly? DueDay { get; set; }
        public DateTime DueDayTime
        {
            get
            {
                if (DueDay != null)
                    return new DateTime(DueDay.Value.Year, DueDay.Value.Month, DueDay.Value.Day);
                else
                    return DateTime.Now;
            }
        }
        public int? UserId { get; set; }
        public string? TaskName { get; set; }

        public int? GroupId { get; set; }

        public int? StatusId { get; set; }
        public string? TaskDescription { get; set; }
        public string? TaskUpdate { get; set; }
        public string StatusName
        {
            get
            {
                if (StatusId != null)
                {
                    TaskStatus? s = ((App)Application.Current).BasicData.TaskStatuses.Where(s => s.TaskStatusId == StatusId).FirstOrDefault();
                    if (s != null)
                        return s.TaskStatusName;
                    
                }
                return "Unknown!";
            }
        }

        public string TypeName
        {
            get
            {
                if (TaskType != null)
                {
                    TaskType? s = ((App)Application.Current).BasicData.TaskTypes.Where(s => s.TaskTypeId == TaskType).FirstOrDefault();
                    if (s != null)
                        return s.TaskTypeName;

                }
                return "Unknown!";
            }
        }
        public Task() { }
        public Task(Models.Task task)
        {
            this.TaskName = task.TaskName;
            this.StatusId = task.StatusId;
            this.TaskId = task.TaskId;
            this.DueDay = task.DueDay;
            this.TaskDescription = task.TaskDescription;
            this.TaskUpdate = task.TaskUpdate;
        }
    }
}
