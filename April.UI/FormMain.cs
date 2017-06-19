using April.DataAccess.Context;
using April.Domain.Entity;
using April.UI.Implimentations;
using April.UserControls.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
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

            //var check = Controls.DataCheck(); 

            //var screenCapture = new ScreenCapture();
            //screenCapture.Get().Save($@"C:\AprilDownload\{Guid.NewGuid().ToString()}.jpg");

            //try
            //{
            //    throw new Exception("TryCatch: Test exception");
            //}
            //catch (Exception)
            //{
            //}
            //throw new Exception("No TryCatch: Test exception");

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

            customDataGridView1.DataSource = dataTable;
            #endregion

            try
            {
                var users = context.User.ToList();
                comboBoxUserActive.DataSource = users;
                comboBoxUserActive.DisplayMember = "Name";

            }
            catch (Exception ex)
            {
                // TODO: error connection
            }

            customDataGridView1.ColumnChangeSettingsEvent += CustomDataGridView1_ColumnChangeSettingsEvent;
        }

        private void SetDataGridColumnCastomization(User user)
        {
            var castomPropertyList = context.DataGridColumnCastomization.Where(dg => dg.User.ID.Equals(user.ID)).ToList();
            if (castomPropertyList != null)
            {
                customDataGridView1.SetCastomColumnsProperty(castomPropertyList);
            }
        }

        private void CustomDataGridView1_ColumnChangeSettingsEvent(DataGridColumnCastomization settings)
        {
            var castomProperty = context.DataGridColumnCastomization.FirstOrDefault(dg => dg.ID.Equals(settings.ID));
            if (castomProperty != null)
            {
                castomProperty.Width = settings.Width;
                castomProperty.Visible = settings.Visible;

                context.Entry(castomProperty).State = EntityState.Modified;
            }
            else
            {
                var user = (User)comboBoxUserActive.SelectedValue;
                settings.UserId = user.ID;
                context.DataGridColumnCastomization.Add(settings);
            }
            context.SaveChanges();
        }

        private void comboBoxUserActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var user = (User)((ComboBox)sender).SelectedValue;
                SetDataGridColumnCastomization(user);
            }
            catch (Exception ex)
            {
                // TODO: error connection
            }
        }

        private void btnSwitchVisible_Click(object sender, EventArgs e)
        {
            customDataGridView1.SwitchVisibleColumn();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var hint = new BalloonTip();
            hint.Show("hi", "its work?", comboBoxUserActive, ToolTipIcon.Info, 2000);
            
        }
    }
}
