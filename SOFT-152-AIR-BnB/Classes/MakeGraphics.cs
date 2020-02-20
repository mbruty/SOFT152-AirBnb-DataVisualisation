using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOFT_152_AIR_BnB
{
    class MakeGraphics
    {
        private readonly Color primary = Color.FromArgb(255, 90, 95);
        private readonly Color primaryGrad = Color.FromArgb(252, 100, 45);
        private readonly Color darkPrimary = Color.FromArgb(198, 32, 53);
        //
        // Nothing is needed in the constructor as everything is stored internally in the class
        //
        public MakeGraphics()
        {
        }
        //
        // Function for the form to draw the top bar
        //
        public void DrawTopBar(PaintEventArgs e)
        {
            //Creating the top bar gradient
            using(LinearGradientBrush linGrBrush = new LinearGradientBrush(
            new Point(0, 0),
            new Point(1920, 80),
            primary,
            primaryGrad))
            using (LinearGradientBrush linGrShadow = new LinearGradientBrush(
            new Point(0, 10),
            new Point(0, 0),
            Color.White,
            darkPrimary))
            {
                //Drawing the rectangles
                Pen pen = new Pen(linGrBrush);
                Pen shadow = new Pen(linGrShadow);
                e.Graphics.FillRectangle(linGrShadow, 0, 30, 1920, 10);
                e.Graphics.FillRectangle(linGrBrush, 0, 0, 1920, 30);
            }
        }
        //
        // Generic function for drawing a string to the screen from a UserControl
        //
        public void DrawString(String inText, int inSize, Color inColor, int inX, int inY, UserControl U)
        {
            Graphics formGraphics = U.CreateGraphics();
            Font drawFont = new Font("Roboto", inSize);
            using (SolidBrush drawBrush = new SolidBrush(inColor))
            using (StringFormat drawFormat = new StringFormat())
                formGraphics.DrawString(inText, drawFont, drawBrush, inX, inY, drawFormat);
        }
        public Color GetDarkPrimary()
        {
            return darkPrimary;
        }
    }
}
