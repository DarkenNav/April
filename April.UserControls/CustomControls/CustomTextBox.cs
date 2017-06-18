using April.UserControls.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace April.UserControls.CustomControls
{
    public class CustomTextBox
        : TextBox, ICheckableControl    {
        public bool EmptyDataCheck { get; set; }

        public string EmptyDataText { get; set; }

        public bool Check(bool showMessage = true)
        {
            if (!EmptyDataCheck) return true;
            if (this.Text != null && this.Text != string.Empty) return true;

            if(showMessage) MessageBox.Show(EmptyDataText);
            return false;
        }

    }
}
