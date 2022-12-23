using System;
using System.Drawing;

namespace Freelancer_Companion_by_Dormammu.Services
{
    public class DrawService
    {
        /// <summary>
        /// Пикселей в одном делении
        /// </summary>
        private int BlockLength { get; set; }
        /// <summary>
        /// Длинна стрелки
        /// </summary>
        private int ArrowLength { get; set; }

        public DrawService(int BlockLength,int ArrowLength)
        {
            this.BlockLength = BlockLength;
            this.ArrowLength = ArrowLength;
        }

        /// <summary>
        /// Рисование оси X
        /// </summary>
        /// <param name="start">Начало</param>
        /// <param name="end">Конец</param>
        /// <param name="map">Карта</param>
        public void DrawXAxis(Point start, Point end, Graphics map)
        {
            //Деления в положительном направлении оси
            for (int i = BlockLength; i < end.X - ArrowLength; i += BlockLength)
            {
                map.DrawLine(Pens.Black, i, -1, i, 1);
            }
            //Деления в отрицательном направлении оси
            for (int i = -BlockLength; i > start.X; i -= BlockLength)
            {
                map.DrawLine(Pens.Black, i, -1, i, 1);
            }
            //Ось
            map.DrawLine(Pens.Black, start, end);
            //Стрелка
            map.DrawLines(Pens.Black, GetArrow(start.X, start.Y, end.X, end.Y, ArrowLength));
        }

        /// <summary>
        /// Рисование оси Y
        /// </summary>
        /// <param name="start">Начало</param>
        /// <param name="end">Конец</param>
        /// <param name="map">Карта</param>
        public void DrawYAxis(Point start, Point end, Graphics map)
        {
            //Деления в отрицательном направлении оси
            for (int i = BlockLength; i < start.Y; i += BlockLength)
            {
                map.DrawLine(Pens.Black, -1, i, 1, i);
            }
            //Деления в положительном направлении оси
            for (int i = -BlockLength; i > end.Y + ArrowLength; i -= BlockLength)
            {
                map.DrawLine(Pens.Black, -1, i, 1, i);
            }
            //Ось
            map.DrawLine(Pens.Black, start, end);
            //Стрелка
            map.DrawLines(Pens.Black, GetArrow(start.X, start.Y, end.X, end.Y, ArrowLength));
        }

        /// <summary>
        /// Рисует местоположение базы
        /// </summary>
        /// <param name="X">X</param>
        /// <param name="Y">Y</param>
        /// <param name="map">Карта</param>
        public void DrawPoint(int X, int Y, Graphics map)
        {
            map.FillRectangle(Brushes.Red, X, Y, 3, 3);
        }

        /// <summary>
        /// Рисование текста
        /// </summary>
        /// <param name="point">Местоположение</param>
        /// <param name="text">Текст</param>
        /// <param name="map">Карта</param>
        /// <param name="isYAxis">Выбор оси</param>
        private void DrawText(Point point, string text, Graphics map, bool isYAxis = false)
        {
            var fontFamily = new FontFamily("Arial");
            var font = new Font(fontFamily, 16, FontStyle.Bold, GraphicsUnit.Pixel);
            var size = map.MeasureString(text, font);
            var location = isYAxis
                ? new PointF(point.X + 1, point.Y - size.Height / 2)
                : new PointF(point.X - size.Width / 2, point.Y + 1);
            var rect = new RectangleF(location, size);
            map.DrawString(text, font, Brushes.Black, rect);
        }

        /// <summary>
        /// Вычисление стрелки оси
        /// </summary>
        /// <param name="x1">X1</param>
        /// <param name="y1">Y1</param>
        /// <param name="x2">X2</param>
        /// <param name="y2">Y2</param>
        /// <param name="height">Ширина стрелки</param>
        /// <param name="width">Длинна стрелки</param>
        /// <returns></returns>
        private PointF[] GetArrow(float x1, float y1, float x2, float y2, float height = 10, float width = 4)
        {
            PointF[] result = new PointF[3];
            //направляющий вектор отрезка
            var n = new PointF(x2 - x1, y2 - y1);
            //Длина отрезка
            var l = (float)Math.Sqrt(n.X * n.X + n.Y * n.Y);
            //Единичный вектор
            var v1 = new PointF(n.X / l, n.Y / l);
            //Длина стрелки
            n.X = x2 - v1.X * height;
            n.Y = y2 - v1.Y * height;
            //формирование элементов
            result[0] = new PointF(n.X + v1.Y * width, n.Y - v1.X * width);
            result[1] = new PointF(x2, y2);
            result[2] = new PointF(n.X - v1.Y * width, n.Y + v1.X * width);
            return result;
        }
    }
}
