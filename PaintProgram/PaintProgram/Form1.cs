using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintProgram
{
    public partial class Form1 : Form
    {
        enum araclar
        {
            kalem, 
            daire,
            dikdortgen
        }

        Image img;
        Bitmap kalem;
        Color grenk = Color.Blue;
        Graphics g;
        int sizeX = 2;
        int sizeY = 2;
        bool durum = false;
        araclar secili = araclar.kalem;
        Point bas, bit;
        public Form1()
        {
            InitializeComponent();
            img = new Bitmap(640,480);
            pictureBox1.Image = img;
            g = Graphics.FromImage(img);
            beyazlat(img, 640, 480);

            kalem = new Bitmap(sizeX,sizeY);

            trackBar1.Minimum = 2;
            trackBar1.Maximum = 32;

        }

        private void beyazlat(Image img, int v1, int v2)
        {
            Bitmap k = new Bitmap(v1,v2);
            for (int i = 0; i < v1; i++)
                for (int j = 0; j < v2; j++)
                {
                    k.SetPixel(i, j, Color.White);
                }
            g.DrawImageUnscaled(k, 0, 0);
            pictureBox1.Image = img;

        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (secili)
            {
                case araclar.kalem: 

                if (e.Button == MouseButtons.Left)
                {
                    durum = true;
                }
                    break;
                case araclar.daire:
                    bas = e.Location;
                    break;
                case araclar.dikdortgen:
                    bas = e.Location;

                    break;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (secili)
            {
                case araclar.kalem:

                    if (e.Button == MouseButtons.Left)
                        {
                            durum = false;
                        }
                    break;
                case araclar.daire:
                    bit = e.Location;

                    DaireCiz(g,bas,bit);
                    
                    pictureBox1.Image = img;
                    break;
                case araclar.dikdortgen:
                    bit = e.Location;
                    DikdortgenCiz(g, bas, bit);

                    pictureBox1.Image = img;

                    break;
            }
        }

        private void DikdortgenCiz(Graphics g, Point bas, Point bit)
        {
            Pen p = new Pen(grenk);
            int x, y;
            x = bas.X;
            if (x > bit.X) x = bit.X;
            y = bas.Y;
            if (y > bit.Y) x = bit.Y;

            g.DrawRectangle(p,x,y,Math.Abs(bit.X-bas.X),Math.Abs(bit.Y- bas.Y));
        }

        private void DaireCiz(Graphics g, Point bas, Point bit)
        {
            Pen p = new Pen(grenk);
            p.Width = sizeX;
            g.DrawEllipse(p,bas.X,bas.Y,bit.X - bas.X, bit.Y- bas.Y);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            switch (secili)
            {
                case araclar.kalem:

                    if (e.Button == MouseButtons.Left)
                    {
                        CizgiCiz(g, e.Location.X, e.Location.Y,grenk);
                    }
                    break;
                case araclar.daire:
                    break;
                case araclar.dikdortgen:
                    break;
            }
        }

        private void CizgiCiz(Graphics g, int x, int y,Color c)
        {
            if (durum)
            {
                for(int i=0;i<sizeX;i++)
                    for(int j=0;j<sizeY;j++)
                    {
                        kalem.SetPixel(i, j, c);
                    }
                g.DrawImageUnscaled(kalem,x,y);
                pictureBox1.Image = img;
            }

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value+"";
            sizeX = trackBar1.Value;
            sizeY = trackBar1.Value;
            kalem = new Bitmap(sizeX, sizeY);

        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img.Save("test.jpg",ImageFormat.Jpeg);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sonuc = colorDialog1.ShowDialog();

            if (sonuc == DialogResult.OK)
            {
                grenk = colorDialog1.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            secili = araclar.kalem;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            secili = araclar.daire;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            secili = araclar.dikdortgen;
        }
    }
}
