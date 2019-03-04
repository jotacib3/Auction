using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Year : IEntity
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
    }
}
