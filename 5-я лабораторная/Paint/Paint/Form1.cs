using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        // Перечисление для определения текущего действия мыши
        private enum MouseAction { Draw, Fill };

        // Текущее действие мыши и цвет по умолчанию
        private MouseAction currentAction = MouseAction.Draw;
        private Color currentColor = Color.Black;

        // Точка начала рисования, флаг рисования и графика для рисования
        private Point startPoint;
        private bool drawing = false;
        private Graphics graphics;
        private Bitmap bitmap;

        public Form1()
        {
            InitializeComponent();
            InitializePictureBox();
        }

        // Инициализация PictureBox и привязка обработчиков событий
        private void InitializePictureBox()
        {
            // Создание нового изображения для PictureBox
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            // Получение объекта Graphics для рисования на изображении
            graphics = Graphics.FromImage(bitmap);

            // Установка изображения для PictureBox и очистка его фона
            pictureBox1.Image = bitmap;
            graphics.Clear(Color.White);

            // Привязка обработчиков событий мыши
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
        }

        // Обработчик события отпускания кнопки мыши
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false; // Остановка рисования
        }

        // Обработчик события нажатия кнопки мыши
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentAction == MouseAction.Draw)
            {
                drawing = true; // Начало рисования
                startPoint = e.Location; // Запоминаем начальную точку
            }
            else if (currentAction == MouseAction.Fill)
            {
                FillRegion(e.Location, currentColor); // Заполнение области цветом
            }
        }

        // Обработчик события перемещения мыши
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // Если идет рисование и выбрано действие "рисование"
            if (drawing && currentAction == MouseAction.Draw)
            {
                // Создание пера для рисования линии
                Pen pen = new Pen(currentColor, 2);

                // Рисование линии между предыдущей и текущей точками
                graphics.DrawLine(pen, startPoint, e.Location);

                // Обновление начальной точки для следующей линии
                startPoint = e.Location;

                // Обновление PictureBox для отображения изменений
                pictureBox1.Invalidate();
            }
        }

        // Метод для заливки области цветом
        private void FillRegion(Point startPoint, Color fillColor)
        {
            // Создание временного изображения для работы с пикселями
            Bitmap tempBitmap = new Bitmap(bitmap);
            Queue<Point> points = new Queue<Point>();
            points.Enqueue(startPoint);

            // Получение цвета начальной точки
            Color targetColor = tempBitmap.GetPixel(startPoint.X, startPoint.Y);

            // Поиск всех точек, которые должны быть заполнены новым цветом
            while (points.Count > 0)
            {
                Point currentPoint = points.Dequeue();

                // Проверка находится ли точка внутри границ PictureBox
                if (currentPoint.X < 0 || currentPoint.Y < 0 || currentPoint.X >= pictureBox1.Width || currentPoint.Y >= pictureBox1.Height)
                    continue;

                // Если цвет текущей точки совпадает с начальным цветом
                if (tempBitmap.GetPixel(currentPoint.X, currentPoint.Y) == targetColor)
                {
                    // Заполняем точку новым цветом
                    tempBitmap.SetPixel(currentPoint.X, currentPoint.Y, fillColor);

                    // Добавляем соседние точки для проверки их цвета
                    points.Enqueue(new Point(currentPoint.X + 1, currentPoint.Y));
                    points.Enqueue(new Point(currentPoint.X - 1, currentPoint.Y));
                    points.Enqueue(new Point(currentPoint.X, currentPoint.Y + 1));
                    points.Enqueue(new Point(currentPoint.X, currentPoint.Y - 1));
                }
            }

            // Обновление изображения и PictureBox
            bitmap = tempBitmap;
            pictureBox1.Image = bitmap;
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            currentAction = MouseAction.Draw; 
        }

  
        private void button2_Click(object sender, EventArgs e)
        {
            currentAction = MouseAction.Fill; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //диалог выбора цвета и установка текущего цвета
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = colorDialog.Color;
            }
        }
    }
}
