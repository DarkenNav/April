using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace April.UserControls.CustomMessageBox
{
    public partial class FormInputTextWithConfirm : Form
    {
        public string FilterText
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }


        public FormInputTextWithConfirm()
        {
            InitializeComponent();
        }

        private void FormInputTextWithConfirm_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }
    }
}
