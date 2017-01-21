using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dork.Core.Domain
{
    public abstract class EntityBase
    {
        public string Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;
            var entity = obj as EntityBase;
            if (entity == null) return false;
            return Id == entity.Id;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id?.GetHashCode() ?? base.GetHashCode();
            }
        }
    }
}
