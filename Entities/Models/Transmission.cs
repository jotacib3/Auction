﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Transmission :IEntity
    {
        public int  Id { get; set; }
        public string Nombre { get; set; }
    }
}
