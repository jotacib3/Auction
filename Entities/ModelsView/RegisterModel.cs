using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.ModelsView
{
    public class RegisterModel
    {

        [StringLength(256)]
        public string UserName { get; set; }

        [Range(0, 10000000000000)]
        public int NoEmployee { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string PaternalLastName { get; set; }

        [StringLength(50)]
        public string MaternalLastname { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(15)]
        public string CelularTelephone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        public string Address { get; set; }
    }
}
