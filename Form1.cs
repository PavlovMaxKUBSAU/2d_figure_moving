using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace WinFormApp
{
    public partial class Form1 : Form
    {
        Form2 form2;

        Bitmap bitmap;
        Pen blackPen;
        Pen redPen;
        Pen darkGoldPen;
        Graphics graph;
        Font font;
        public static Brush brush_multi;
        Brush brush_brown;
        LinearGradientBrush GradBrush;
        Rectangle rect1;

        float x = 99, y = 400; //позиция фигуры
        int dx=0, dy=-1; //смещение при перерисовке
        float koef_xy = 1; //коэф скорости фигуры, чтобы сделать плавное (с частотой не более 30Гц) перемещение фигуры, даже если требуется скорость более 30 пикс/сек

        public Form1()
        {
            InitializeComponent();

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graph = Graphics.FromImage(bitmap);
            blackPen = new Pen(Color.Black);
            blackPen.Width = 2;
            redPen = new Pen(Color.Red);
            redPen.Width = 2;
            darkGoldPen = new Pen(Color.DarkGoldenrod);
            darkGoldPen.Width = 3;
            font = new Font("Arial", 15);
            brush_multi = Brushes.Crimson;
            brush_brown = Brushes.SaddleBrown;
            GradBrush = new LinearGradientBrush(new PointF(0, 0), new PointF(100, 100), Color.White, Color.Gainsboro);

            clearPictureBox1();
            pictureBox1.Image = bitmap;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            DrawFigure();
        }

        void DrawFigure()
        {
            rect1 = new Rectangle(Convert.ToInt32(Math.Floor(x))-60, Convert.ToInt32(Math.Floor(y)) - 60, 60, 60);

            graph.FillRectangle(brush_multi, rect1);
            graph.DrawRectangle(darkGoldPen, rect1);


            Point[] points1 = {
                new Point(rect1.X, rect1.Y + 90),
                new Point(rect1.X, rect1.Y - 30),
                new Point(rect1.X - 40, rect1.Y + rect1.Height/2)
            };
            
            Point[] points2 = {
                new Point(rect1.X + rect1.Width, rect1.Y + 90),
                new Point(rect1.X + rect1.Width, rect1.Y - 30),
                new Point(rect1.X + rect1.Width + 40, rect1.Y + rect1.Height/2)
            };

            graph.FillPolygon(brush_multi, points1); //создание треугольника
            graph.FillPolygon(brush_multi, points2);

            pictureBox1.Image = bitmap;
        }

        void clearPictureBox1()
        {
            graph.Clear(Color.White);
            graph.FillRectangle(GradBrush, 0, 0, 1200, 800);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs mouse)
        {
            if(rect1.Contains(mouse.X, mouse.Y))
            {
                form2 = new Form2();
                form2.ShowDialog();
            }
        }

        private void trackBar1_updated(object sender, EventArgs e)
        {
            int interval_real = 100 - trackBar1.Value;

            if (interval_real < 33)
            {
                timer1.Interval = 33;
                koef_xy = 33 / interval_real;
            }
            else
            {
                timer1.Interval = interval_real;
                koef_xy = 1;
            }
        }

        private void button1_Click(object sender, EventArgs e) => timer1.Enabled = true;

        private void timer1_tick(object sender, EventArgs e)
        {
            if (x < 100 && y > bitmap.Height - 100)
            {
                dx = 0;
                dy = -1;
            }
            if (x < 100 && y < 100)
            {
                dx = 1;
                dy = 0;
            }
            if (x > bitmap.Width - 50 && y < 100)
            {
                dx = 0;
                dy = 1;
            }
            if (x > bitmap.Width - 50 && y > bitmap.Height - 100)
            {
                dx = -1;
                dy = 0;
            }

            x += dx*koef_xy;
            y += dy*koef_xy;

            clearPictureBox1();
            DrawFigure();
        }
    }
}