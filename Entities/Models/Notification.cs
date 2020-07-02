using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Notification : IEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
        public bool Enabled { get; set; }
        public string Description { get; set; }

        //public Notification(int id, string userId, User user, bool enabled, string description)
        //{
        //    Id = id;
        //    UserId = userId;
        //    this.user = user;
        //    Enabled = enabled;
        //    Description = description;
        //}
    }
}
