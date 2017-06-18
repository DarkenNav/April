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
using April.Domain.Entity;

namespace April.UserControls.CustomDataGrid
{
    public partial class CustomDataGridView : UserControl
    {
        public delegate void ColumnChangeSettingsEventHandler(DataGridColumnCastomization settings);
        public event ColumnChangeSettingsEventHandler ColumnChangeSettingsEvent;

        public DataTable DataSource
        {
            get
            {
                return (DataTable)dataGridView1.DataSource;
            }
            set
            {
                var dataTable = value;
                dataGridView1.DataSource = dataTable;
                if (dataTable != null)
                {
                    foreach (DataColumn col in dataTable.Columns)
                    {
                        var newColumn = new CustomDataGridViewTextBoxColumn();
                        newColumn.Name = col.ColumnName;
                        newColumn.DataPropertyName = col.ColumnName;
                        dataGridView1.Columns.Add(newColumn);
                    }
                }

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
            contextMenu.VisibleChangeEvent += ContextMenu_VisibleChangeEvent;

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnWidthChanged += DataGridView1_ColumnWidthChanged;
        }

        private void ContextMenu_VisibleChangeEvent(CustomDataGridViewTextBoxColumn column)
        {
            ColumnChangeSettings(column);
        }

        private void DataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            ColumnChangeSettings((CustomDataGridViewTextBoxColumn)e.Column);
        }

        private void ColumnChangeSettings(CustomDataGridViewTextBoxColumn column)
        {
            if (BlockSaveSetting) return;

            var setting = new DataGridColumnCastomization
            {
                ID = (column.UserSettingID != Guid.Empty) ? column.UserSettingID : Guid.NewGuid(),
                Name = column.Name,
                Width = column.Width,
                Visible = column.Visible
            };
            ColumnChangeSettingsEvent?.Invoke(setting);
        }

        private bool BlockSaveSetting;
        public void SetCastomColumnsProperty(List<DataGridColumnCastomization> settings)
        {
            BlockSaveSetting = true;
            foreach (CustomDataGridViewTextBoxColumn column in dataGridView1.Columns)
            {
                var set = settings.FirstOrDefault(s => s.Name.Equals(column.DataPropertyName));
                if (set != null)
                {
                    column.UserSettingID = set.ID;
                    column.Width = set.Width;
                    column.Visible = set.Visible;
                }
                else
                {
                    column.DefaultSeting();
                }
            }
            BlockSaveSetting = false;
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
            var column = (CustomDataGridViewTextBoxColumn)e.Column;

            column.SortMode = DataGridViewColumnSortMode.Programmatic;

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new Font(this.Font, FontStyle.Regular);

            column.HeaderCell.Style = style;

            column.DefaultWidth = column.Width;

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl dataGridViewControl = (DataGridViewTextBoxEditingControl)e.Control;
            dataGridViewControl.KeyPress += DataGridViewControl_KeyPress;
        }

        private void DataGridViewControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (waitFilterKeyPress)
            {
                dialogForm.FilterText = e.KeyChar.ToString();
                if (dialogForm.ShowDialog() == DialogResult.OK)
                {
                    var filterField = dataGridView1.Columns[filtredColumnsIndex].Name;
                    DataSource.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", filterField, dialogForm.FilterText);
                }

                SetSelectFilterColumn();
                e.Handled = true;
            }
        }

        public void SwitchVisibleColumn()
        {
            foreach (var column in dataGridView1.Columns)
            {
                if (column.GetType() == typeof(CustomDataGridViewTextBoxColumn))
                {
                    var col = (CustomDataGridViewTextBoxColumn)column;
                    if (col.CanSwitch)
                    {
                        col.Visible = !col.Visible;
                    }
                }
            }
        }
    }
}
