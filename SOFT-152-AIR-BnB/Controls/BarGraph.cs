using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SOFT_152_AIR_BnB
{
    public partial class BarGraph : UserControl
    {
        private const int barWidth = 100, spacing = 20, padding = 50, baseHeight = 20;
        private double[] dataPoints;
        private string[] labels;
        private readonly string title;
        private Rectangle[] rectangles;
        private Bitmap image;
        private double highest = 1, lowest = 10000;
        public EventHandler processed;
        public BarGraph(int height, string name)
        {
            InitializeComponent();
            this.Width = padding + spacing * 2; //Wide enough for one bar, will be extended when more bars are added
            this.Height = height - 17; // -17 to allow for the scrollbar
            dataPoints = new double[0];
            rectangles = new Rectangle[0];
            labels = new string[0];
            title = name;
        }
        public void Add(double input, string inLabel)
        {
            if (input > highest)
            {
                highest = input;
            }
            if (input < lowest)
            {
                lowest = input;
            }
            Array.Resize(ref dataPoints, dataPoints.Length + 1);
            Array.Resize(ref rectangles, rectangles.Length + 1);
            Array.Resize(ref labels, labels.Length + 1);
            dataPoints[dataPoints.Length - 1] = input;
            labels[labels.Length - 1] = inLabel;
            this.Width = this.Width + spacing + barWidth;
        }
        public void CalculateBars()
        {
            double scaleFactor = (Height - padding * 2 - baseHeight) / 100;
            for (int i = 0; i < dataPoints.Length; i++)
            {
                if(dataPoints.Length == 1)
                {
                    rectangles[0] = new Rectangle(
                        padding + spacing + spacing, //X
                        padding + 2, //y
                        barWidth, //width
                        baseHeight); //height
                }
                else
                {
                    rectangles[i] = new Rectangle(
                        padding + spacing + (spacing * (i)) + (barWidth * (i)) + 2, //X
                        padding + 2, //y
                        barWidth, //width
                        Convert.ToInt32(Math.Floor(((dataPoints[i] - lowest) / (highest - lowest) * 100) * scaleFactor)) + baseHeight); //height
                }
            }
            DrawGraph();
        }
        private void DrawGraph()
        {
            image  = new Bitmap(Width, Height);
            using(Graphics g = Graphics.FromImage(image))
            using(Pen pen = new Pen(Color.Black, 3))
            using(SolidBrush brush = new SolidBrush(Color.Black))
            {
                //Y axis
                g.DrawLine(pen, padding, padding, padding, this.Height - padding);
                //X axis
                g.DrawLine(pen, padding, padding, this.Width, padding);
                foreach (Rectangle rect in rectangles)
                {
                    g.FillRectangle(brush, rect);
                }
                image.RotateFlip(RotateFlipType.Rotate180FlipX);
                Font drawFont = new Font("Microsoft Sans Serif", 16);
                for (int i = 0; i < labels.Length; i++)
                {
                    if (labels[i].Length > 9)
                    {
                        g.DrawString(labels[i].Substring(0, 9), drawFont, brush,
                        padding + spacing + (spacing * (i)) + (barWidth * (i)) + 2,//X
                        Height - padding); //y
                        g.DrawString(labels[i].Substring(9), drawFont, brush,
                        padding + spacing + (spacing * (i)) + (barWidth * (i)) + 2,//X
                        Height - padding + 20); //y
                    }
                    else
                    {
                        g.DrawString(labels[i], drawFont, brush,
                            padding + spacing + (spacing * (i)) + (barWidth * (i)) + 2,//X
                            Height - padding); //y
                    }
                }
                g.DrawString(String.Format("{0}", title), drawFont, brush, padding, 20);
                g.DrawString("0", drawFont, brush, 0, Height - padding - 20);
                g.DrawString(Convert.ToString(lowest), drawFont, brush, 0, Height - padding - baseHeight - 20);
                g.DrawString(Convert.ToString(highest), drawFont, brush, 0, padding + 20);
                processed(this, EventArgs.Empty);
                imageBox.Image = image;
            }
            //Save the image for debug purposese
            //image.Save(String.Format("bar.png"));
        }
    }
}
