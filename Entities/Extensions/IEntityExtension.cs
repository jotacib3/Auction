using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    public static class IEntityExtensions
    {

        public static bool IsObjectNull(this IEntity entity)
        {
            return entity == null;
        }

        public static bool IsDifferentObject(this IEntity entity, int Id)
        {
            return !entity.Id.Equals(Id);
        }


    }
}
