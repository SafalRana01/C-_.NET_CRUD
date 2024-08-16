using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstCrudProject.Models.DBEntities
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName ="varchar(50)")]
        public string FirstName { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfbirth { get; set; }

        public string Email { get; set; }

        public double Salary { get; set; }


    }
}
