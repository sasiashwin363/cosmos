using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CosmosWebAPI.Models
{
    public class Employee
    {
        [Key]
        public string? id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string? EmployeeName { get; set; }
        [Required]
        public string? Department { get; set; }
        [Required]
        public int Salary { get; set; }
    }
}
