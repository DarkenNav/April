using April.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace April.UserControls.CustomDataGrid
{
    public class CustomDataGridViewTextBoxColumn
        : DataGridViewTextBoxColumn
    {
        public Guid UserSettingID { get; set; } = Guid.Empty;

        public int DefaultWidth { get; set; }

        public bool CanSwitch { get; set; } = true;

        public void DefaultSeting()
        {
            Width = DefaultWidth;
            Visible = true;

        }


    }
}
