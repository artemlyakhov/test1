using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MConverter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            mask1.Text = Directory.GetCurrentDirectory() + "\\xa*.bmp";

            int count = BmpPngConverter.Count();

            Show();

            if (count > 0)
            {
                int sqrt = (int)Math.Sqrt((double)count);
                if ((double)sqrt == Math.Sqrt((double)count))
                {
                    x1.Text = y1.Text = sqrt.ToString();
                }
                else
                {
                    x1.Text = count.ToString();
                    y1.Text = "1";
                }

                //convert1.Focus();
                convert1.PerformClick();
            }

        }

// change
        private void convert1_Click(object sender, EventArgs e)
        {
            BmpPngConverter.Convert(int.Parse(x1.Text), int.Parse(y1.Text));

            //MessageBox.Show("Done");

            Application.Exit();
        }

    }
}