using April.UserControls.CustomDataGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace April.UserControls.CustomContextMenu
{
    public class CustomDataGridContextMenu
        : ContextMenu
    {
        DataGridView dataGridView;

        public delegate void VisibleChangeEventHandler(CustomDataGridViewTextBoxColumn column);
        public event VisibleChangeEventHandler VisibleChangeEvent;

        public CustomDataGridContextMenu(DataGridView dataGridView)
        {
            this.dataGridView = dataGridView;
        }

        public void CreateItems(int index)
        {
            var headerColumnName = dataGridView.Columns[index].Name;

            MenuItems.Clear();
            MenuItems.Add(new MenuItem(
                $"Скрыть {headerColumnName}",
                (sender, e) => ChangeVisibleColumn(index)
            ));
            MenuItems.Add("-");
            foreach (DataGridViewColumn col in dataGridView.Columns)
            {
                if (!col.Visible)
                {
                    MenuItems.Add(new MenuItem(
                        $"Показать {col.Name}",
                        (sender, e) => ChangeVisibleColumn(col.Index)
                    ));
                }
            }
        }

        private void ChangeVisibleColumn(int index)
        {
            try
            {
                var column = (CustomDataGridViewTextBoxColumn)dataGridView.Columns[index];
                column.Visible = !column.Visible;

                VisibleChangeEvent?.Invoke(column);
            }
            catch (Exception ex)
            {

            } 
        }

        public void Show()
        {
            Show(dataGridView, dataGridView.PointToClient(Cursor.Position));
        }

    }
}
