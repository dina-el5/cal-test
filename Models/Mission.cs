using System.ComponentModel.DataAnnotations;
using Calendar.Models;

namespace Calendar.Models
{
    public class Mission
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        [Required]
        public SiteChoice Site { get; set; }

        [Required]
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
    public enum SiteChoice
    {
        SiteA,
        SiteB,
        SiteC,
        SiteD
    }
}
