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
    public partial class EnterData : Form
    {
        private string text;
        public EventHandler dataSubmit;
        public EnterData(string text)
        {
            InitializeComponent();
            textLabel.Text = text;
            this.inputBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckKeys);
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            text = inputBox.Text;
            dataSubmit?.Invoke(this, e);
        }
        public string GetText()
        {
            return text;
        }
        private void CheckKeys(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                submitBtn.PerformClick();
            }
        }
    }
}
