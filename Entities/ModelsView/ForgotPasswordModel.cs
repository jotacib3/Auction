using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.ModelsView
{
    public class ForgotPasswordModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
