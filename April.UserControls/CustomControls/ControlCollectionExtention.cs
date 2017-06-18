using April.UserControls.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.Control;

namespace April.UserControls.CustomControls
{
    public static class ControlCollectionExtention
    {
        public static bool DataCheck(this ControlCollection control)
        {
            var result = true;
            foreach (var elem in control)
            {
                if (elem.GetType() == typeof(ICheckableControl))
                {
                    if (!((ICheckableControl)elem).Check(false))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
