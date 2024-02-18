using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Брезенхем_и_ЦДА
{
    public partial class Form1 : Form
    {
        private int x1, y1, x2, y2, x3, y3, x4, y4;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Обработчик события изменения текста в textBox1
            if (textBox1.Text == "Введите X")
            {
                textBox1.Text = "";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Обработчик события изменения текста в textBox2
            if (textBox2.Text == "Введите Y")
            {
                textBox2.Text = "";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // Обработчик события изменения текста в textBox3
            if (textBox3.Text == "Введите X")
            {
                textBox3.Text = "";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // Обработчик события изменения текста в textBox4
            if (textBox4.Text == "Введите Y")
            {
                textBox4.Text = "";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            // Обработчик события изменения текста в textBox5
            if (textBox5.Text == "Введите X")
            {
                textBox5.Text = "";
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            // Обработчик события изменения текста в textBox6
            if (textBox6.Text == "Введите Y")
            {
                textBox6.Text = "";
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            // Обработчик события изменения текста в textBox7
            if (textBox7.Text == "Введите X")
            {
                textBox7.Text = "";
            }
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            // Обработчик события изменения текста в textBox8
            if (textBox8.Text == "Введите Y")
            {
                textBox8.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // При нажатии кнопки "Нарисовать"

            // Парсим введенные координаты
            if (int.TryParse(textBox1.Text, out x1) && int.TryParse(textBox2.Text, out y1) &&
                int.TryParse(textBox3.Text, out x2) && int.TryParse(textBox4.Text, out y2) &&
                int.TryParse(textBox5.Text, out x3) && int.TryParse(textBox6.Text, out y3) && 
                int.TryParse(textBox7.Text, out x4) && int.TryParse(textBox8.Text, out y4)
                )
            {
                // Рисуем линию по алгоритму Брезенхема в PictureBox1
                DrawBresenhamLine();

                // Рисуем линию по методу ЦДА в PictureBox2
                DrawDDALine();
            }
            else
            {
                // Выводим сообщение об ошибке в случае некорректных координат
                MessageBox.Show("Введите корректные координаты");
            }
        }

        private void DrawBresenhamLine()
        {
            // Рисуем линию по алгоритму Брезенхема

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                BresenhamLine(x1, y1, x2, y2, g);
            }
            pictureBox1.Image = bmp;
        }

        private void DrawDDALine()
        {
            // Рисуем линию по методу ЦДА

            Bitmap bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                DDALine(x3, y3, x4, y4, g);
            }
            pictureBox2.Image = bmp;
        }

        // Рисование линии по алгоритму Брезенхема
        private void BresenhamLine(int x1, int y1, int x2, int y2, Graphics g)
        {
            // Вычисление разницы по x и y
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            // Установка направления отрисовки по x и y
            int sx = (x1 < x2) ? 1 : -1;
            int sy = (y1 < y2) ? 1 : -1;
            // Вычисление ошибки        
            int err = dx - dy;
            // Основной цикл отрисовки
            while (true)
            {
                // Закрашивание текущего пикселя
                g.FillRectangle(Brushes.Black, x1, y1, 1, 1);

                // Проверка достижения конечной точки
                if (x1 == x2 && y1 == y2)
                    break;

                // Вычисление ошибки для следующего шага
                int e2 = 2 * err;

                // Перемещение по оси x при необходимости
                if (e2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }

                // Перемещение по оси y при необходимости
                if (e2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }

        // Рисование линии по методу ЦДА
        private void DDALine(int x3, int y3, int x4, int y4, Graphics g)
        {
            // Вычисление разницы по x и y
            int dx = x4 - x3;
            int dy = y4 - y3;

            // Нахождение максимального количества шагов
            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));

            // Вычисление инкрементов по x и y
            float xIncrement = dx / (float)steps;
            float yIncrement = dy / (float)steps;

            // Инициализация начальных значений
            float x = x3;
            float y = y3;

            // Основной цикл отрисовки
            for (int i = 0; i <= steps; i++)
            {
                // Закрашивание текущего пикселя
                g.FillRectangle(Brushes.Black, (int)x, (int)y, 1, 1);

                // Обновление координат для следующего шага
                x += xIncrement;
                y += yIncrement;
            }
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
           

        }
    }
}
