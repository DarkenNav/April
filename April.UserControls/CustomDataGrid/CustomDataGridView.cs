using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using April.UserControls.CustomContextMenu;
using April.UserControls.CustomMessageBox;

namespace April.UserControls.CustomDataGrid
{
    public partial class CustomDataGridView : UserControl
    {
        private DataTable dataTable;
        public DataTable Data
        {
            get
            {
                return dataTable;
            }
            set
            {
                dataTable = value;
                dataGridView1.DataSource = dataTable;
            }
        }

        private bool waitFilterKeyPress;
        private int filtredColumnsIndex;
        private FormInputTextWithConfirm dialogForm;
        private CustomDataGridContextMenu contextMenu;

        public CustomDataGridView()
        {
            InitializeComponent();

            dialogForm = new FormInputTextWithConfirm();
            dialogForm.Text = "Введите данные";

            contextMenu = new CustomDataGridContextMenu(dataGridView1);

        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    SetSelectFilterColumn(e.ColumnIndex);
                    break;
                case MouseButtons.Right:
                    ShowContextMenu(e);
                    break;
            }
        }

        private void ShowContextMenu(DataGridViewCellMouseEventArgs e)
        {
            contextMenu.CreateItems(e.ColumnIndex);
            contextMenu.Show();

        }

        private void SetSelectFilterColumn(int index = -1)
        {
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell.Style.Font = new Font(col.HeaderCell.Style.Font, FontStyle.Regular);
            }

            if (waitFilterKeyPress && index >= 0 && filtredColumnsIndex != index)
            {
                waitFilterKeyPress = false;
            }

            if (!waitFilterKeyPress && index >= 0)
            {
                filtredColumnsIndex = index;
               
                var headerCell = dataGridView1.Columns[filtredColumnsIndex].HeaderCell;
                headerCell.Style.Font = new Font(headerCell.Style.Font, FontStyle.Bold);
            }

            waitFilterKeyPress = !waitFilterKeyPress;
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.Programmatic;

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new Font(this.Font, FontStyle.Regular);

            e.Column.HeaderCell.Style = style;
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl dataGridViewControl = (DataGridViewTextBoxEditingControl)e.Control;
            dataGridViewControl.KeyPress += DataGridViewControl_KeyPress;

            //e.Control.KeyPress += new KeyPressEventHandler(dataGridViewTextBox_KeyPress);
        }

        private void DataGridViewControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (waitFilterKeyPress)
            {
                dialogForm.FilterText = e.KeyChar.ToString();
                if (dialogForm.ShowDialog() == DialogResult.OK)
                {
                    var filterField = dataGridView1.Columns[filtredColumnsIndex].Name;
                    dataTable.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", filterField, dialogForm.FilterText);
                }

                SetSelectFilterColumn();
                e.Handled = true;
            }
        }
    }
}
