using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDepartment.DataBase.Context.Models
{
    public class Employee : IDbEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Salary { get; set; }

        [Required]
        public Department Department { get; set; }

        public ICollection<Skill> Skills { get; set; }

    }
}
