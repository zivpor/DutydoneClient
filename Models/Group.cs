using DutydoneClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutydoneClient.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public int? GroupAdmin { get; set; }
        public string? GroupName { get; set; }
        public string GroupProfileImagePath { get; set; }
        public int? GroupType { get; set; }
        public string? GroupTypeName
        {
            get
            {
                if (GroupType != null)
                {
                    List<GroupType> types = ((App)Application.Current).BasicData.GroupTypes;
                    GroupType? type = types.Where(t => t.GroupTypeId == GroupType).FirstOrDefault();
                    if (type != null)
                        return type.GroupTypeName;
                    return "Unknown";
                }
                return "Unknown";
            }
        }

        public string FullImageUrl
        {

            get
            {
                return DutyDoneAPIProxy.ImageBaseAddress + this.GroupProfileImagePath; 
            }
        }
        public Group() { }
        
    }
}
