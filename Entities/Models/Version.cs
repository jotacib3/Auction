using System;

namespace Entities.Models
{
    public class Version : IEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
