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
                dataGridView.Columns[index].Visible = !dataGridView.Columns[index].Visible;
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
