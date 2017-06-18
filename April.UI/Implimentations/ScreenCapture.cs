using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace April.UI.Implimentations
{
    public class ScreenCapture
    {
        public Image Get()
        {
            Image img = null;
            using (Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                }

                MemoryStream ms = new MemoryStream();
                bitmap.Save(ms, ImageFormat.Jpeg);
                img = Image.FromStream(ms);
            }

            return img;
        }

    }
}
