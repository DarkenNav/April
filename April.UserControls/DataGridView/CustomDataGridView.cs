using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace April.UserControls.DataGridView
{
    public partial class CustomDataGridView : UserControl
    {
        private DataTable data;
        public DataTable Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
                dataGridView1.DataSource = data;
            }
        }


        public CustomDataGridView()
        {
            InitializeComponent();
        }

    }
}
