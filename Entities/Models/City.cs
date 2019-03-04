using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class City : IEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
