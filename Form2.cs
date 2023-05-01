using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormApp
{
    public partial class Form2 : Form
    {
        Bitmap bitmap;
        Graphics graph;
        Pen blackPen;
        Brush brush_red;
        Brush brush_black;

        public Form2()
        {
            InitializeComponent();

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graph = Graphics.FromImage(bitmap);
            blackPen = new Pen(Color.Black);
            blackPen.Width = 2;
            brush_red = Brushes.Crimson;
            brush_black = Brushes.Black;

            pictureBox1.Image = bitmap;

            graph.FillRectangle(brush_red, 0, 0, 50, 50);
            graph.FillRectangle(brush_black, 125, 0, 50, 50);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) Form1.brush_multi = Brushes.Crimson;
            if (radioButton2.Checked) Form1.brush_multi = Brushes.Black;

            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
