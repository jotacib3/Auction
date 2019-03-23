using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.ModelsView
{
    public class RegisterEmployeeModel: RegisterModel
    {
        [Required]
        [Display(Name = "Employee data")]
        public EmployeeData Employee { get; set; }
    }
}
