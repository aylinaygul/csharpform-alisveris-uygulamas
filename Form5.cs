using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form5 : UserControl
    {
        public Form5()
        {
            InitializeComponent();
            
        }
        public string img1
        {
            get
            {
                return pictureBox1.ImageLocation;
            }

            set
            {
                pictureBox1.ImageLocation = value;
            }

        }
        public Image img
        {
            get
            {
                return pictureBox1.Image;
            }

            set
            {
                pictureBox1.Image = value;
            }

        }

        public string label
        {
            get
            {
                return label1.Text;
            }

            set
            {
                label1.Text = value;
            }

        }
        public string labelx
        {
            get
            {
                return label3.Text;
            }

            set
            {
                label3.Text = value;
            }
        }

        public string price
        {
            get
            {
                return label2.Text;
            }

            set
            {
                label2.Text = value;
            }

        }

    }
}
