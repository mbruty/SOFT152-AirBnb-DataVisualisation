using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOFT_152_AIR_BnB
{
    public partial class CustomListBox : UserControl
    {
        private readonly int size = 28;
        public EventHandler IndexChanged, doubleClick, dataSubmitted;
        private readonly EnterData input;
        private readonly string leftHeader, rightHeader;
        private bool ignoreIndexChanged = false;
        private string side;
        public CustomListBox(string inputLeft, string inputRight)
        {
            InitializeComponent();
            leftHeader = inputLeft;
            rightHeader = inputRight;
            leftBox.Items.Add(leftHeader);
            rightBox.Items.Add(rightHeader);
            leftBox.AutoSize = true;
            rightBox.AutoSize = true;
        }
        public void SetIndex(int index)
        {
            leftBox.SelectedIndex = index;
            rightBox.SelectedIndex = index;
        }
        public int GetIndex()
        {
            if(leftBox.SelectedIndex != 0)
            {
                return Convert.ToInt32(leftBox.SelectedIndex) - 1;
                // -1 to ignore the header
            }
            return 0;
        }
        public int SearchIndex(string name)
        {
            //Used for setting the host index based by name as you can't know the possible index of the host like everything else
            for(int i = 0; i <= leftBox.Items.Count; i++)
            {
                if(Convert.ToString(leftBox.Items[i]) == name)
                {
                    return i;
                }
            }
            return 0;
        }
        public void Clear()
        {
            ignoreIndexChanged = true;
            //Clear all the items
            leftBox.Items.Clear();
            rightBox.Items.Clear();
            //Add the headers
            leftBox.Items.Add(leftHeader);
            rightBox.Items.Add(rightHeader);
            //Resize back to default
            leftBox.Size = new Size(175, 28);
            rightBox.Size = new Size(153, 28);
            this.Size = new Size(330, 34);
        }

        public void AllowChange() => ignoreIndexChanged = false;
        public void AddLeft(string input)
        {
            leftBox.Size = new Size(leftBox.Size.Width, leftBox.Size.Height + size);
            leftBox.Items.Add(input);
            rightBox.Location = new Point(leftBox.Size.Width, 0);
        }
        public void AddRight(string input)
        {
            rightBox.Size = new Size(rightBox.Size.Width, rightBox.Size.Height + size);
            rightBox.Items.Add(input);
        }
        public bool Has(string input)
        {
            foreach(string name in leftBox.Items)
            {
                if(input == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void LeftBox_DoubleClick(object sender, EventArgs e)
        {
            side = "left";
            //Bubble the event up the chain
            doubleClick?.Invoke(this, e);
        }
        private void DataEnter(object sender, EventArgs e)
        {
            //Get rid of the data entry form
            input.Dispose();
            //Bubble the event up the chain
            dataSubmitted?.Invoke(this, e);
        }
        private void RightBox_DoubleClick(object sender, EventArgs e)
        {
            string rightTop = Convert.ToString(rightBox.Items[0]).Substring(0,2) ;
            if (rightTop == "No")
            {
                //Cannot edit field in the number column as this will mess up the reaing and writing for the file
                MessageBox.Show("You cannot edit this field");
            }
            else
            {
                side = "right";
                doubleClick?.Invoke(this, e);
            }
        }

        private void RightBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ignoreIndexChanged)
            {
                //Not allowing the top row to be selected
                if(rightBox.SelectedIndex == 0)
                {
                    rightBox.SelectedIndex = 1;
                }
                leftBox.SelectedIndex = rightBox.SelectedIndex;
                //Sending the event up to the form
                IndexChanged?.Invoke(this, e);
            }
        }

        private void LeftBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ignoreIndexChanged)
            {
                //Not allowing the top row to be selected
                if (leftBox.SelectedIndex == 0)
                {
                    leftBox.SelectedIndex = 1;
                }
                rightBox.SelectedIndex = leftBox.SelectedIndex;
                //Sending the event up to the form
                IndexChanged?.Invoke(this, e);
            }
        }
        public string GetSelectedText()
        {
            //ternary operator that returns the selected item if side == left, else returns the selected items on the right
            return side == "left" ? String.Format("{0}: {1}", leftBox.Items[0], leftBox.Items[leftBox.SelectedIndex]) :
                String.Format("{0}: {1}", rightBox.Items[0], rightBox.Items[rightBox.SelectedIndex]);
        }
        public string GetLeftText()
        {
            return Convert.ToString(leftBox.Items[leftBox.SelectedIndex]);
        }
        public string GetSide()
        {
            return side;
        }
    }
}
