using April.DataAccess.Context;
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
        AprilDbContext context = new AprilDbContext();
        Random rand = new Random();

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
            dataTable.Columns.Add("Дата регистрации");
            dataTable.Columns.Add("Активность");

            for (var i = 0; i < 500; i++)
            {
                dataTable.Rows.Add(
                    $"Ivan {i.ToString()}",
                    $"{i.ToString()}-{rand.Next(0, 99999)}",
                    $"{DateTime.Now.AddDays(-rand.Next(0, 5)).ToString("dd.MM.yyyy")}",
                    $"{((rand.Next(0, 10) > 5) ? "Активен" : "Не активен")}"
                    );
            }

            customDataGridView1.Data = dataTable;

            #endregion

            try
            {
                var users = context.User.ToList();

                comboBoxUserActive.DataSource = users;
                comboBoxUserActive.DisplayMember = "Name";
                comboBoxUserActive.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                // TODO: error connection
            } 

        }
    }
}
