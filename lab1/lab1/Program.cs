using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        /// <summary>
        /// Выполнение программы начинается с этой функции.
        /// </summary>
        static void Main(string[] args)
        {
            float[] triangle = AskTriangleUI();
            float square = CalcTriangleSquare(triangle);
            WriteSquareUI(square);
            // Ожидаем нажатия любой клавиши, чтобы консоль сразу не закрылась.
            Console.ReadKey();
        }
        /// <summary>
        /// Запрос у пользователя координат вершин треугольника.
        /// </summary>
        /// <returns></returns>
        static float[] AskTriangleUI()
        {
            // Запрашиваем координаты вершин треугольника у пользователя.
            Console.WriteLine("Введите координаты вершин треугольника " +
            "через пробел в формате xa ya xb yb xc yc:");
            string coordsLine = Console.ReadLine();
            // Перепаковываем координаты в массив вещественных чисел.
            string[] coords = coordsLine.Split(' ');
            float[] triangle = new float[coords.Length];

            for (int i = 0; i < coords.Length; i++)
            {
                triangle[i] = Convert.ToInt32(coords[i]);
            }

            return triangle;
        }
        /// <summary>
        /// Расчет длины отрезка.
        /// </summary>
        static float CalcLength(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }
        /// <summary>
        /// Расчет длин сторон треугольника.
        /// </summary>
        static float[] CalcTriangleSides(float[] triangle)
        {
            float[] sides = new float[3];
            sides[0] = CalcLength(triangle[0], triangle[1], triangle[2], triangle[3]);
            sides[1] = CalcLength(triangle[2], triangle[3], triangle[4], triangle[5]);
            sides[2] = CalcLength(triangle[4], triangle[5], triangle[0], triangle[1]);
            return sides;
        }
        /// <summary>
        /// Расчет площади треугольника.
        /// </summary>
        static float CalcTriangleSquare(float[] triangle)
        {
            float[] sides = CalcTriangleSides(triangle);
            float p = (sides[0] + sides[1] + sides[2]) / 2; // полупериметр
            float s = CalcTriangleSquareGeron(sides[0], sides[1], sides[2], p);
            return s;
        }

        /// <summary>
        /// Расчет площади по формуле Герона.
        /// </summary>
        static float CalcTriangleSquareGeron(float a, float b, float c, float p)
        {
            return (float)Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }
        /// <summary>
        /// Вывод результатов расчета пользователю.
        /// </summary>
        /// <param name="square"></param>
        static void WriteSquareUI(float square)
        {
            Console.WriteLine("Площадь треугольника S = {0}", square);
        }
    }
}
