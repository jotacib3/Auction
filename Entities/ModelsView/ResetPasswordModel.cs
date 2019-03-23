using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ModelsView
{
    public class ResetPasswordModel
    {
        public string Code { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
