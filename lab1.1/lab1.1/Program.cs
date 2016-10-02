using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            float maxSquare = 0;
            float[] maxDlina = new float[3];
            float minSquare = 0;
            float[] minDlina = new float[3];
            ///////float[,] triangle = AskTriangleUI();
            float[,] triangle = { { 1, 8 }, { 2, 6 }, { 3, 20 }, { 4, 31 }, { 5, 17 }};
            float square = CalcTriangleSquare(triangle);
            Sochetanie(triangle);
            MaxSq(TempTriangle, square, ref maxSquare, ref  maxDlina);
            //WriteSquareUI(triangle);
            //Console.WriteLine("площадь={0}", square);
            //WriteMassive(triangle);
            Console.WriteLine("max square = {0}\t max dlina={1}", maxSquare, maxDlina);
            // Ожидаем нажатия любой клавиши, чтобы консоль сразу не закрылась.
            Console.ReadKey();
        }

        static void Sochetanie(float[,] traingle )
        {
            float[,] TempTriangle = new float[3, 2];
            

            for (int i = 0; i < traingle.Length / 2 - 2; i++)
                for (int j = i + 1; j < traingle.Length / 2 - 1; j++)
                    for (int k = j + 1; k < traingle.Length / 2; k++)
                    {
                        TempTriangle[0, 0] = traingle[i, 0];
                        TempTriangle[0, 1] = traingle[i, 1];
                        TempTriangle[1, 0] = traingle[j, 0];
                        TempTriangle[1, 1] = traingle[j, 1];
                        TempTriangle[2, 0] = traingle[k, 0];
                        TempTriangle[2, 1] = traingle[k, 1];
                        float square = CalcTriangleSquare(TempTriangle);
                        Console.WriteLine("площадь = {0}",square);
                        MaxSq(TempTriangle, square, ref maxSquare, ref  maxDlina);
                        

                            
                    }
            
        }

        static void MaxSq( float[,] TempTriangle, float square, ref float maxSquare, ref float[] maxDlina)
        {
            if (maxSquare < square)
            {
                maxSquare = square;
                maxDlina = CalcTriangleSides(TempTriangle);

            }
        }
     
        static float[,] AskTriangleUI()
        {
            // Запрашиваем координаты вершин треугольника у пользователя.
            Console.WriteLine("Введите координаты вершин треугольника " +
            "через пробел в формате xa ya xb yb xc yc:");
            string coordsLine = Console.ReadLine();
            // Перепаковываем координаты в массив вещественных чисел.
            string[] coords = coordsLine.Split(' ');
            float[,] triangle = new float[coords.Length/2,2];

            for (int i = 0; i < coords.Length; i++)
            {
                triangle[i, 0] = Convert.ToInt32(coords[i]);
                triangle[i, 1] = Convert.ToInt32(coords[i+1]);
            }

            return triangle;


        }

        static float CalcLength(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }

        static float[] CalcTriangleSides(float[,] triangle)
        {
            float[] sides = new float[3];
            sides[0] = CalcLength(triangle[0,0], triangle[0,1], triangle[1,0], triangle[1,1]);
            sides[1] = CalcLength(triangle[1,0], triangle[1,1], triangle[2,0], triangle[2,1]);
            sides[2] = CalcLength(triangle[2,0], triangle[2,1], triangle[0,0], triangle[0,1]);
            //Console.WriteLine("длины={0}", sides);
            return sides;
        }
        /// <summary>
        /// Расчет площади треугольника.
        /// </summary>
        static float CalcTriangleSquare(float[,] triangle)
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
            float result = (float)Math.Sqrt(p * (p-a) * (p-b) * (p-c));
            return result;
        }
        /// <summary>
        /// Вывод результатов расчета пользователю.
        /// </summary>
        /// <param name="square"></param>



        static void WriteMassive(float[,] massive)
        {
            for (int i=0; i<massive.Length/2;i++ )
                Console.WriteLine("Элемент массива = {0},{1}", massive[i,0], massive[i,1]);
            //foreach (float i in massive)
            //{
                
            //    Console.WriteLine("Элемент массива = {0}", i);
            //}
        }

        static void WriteSquareUI(float square)
        {
            Console.WriteLine("Площадь треугольника S = {0}", square);
        }
    }
}