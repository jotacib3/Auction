using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Location : IEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
