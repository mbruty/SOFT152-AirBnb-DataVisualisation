using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SOFT_152_AIR_BnB
{
    public partial class Map : UserControl
    {
        private Image brookyln, manhatten, stattenIsland, bronx, queens;
        private RectangleF brooklynRect, manhattenRect, stattenIslandRect, bronxRect, queensRect;
        public Map()
        {
            InitializeComponent();
            brookyln = new Bitmap(Properties.Resources.Brooklyn);
            brooklynRect = new RectangleF(123, 126, brookyln.Width * 0.16f, brookyln.Height * 0.16f);
            manhatten = new Bitmap(Properties.Resources.Manhatten);
            manhattenRect = new RectangleF(119, 27, manhatten.Width * 0.16f, manhatten.Height * 0.16f);
            stattenIsland = new Bitmap(Properties.Resources.Staten_Island);
            stattenIslandRect = new RectangleF(11, 221, stattenIsland.Width * 0.16f, stattenIsland.Height * 0.16f);
            bronx = new Bitmap(Properties.Resources.Bronx);
            bronxRect = new RectangleF(184, 0, bronx.Width * 0.16f, bronx.Height * 0.16f);
            queens = new Bitmap(Properties.Resources.Queens);
            queensRect = new RectangleF(166, 81, queens.Width * 0.16f, queens.Height * 0.16f);
        }

        private void Map_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(brookyln, brooklynRect);
            e.Graphics.DrawImage(manhatten, manhattenRect);
            e.Graphics.DrawImage(stattenIsland, stattenIslandRect);
            e.Graphics.DrawImage(bronx, bronxRect);
            e.Graphics.DrawImage(queens, queensRect);
            using (LinearGradientBrush linGrShadow = new LinearGradientBrush(
            new Point(0, 0),
            new Point(0, 400),
            Color.OrangeRed,
            Color.Green))
            {
                //Drawing the rectangles
                e.Graphics.FillRectangle(linGrShadow, 400, 0, 50, 400);
            }
        }
    }
}
