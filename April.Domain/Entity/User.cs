﻿using April.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace April.Domain.Entity
{
    public class User
        : EntityBase
    {
        public string Name { get; set; }

        public ICollection<DataGridColumnCastomization> DataGridCastomizations { get; set; }
    }
}
