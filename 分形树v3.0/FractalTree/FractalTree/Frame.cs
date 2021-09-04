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
using System.Threading;
using System.IO;

namespace FractalTree
{
    
    public partial class Form1 : Form
    {
        bool click1 = false;
        bool click2 = false;
        //文件名
        public string curFileName= @"picture.jpg";
        //图像对象
        private System.Drawing.Bitmap curBitmap;
        //public Bitmap bmp = new Bitmap(769, 650);

        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.Size = new Size(769, 650);
            this.CenterToScreen();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            picturebox.BorderStyle = BorderStyle.Fixed3D;
            this.Text = "分形树";
            curBitmap = (Bitmap)Image.FromFile(curFileName);
          
            //Graphics g = Graphics.FromImage(bmp);
            //picturebox.Image = ;
            //curBitmap = (Bitmap)bmp;
        }

        public void pictureBox_Paint(object sender, PaintEventArgs e)
        {

            //Graphics g = this.CreateGraphics();
            //picturebox.Refresh();
            if (curBitmap != null)
                {
                //使用DrawImage的方法绘制图像
                Graphics g = e.Graphics;
                //curBitmap.Width, curBitmap.Height图像的宽度和高度
                g.DrawImage(curBitmap, 0, 0, curBitmap.Width, curBitmap.Height);
                }
            if (click1)
            {
                //picturebox.Refresh();
                //Graphics g = e.Graphics;
                //if(curFileName!=null)
                curBitmap = (Bitmap)Image.FromFile(curFileName);
                //else
                  //  curBitmap = bmp;
                this.picturebox.Image = curBitmap;
                Graphics g = Graphics.FromImage(this.picturebox.Image);
                Pen pen = new Pen(Color.Black);
                float length = Convert.ToSingle(tb1.Text) * 10;
                double angle = Convert.ToDouble(nud3.Value);
                int depth = Convert.ToInt32(nud1.Value);
                float xo = (picturebox.Width / 2) - 2;
                float yo = picturebox.Height - Convert.ToSingle(5.4);  
                paintFractalTree(pen, xo, yo, depth, 90, length, angle, g);//那个90是确定这颗树往哪个方向开始长的
                pen.Dispose();
                click1 = false;
            }
            else if(click2)
            {
                //picturebox.Refresh();
                if (curFileName != null)
                    curBitmap = (Bitmap)Image.FromFile(curFileName);
                //else
                  //  curBitmap = bmp;
                this.picturebox.Image = curBitmap;
                Graphics g = Graphics.FromImage(this.picturebox.Image);
                //Graphics g = e.Graphics;
                Pen pen = new Pen(Color.Black);
                float length = Convert.ToSingle(tb1.Text) * 10;
                double angle = Convert.ToDouble(nud3.Value);
                int depth = Convert.ToInt32(nud1.Value);
                float xo = (picturebox.Width / 2) - 2;
                float yo = picturebox.Height - Convert.ToSingle(5.4);
                paintFractalTree2(pen, xo, yo, depth, 90, length*1/2, angle, g);//那个90是确定这颗树往哪个方向开始长的
                pen.Dispose();
                click2 = false;
            }
        }

        private void paintFractalTree(Pen pen, float xo, float yo, int depth, double dangle, float length, double angle, Graphics line)
        {
            if (depth > -1)
            {
                length = length * 2 / 3;
                float yf = yo - (sin(Converttohudu(dangle)) * length);//转换成弧度
                float xf = xo - (cos(Converttohudu(dangle)) * length);
                line.DrawLine(pen, xo, yo, xf, yf);
                Random rnd = new Random();
                // 随机色生成
                Color myColor1 = Color.FromArgb
                    (
                         rnd.Next(0, 255), /*红色*/
                         rnd.Next(0, 255), /*绿色*/
                         rnd.Next(0, 255)  /*蓝色*/
                    );

                Pen pen2 = new Pen(myColor1);

                paintFractalTree(pen2, xf, yf, depth - 1, dangle + angle, length, angle, line);//画右分支
                Color myColor2 = Color.FromArgb
                    (
                         rnd.Next(0, 255), /*红色*/
                         rnd.Next(0, 255), /*绿色*/
                         rnd.Next(0, 255)  /*蓝色*/
                    );
                Pen pen3 = new Pen(myColor2);
                
                paintFractalTree(pen3, xf, yf, depth - 1, dangle - angle, length, angle, line);//画左分支
            }
        }
        
        private void paintFractalTree2(Pen pen, float xo, float yo, int depth, double dangle, float length, double angle, Graphics line)
        {
            double attach = 50;
            if (depth > -1)
            {
                
                float changelength = length * 2 / 3;
                float x1 = xo - (cos(Converttohudu(dangle))) * length;
                float y1 = yo- (sin(Converttohudu(dangle)))*length;
                float x2 = x1 - (cos(Converttohudu(dangle))) * changelength;
                float y2 = y1 - (sin(Converttohudu(dangle))) * changelength;
                float x2r = x1 - (cos(Converttohudu(dangle+angle))) * changelength;
                float y2r = y1 - (sin(Converttohudu(dangle+angle))) * changelength;
                float x2l = x1 - (cos(Converttohudu(dangle -angle))) * changelength;
                float y2l = y1 - (sin(Converttohudu(dangle - angle))) * changelength;
                line.DrawLine(pen, xo, yo, x1, y1);
                //line.DrawLine(pen, x1, y1, x2, y2);
                line.DrawLine(pen, x1, y1, x2r, y2r);
                //line.DrawLine(pen, x1, y1, x2l, y2l);
                
               
                Random rnd = new Random();
                // 随机色生成
                Color myColor1 = Color.FromArgb
                    (
                         rnd.Next(0, 255), /*红色*/
                         rnd.Next(0, 255), /*绿色*/
                         rnd.Next(0, 255)  /*蓝色*/
                    );

                Pen pen2 = new Pen(myColor1);
                paintFractalTree2(pen2, x2r, y2r, depth - 1, dangle + angle*3/2 + angle, length*2/3, angle, line);//画右分支
                paintFractalTree2(pen2, x2r, y2r, depth - 1, dangle + angle * 3 / 2 - angle, length*2/3, angle, line);//画右分支
                //paintFractalTree2(pen2, x2l, y2l, depth - 1, dangle - angle * 3 / 2 - angle, length*2/3, angle, line);//画右分支
                //paintFractalTree2(pen2, x2l, y2l, depth - 1, dangle - angle * 3 / 2 + angle, length * 2 / 3, angle, line);//画右分支

                Color myColor2 = Color.FromArgb
                    (
                         rnd.Next(0, 255), /*红色*/
                         rnd.Next(0, 255), /*绿色*/
                         rnd.Next(0, 255)  /*蓝色*/
                    );
                Pen pen3 = new Pen(myColor2);
              
            }
        }

        private float cos(double angle)
        {
            return (float)Math.Cos(angle);
        }//求cos的值

        private float sin(double angle)
        {
            return (float)Math.Sin(angle);
        }//求sin的值

        private double Converttohudu(double angle)
        {
            return (Math.PI * angle) / 180;
        }//将角度转换成弧度

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar) && !e.KeyChar.Equals(','))
            {
                e.Handled = true;//可以从文本框中获取键盘读入的数据
            }
        }

        private void drawbt_Click(object sender, EventArgs e)
        {
            click1 = true;
            picturebox.Refresh();
        }

        private void nud3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void nud1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnDlg = new OpenFileDialog();
            opnDlg.Filter = "所有图像文件 | *.bmp; *.pcx; *.png; *.jpg; *.gif;" +
                "*.tif; *.ico; *.dxf; *.cgm; *.cdr; *.wmf; *.eps; *.emf|" +
                "位图( *.bmp; *.jpg; *.png;...) | *.bmp; *.pcx; *.png; *.jpg; *.gif; *.tif; *.ico|" +
                "矢量图( *.wmf; *.eps; *.emf;...) | *.dxf; *.cgm; *.cdr; *.wmf; *.eps; *.emf";
            opnDlg.Title = "打开图像文件";
            opnDlg.ShowHelp = true;
            if (opnDlg.ShowDialog() == DialogResult.OK)
            {
                 curFileName = opnDlg.FileName;
                try
                {
                    curBitmap = (Bitmap)Image.FromFile(curFileName);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
            //对窗体进行重新绘制，这将强制执行paint事件处理程序
            Invalidate();
            picturebox.Refresh();


        }
        //private void Form1_Paint(object sender, PaintEventArgs e)
        //{
        //    //使用窗体的Paint事件的PaintEventArgs属性来获取一个与窗体相关联的Graphic对象。
        //    Graphics g = e.Graphics;
        //    if (curBitmap != null)
        //    {
        //        //使用DrawImage的方法绘制图像
        //        //160,20 :显示在主窗体内，图像左上角的坐标
        //        //curBitmap.Width, curBitmap.Height图像的宽度和高度
        //        g.DrawImage(curBitmap, 160, 20, curBitmap.Width, curBitmap.Height);
        //    }
        //}


        public void 保存ToolStripMenuItem_Click(object sender, EventArgs e ){ }
        private void button1_Click(object sender, EventArgs e)
        {
            click2 = true;
            picturebox.Refresh();
        }

        private void 保存ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
            if (curBitmap == null)
            {
                
                return;
            }
            Graphics g = Graphics.FromImage(curBitmap);
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Title = "保存为";
            saveDlg.OverwritePrompt = true;
            saveDlg.Filter =
                "BMP文件 (*.bmp) | *.bmp|" +
                "Gif文件 (*.gif) | *.gif|" +
                "JPEG文件 (*.jpg) | *.jpg|" +
                "PNG文件 (*.png) | *.png";
            saveDlg.ShowHelp = true;
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveDlg.FileName;
                string strFilExtn = fileName.Remove(0, fileName.Length - 3);
                switch (strFilExtn)
                {
                    case "bmp":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "gif":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "tif":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Tiff);
                        break;
                    case "png":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        break;
                }
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curBitmap == null)
            {

                return;
            }
            Graphics g = Graphics.FromImage(curBitmap);
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Title = "保存为";
            saveDlg.OverwritePrompt = true;
            saveDlg.Filter =
                "BMP文件 (*.bmp) | *.bmp|" +
                "Gif文件 (*.gif) | *.gif|" +
                "JPEG文件 (*.jpg) | *.jpg|" +
                "PNG文件 (*.png) | *.png";
            saveDlg.ShowHelp = true;
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveDlg.FileName;
                string strFilExtn = fileName.Remove(0, fileName.Length - 3);
                switch (strFilExtn)
                {
                    case "bmp":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "gif":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "tif":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Tiff);
                        break;
                    case "png":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        break;
                }
            }
        }
    }
    }


