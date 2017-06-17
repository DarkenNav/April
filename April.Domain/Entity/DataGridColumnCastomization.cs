using April.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace April.Domain.Entity
{
    public class DataGridColumnCastomization
        : EntityBase
    {
        public string Name { get; set; }

        public int Width { get; set; }
        public bool Visible { get; set; }
        public int Order { get; set; }


        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
