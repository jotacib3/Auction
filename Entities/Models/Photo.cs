using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Photo : IEntity
    {
        public int Id { get; set; }

        public string FileName { get; set; }
    }
}
