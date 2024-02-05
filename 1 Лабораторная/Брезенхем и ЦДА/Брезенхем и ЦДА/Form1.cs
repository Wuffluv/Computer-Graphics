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
        // Переменные для хранения координат
        private int x1, y1, x2, y2;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Обработчик события для pictureBox1 (алгоритм Брезенхема)
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // При нажатии кнопки "Нарисовать"

            // Парсим введенные координаты
            if (int.TryParse(textBox1.Text, out x1) && int.TryParse(textBox2.Text, out y1) &&
                int.TryParse(textBox3.Text, out x2) && int.TryParse(textBox4.Text, out y2))
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

            // Создаем новый объект Bitmap для хранения изображения
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Вызываем метод для рисования линии по алгоритму Брезенхема
                BresenhamLine(x1, y1, x2, y2, g);
            }
            // Отображаем изображение в pictureBox1
            pictureBox1.Image = bmp;
        }

        private void DrawDDALine()
        {
            // Рисуем линию по методу ЦДА

            // Создаем новый объект Bitmap для хранения изображения
            Bitmap bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Вызываем метод для рисования линии по методу ЦДА
                DDALine(x1, y1, x2, y2, g);
            }
            // Отображаем изображение в pictureBox2
            pictureBox2.Image = bmp;
        }

        private void BresenhamLine(int x1, int y1, int x2, int y2, Graphics g)
        {
            // Реализация алгоритма Брезенхема для рисования линии

            // Вычисляем разницу по x и y
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            // Устанавливаем направление отрисовки по x и y
            int sx = (x1 < x2) ? 1 : -1;
            int sy = (y1 < y2) ? 1 : -1;

            // Вычисляем ошибку
            int err = dx - dy;

            // Основной цикл отрисовки
            while (true)
            {
                // Закрашиваем текущий пиксель
                g.FillRectangle(Brushes.Black, x1, y1, 1, 1);

                // Если достигли конечной точки, выходим из цикла
                if (x1 == x2 && y1 == y2)
                    break;

                // Вычисляем ошибку для следующего шага
                int e2 = 2 * err;

                // Перемещаемся по оси x при необходимости
                if (e2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }

                // Перемещаемся по оси y при необходимости
                if (e2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void DDALine(int x1, int y1, int x2, int y2, Graphics g)
        {
            // Реализация метода ЦДА для рисования линии

            // Вычисляем разницу по x и y
            int dx = x2 - x1;
            int dy = y2 - y1;

            // Находим максимальное количество шагов
            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));

            // Вычисляем инкременты по x и y
            float xIncrement = dx / (float)steps;
            float yIncrement = dy / (float)steps;

            // Инициализируем начальные значения
            float x = x1;
            float y = y1;

            // Основной цикл отрисовки
            for (int i = 0; i <= steps; i++)
            {
                // Закрашиваем текущий пиксель
                g.FillRectangle(Brushes.Black, (int)x, (int)y, 1, 1);

                // Обновляем координаты для следующего шага
                x += xIncrement;
                y += yIncrement;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Обработчик события изменения текста в textBox2
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Обработчик события для pictureBox2 (метод ЦДА)
        }
    }
}
