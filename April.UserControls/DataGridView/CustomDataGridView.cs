﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using April.UserControls.MessageBox;

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

        private bool waitFilterKeyPress;
        private int filtredColumnsIndex;
        private FormInputTextWithConfirm dialogForm;

        public CustomDataGridView()
        {
            InitializeComponent();

            dialogForm = new FormInputTextWithConfirm();
            dialogForm.Text = "Введите данные";


        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SetSelectFilterColumn(e.ColumnIndex);
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
                dialogForm.InputText = e.KeyChar.ToString();
                if (dialogForm.ShowDialog() == DialogResult.OK)
                {
                    //data.
                }

                SetSelectFilterColumn();
                e.Handled = true;
            }
        }
    }
}
