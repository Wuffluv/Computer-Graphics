using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ellipse
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private PointF center;
        private float width;
        private float height;
        private float angle;
        public Form1()
        {
            InitializeComponent();
            center = new PointF(pictureBox1.Width / 2, pictureBox1.Height / 2);
            width = 100;
            height = 50;
            angle = 30; // Угол в градусах
        }



        private void DrawEllipse(PointF center, float width, float height, float angle)
        {
            float angleInRadians = angle * (float)Math.PI / 180;
            float cosAngle = (float)Math.Cos(angleInRadians);
            float sinAngle = (float)Math.Sin(angleInRadians);

            // Создаем битмап для рисования
            using (Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height))
            {
                // Получаем Graphics изображения
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    // Очищаем изображение
                    g.Clear(Color.White);

                    // Рисуем эллипс
                    using (Pen pen = new Pen(Color.Black))
                    {
                        for (float t = 0; t <= 2 * Math.PI; t += 0.01f)
                        {
                            float x = center.X + width / 2 * (float)Math.Cos(t) * cosAngle - height / 2 * (float)Math.Sin(t) * sinAngle;
                            float y = center.Y + width / 2 * (float)Math.Cos(t) * sinAngle + height / 2 * (float)Math.Sin(t) * cosAngle;
                            g.DrawEllipse(pen, x, y, 1, 1);
                        }
                    }
                }

                // Устанавливаем изображение в PictureBox
                pictureBox1.Image = bitmap;
            }
        }


            private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Обработка нажатия кнопки для построения эллипса
            if (float.TryParse(textBox1.Text, out width) &&
                float.TryParse(textBox2.Text, out height) &&
                float.TryParse(textBox3.Text, out angle))
            {
                DrawEllipse(center, width, height, angle);
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректные значения ширины, высоты и угла.");
            }
        }
    }
}
