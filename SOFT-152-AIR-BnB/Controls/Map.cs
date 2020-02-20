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
using System.Threading;

namespace SOFT_152_AIR_BnB
{
    public partial class Map : UserControl
    {
        private Bitmap brookyln, manhatten, stattenIsland, bronx, queens, gradient;
        private RectangleF brooklynRect, manhattenRect, stattenIslandRect, bronxRect, queensRect;
        public EventHandler mapLoaded;
        private double[] avgPrice;
        public Map(double brookylnPrice, double manhattanPrice, double stattenIslandPrice, double bronxPrice, double queensPrice)
        {
            InitializeComponent();
            brookyln = new Bitmap(Properties.Resources.Brooklyn);
            brooklynRect = new RectangleF(123, 151, brookyln.Width * 0.16f, brookyln.Height * 0.16f);
            manhatten = new Bitmap(Properties.Resources.Manhatten);
            manhattenRect = new RectangleF(119, 52, manhatten.Width * 0.16f, manhatten.Height * 0.16f);
            stattenIsland = new Bitmap(Properties.Resources.Staten_Island);
            stattenIslandRect = new RectangleF(11, 246, stattenIsland.Width * 0.16f, stattenIsland.Height * 0.16f);
            bronx = new Bitmap(Properties.Resources.Bronx);
            bronxRect = new RectangleF(184, 25, bronx.Width * 0.16f, bronx.Height * 0.16f);
            queens = new Bitmap(Properties.Resources.Queens);
            queensRect = new RectangleF(166, 106, queens.Width * 0.16f, queens.Height * 0.16f);
            createGradient();
            avgPrice = new double[] { brookylnPrice, manhattanPrice, stattenIslandPrice, bronxPrice, queensPrice };

        }
        public void Process()
        {
            //As it takes a while to process the image, it is ran on a different thread as to not take ages loading the form
            Thread t = new Thread(colourDistricts);
            t.Start();
        }
        private void colourDistricts()
        {
            double maxPrice = 0f, minPrice = avgPrice[0];
            foreach(double price in avgPrice)
            {
                //Get the higest and lowest price
                if(maxPrice < price)
                {
                    maxPrice = price;
                }
                if(price < minPrice)
                {
                    minPrice = price;
                }
            }
            //Thread safe setting of the high, mid and low lables
            Util.SetControlPropertyThreadSafe(minPriceLabel, "Text", String.Format("${0:00}", minPrice));
            Util.SetControlPropertyThreadSafe(maxPriceLabel, "Text", String.Format("${0:00}", maxPrice));
            Util.SetControlPropertyThreadSafe(midLabel, "Text", String.Format("${0:00}", (minPrice + maxPrice) / 2));
            //Getting the colour of each district from the created gradent on a % of 100 where 100 is the highest and 0% is the lowest
            Color brookylnCol = gradient.GetPixel(0, Convert.ToInt32(Math.Floor(((avgPrice[0] - minPrice) / (maxPrice - minPrice)) * 100))),
                manhattenCol = gradient.GetPixel(0, Convert.ToInt32(Math.Floor(((avgPrice[1] - minPrice) / (maxPrice - minPrice)) * 100))),
                stattenIslandCol = gradient.GetPixel(0, Convert.ToInt32(Math.Floor(((avgPrice[2] - minPrice) / (maxPrice - minPrice)) * 100))),
                bronxCol = gradient.GetPixel(0, Convert.ToInt32(Math.Floor(((avgPrice[3] - minPrice) / (maxPrice - minPrice)) * 100))),
                queensCol = gradient.GetPixel(0, Convert.ToInt32(Math.Floor(((avgPrice[4] - minPrice) / (maxPrice - minPrice)) * 100)));
            //Colour each district with the new colour
            replaceColour(brookylnCol, brookyln);
            replaceColour(manhattenCol, manhatten);
            replaceColour(stattenIslandCol, stattenIsland);
            replaceColour(bronxCol, bronx);
            replaceColour(queensCol, queens);
            //Pushing the event up that the processing has finished
            mapLoaded(this, EventArgs.Empty);
        }
        private void replaceColour(Color colour, Bitmap image)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    //Goes through each pixel of the image and changes the colour if the pixel isn't transparent
                    if (image.GetPixel(x, y) != Color.FromArgb(0, 0, 0, 0))
                    {
                        image.SetPixel(x, y, colour);
                    }
                }
            }
        }
        private void createGradient()
        {
            //Creates a gradient where 0 is green and 100 is red, with orange as the midpoint
            gradient = new Bitmap(1, 101);
            using (Graphics graphics = Graphics.FromImage(gradient))
            using (LinearGradientBrush linGr = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, 101),
                Color.Orange, Color.Red))
            {
                //Color blend needed as the gradient from red - green isn't pleasing on the eyes
                ColorBlend blend = new ColorBlend(3);
                blend.Colors = new Color[3] { Color.Green, Color.Orange, Color.Red };
                blend.Positions = new float[3] { 0f, 0.5f, 1f };
                linGr.InterpolationColors = blend;
                graphics.FillRectangle(linGr, 0, 0, gradient.Width, gradient.Height);
            }
            //Uncomment this to inspect the gradient and make sure it's working as expected
            //gradient.Save("Temp.bmp");

        }

        private void Map_Paint(object sender, PaintEventArgs e)
        {
            //Draw each image on to the map
            e.Graphics.DrawImage(brookyln, brooklynRect);
            e.Graphics.DrawImage(manhatten, manhattenRect);
            e.Graphics.DrawImage(stattenIsland, stattenIslandRect);
            e.Graphics.DrawImage(bronx, bronxRect);
            e.Graphics.DrawImage(queens, queensRect);

            //Draw the gradient on the side of the image as a key
            using (LinearGradientBrush linGr = new LinearGradientBrush(
                new Point(0, 0),
                new Point(0, 350),
                Color.Red, Color.Orange))
            {
                ColorBlend blend = new ColorBlend(3);
                blend.Colors = new Color[3] { Color.Red, Color.Orange, Color.Green };
                blend.Positions = new float[3] { 0f, 0.5f, 1f };
                linGr.InterpolationColors = blend;
                //Drawing the rectangles
                e.Graphics.FillRectangle(linGr, 350, 50, 50, 300);
            }
        }
    }
}
