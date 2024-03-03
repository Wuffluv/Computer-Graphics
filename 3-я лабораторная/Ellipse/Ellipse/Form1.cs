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
        private int ellipseWidth; // Ширина 
        private int ellipseHeight; // Высота 

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Paint += pictureBox1_Paint; //обработчик события Paint для изображенияя
        }

        // Обработчик события Paint для изображения
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Рисуем эллипс в pictureBox1
            DrawEllipse(e.Graphics);
        }

        // Метод для рисования эллипса
        private void DrawEllipse(Graphics g)
        {
            // Задаем параметры эллипса
            int x = (pictureBox1.Width - ellipseWidth) / 2;
            int y = (pictureBox1.Height - ellipseHeight) / 2;

            // Рисуем эллипс 
            g.DrawEllipse(Pens.White, x, y, ellipseWidth, ellipseHeight);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем введенные пользователем значения ширины и высоты
            if (int.TryParse(textBox1.Text, out ellipseWidth) && int.TryParse(textBox2.Text, out ellipseHeight))
            {
             
                pictureBox1.Invalidate();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректные значения для ширины и высоты.");
            }
        }
    }
}
