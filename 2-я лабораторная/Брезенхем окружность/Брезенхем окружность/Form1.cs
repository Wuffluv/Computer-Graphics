using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Брезенхем_окружность
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Получаем введенный радиус из textBox1
            if (int.TryParse(textBox1.Text, out int radius))
            {
                // Вызываем метод Брезенхема для отрисовки окружности
                DrawBresenhamCircle(radius);
            }
            else
            {
                MessageBox.Show("Введите корректное число для радиуса.");
            }
        }

        // Реализация метода построения окружности
        private void DrawBresenhamCircle(int radius)
        {
            // Получаем графический контекст для pictureBox1
            Graphics g = pictureBox1.CreateGraphics();

            // Центр окружности (половина ширины и высоты pictureBox1)
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;

            int x = radius;
            int y = 0;
            int error = 0;

            while (x >= y)
            {
                // Рисуем по алгоритму
                g.FillRectangle(Brushes.Black, centerX + x, centerY - y, 1, 1);
                g.FillRectangle(Brushes.Black, centerX - x, centerY - y, 1, 1);
                g.FillRectangle(Brushes.Black, centerX + y, centerY - x, 1, 1);
                g.FillRectangle(Brushes.Black, centerX - y, centerY - x, 1, 1);

                g.FillRectangle(Brushes.Black, centerX + x, centerY + y, 1, 1);
                g.FillRectangle(Brushes.Black, centerX - x, centerY + y, 1, 1);
                g.FillRectangle(Brushes.Black, centerX + y, centerY + x, 1, 1);
                g.FillRectangle(Brushes.Black, centerX - y, centerY + x, 1, 1);

                //переходим к следующей горизонтальной линии, где должны быть отрисованы точки окружности
                y++;

                // Обновляем ошибку
                if (error <= 0)
                {
                    error += 2 * y + 1;
                }
                else
                {
                    x--;
                    error += 2 * (y - x) + 1;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
