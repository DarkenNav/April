using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace April.Domain.Entity.Base
{
    public class EntityBase
    {
        public Guid ID { get; set; } = Guid.NewGuid();
    }
}
