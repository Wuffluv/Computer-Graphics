using System;
using System.Drawing;
using System.Windows.Forms;

namespace CutLine
{
    public partial class Form1 : Form
    {
        private bool isDrawingRect = false; 
        private bool isDrawingLine = false; 
        private Point startPoint; 
        private Point endPoint; 
        private Rectangle rectangle; // Прямоугольник, используемый для отображения выделенной области
        int x1, y1, x2, y2; // Координаты начала и конца прямоугольника

        public Form1()
        {
            InitializeComponent();

            // Добавляем обработчики событий мыши
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.Paint += pictureBox1_Paint;
        }

        //  нажатие кнопки мыши 
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isDrawingRect && !isDrawingLine)
            {
                isDrawingRect = true; // Устанавливаем флаг рисования прямоугольника
                startPoint = e.Location; // Запоминаем начальную точку
                x1 = e.Location.X;
                y1 = e.Location.Y;
            }
        }

        // перемещение мыши 
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawingRect && !isDrawingLine)
            {
                Point currentPoint = e.Location; 
                int x = Math.Min(startPoint.X, currentPoint.X); 
                int y = Math.Min(startPoint.Y, currentPoint.Y); 
                int width = Math.Abs(startPoint.X - currentPoint.X); 
                int height = Math.Abs(startPoint.Y - currentPoint.Y); 
                rectangle = new Rectangle(x, y, width, height); // Создаем прямоугольник
                pictureBox1.Invalidate(); 
            }
        }

        //отпускание кнопки мыши 
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawingRect)
            {
                isDrawingRect = false; // Сбрасываем флаг рисования прямоугольника
                isDrawingLine = true; // Устанавливаем флаг рисования линии 
                x2 = e.Location.X;
                y2 = e.Location.Y;
                startPoint = Point.Empty; // Сбрасываем начальную точку
            }
        }

        // отрисовка содержимого 
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (rectangle != null)
            {
                e.Graphics.DrawRectangle(Pens.Green, rectangle); // Рисуем прямоугольник
            }
        }

        //клик мыши 
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isDrawingLine)
            {
                if (startPoint.IsEmpty)
                {
                    startPoint = e.Location; // Задаем начальную точку линии
                }
                else
                {
                    endPoint = e.Location; // Задаем конечную точку линии

                    using (Graphics g = pictureBox1.CreateGraphics())
                    {
                        // Определяем границы прямоугольника
                        int left = Math.Min(x1, x2);
                        int top = Math.Min(y1, y2);
                        int right = Math.Max(x1, x2);
                        int bottom = Math.Max(y1, y2);

                        // Находим точки пересечения линии с границами прямоугольника
                        Point start = FindEnds(startPoint, endPoint, left, top, right, bottom);
                        Point end = FindEnds(endPoint, startPoint, left, top, right, bottom);

                        // Рисуем только сегмент линии, находящийся внутри прямоугольника
                        if (start != null && end != null)
                        {
                            g.DrawLine(Pens.Black, start, end); // Рисуем линию
                        }
                    }
                    startPoint = Point.Empty; // Сбрасываем начальную точку
                    endPoint = Point.Empty; // Сбрасываем конечную точку
                }
            }
        }

        // Метод для нахождения точки пересечения линии с границами прямоугольника
        private Point FindEnds(Point lineStart, Point lineEnd, int rectLeft, int rectTop, int rectRight, int rectBottom)
        {
            // Используем алгоритм Брезенхэма для отрисовки линии
            int dx = Math.Abs(lineEnd.X - lineStart.X);
            int dy = Math.Abs(lineEnd.Y - lineStart.Y);
            int sx = (lineStart.X < lineEnd.X) ? 1 : -1;
            int sy = (lineStart.Y < lineEnd.Y) ? 1 : -1;
            int err = dx - dy;

            int x = lineStart.X;
            int y = lineStart.Y;

            while (true)
            {
                // Проверяем, находится ли точка внутри прямоугольника
                if (x >= rectLeft && x <= rectRight && y >= rectTop && y <= rectBottom)
                {
                    return new Point(x, y); // Возвращаем точку пересечения
                }

                // Прекращаем цикл, если достигли конечной точки линии
                if (x == lineEnd.X && y == lineEnd.Y)
                {
                    break;
                }

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y += sy;
                }
            }
            return Point.Empty; // Возвращаем пустую точку, если линия не пересекает прямоугольник
        }
    }
}


