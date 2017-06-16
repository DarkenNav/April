using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace April.UI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            #region Test data init
            var dataTable = new DataTable();
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Phone");

            dataTable.Rows.Add("Ivan", "123");
            dataTable.Rows.Add("Petr", "1222");

            customDataGridView1.Data = dataTable;

            #endregion
        }
    }
}
