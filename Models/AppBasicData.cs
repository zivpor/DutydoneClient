using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutydoneClient.Models
{
    public class AppBasicData
    {
        public List<GroupType> GroupTypes { get; set; }
        public List<TaskStatus> TaskStatuses { get; set; }
        public List<TaskType> TaskTypes { get; set; }

    }

    public class GroupType
    {
        public int GroupTypeId { get; set; }
        public string? GroupTypeName { get; set; }
        public GroupType() { }
        
    }

    public class TaskStatus
    {
        public int TaskStatusId { get; set; }
        public string? TaskStatusName { get; set; }
        public TaskStatus() { }
        
    }

    public class TaskType
    {
        public int TaskTypeId { get; set; }
        public string? TaskTypeName { get; set; }
        public TaskType() { }
        
    }
}
