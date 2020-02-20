using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOFT_152_AIR_BnB
{
    public partial class CreateNewProperty : Form
    {
        public EventHandler submit;
        private string[] text;
        public CreateNewProperty()
        {
            InitializeComponent();
        }
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Util.IsNumeric(propIdText.Text))
                {
                    MessageBox.Show("Property ID MUST  be a numer");
                    return;
                }
                else if (!Util.IsNumeric(hostIdText.Text))
                {
                    MessageBox.Show("Host ID MUST  be a numer");
                    return;
                }
                else if (!Util.IsDouble(latText.Text))
                {
                    MessageBox.Show("Latitude MUST  be a numer");
                    return;
                }
                else if (!Util.IsDouble(lonText.Text))
                {
                    MessageBox.Show("Longitude MUST  be a numer");
                    return;
                }
                else if (!Util.IsDouble(priceText.Text.Replace('$', ' ')))
                {
                    MessageBox.Show("Price MUST  be a numer");
                    return;
                }
                else if (!Util.IsNumeric(minNoNightsText.Text))
                {
                    MessageBox.Show("Minimum number of nights MUST  be a numer");
                    return;
                }
                else if (!Util.IsNumeric(avText.Text))
                {
                    MessageBox.Show("Avalibility MUST  be a numer");
                    return;
                }
                //366 as could be leap year
                else if (Convert.ToInt32(avText.Text) > 366)
                {
                    MessageBox.Show("Avalibility MUST be less than 366");
                    return;
                }
            }
            catch (System.OverflowException)
            {
                MessageBox.Show("A number you have inputted is too large!");
                return;
            }
            text = new string[]
            {
                propNameText.Text,
                propIdText.Text,
                hostNameText.Text,
                hostIdText.Text,
                latText.Text,
                lonText.Text,
                priceText.Text,
                minNoNightsText.Text,
                avText.Text,
                roomTypeText.Text
            };
            if (CheckTextBoxes())
            {
                submit?.Invoke(this, e);
                this.Close();
            }
            else MessageBox.Show("No field can be left empty!");
        }
        private bool CheckTextBoxes()
        {
            foreach (string line in text)
            {
                if (line == "") return false;
            }
            return true;

        }
        public string[] GetText()
        {
            return text;
        }
    }
}
