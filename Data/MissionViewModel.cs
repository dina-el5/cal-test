using Calendar.Models;
using System.Collections.Generic;

namespace Calendar.ViewModels
{
    public class MissionViewModel
    {
        public int MissionId { get; set; }
        public string Title { get; set; }

        public List<int> SelectedEmployeeIds { get; set; }
        public List<Employee> AvailableEmployees { get; set; }

        public SiteChoice Site { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
