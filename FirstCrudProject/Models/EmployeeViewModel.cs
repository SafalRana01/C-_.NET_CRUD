using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstCrudProject.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; } = string.Empty;

        [DisplayName("Last Name")]
        public string LastName { get; set; } = string.Empty;

        [DisplayName("Date Of Birth")]
        public DateTime DateOfbirth { get; set; }

        [Required]
        [DisplayName("E-mail")]

        public string Email { get; set; }


        [DisplayName("Salary")]
        public double Salary { get; set; }


    }
}
