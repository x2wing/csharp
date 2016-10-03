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
            
            float[] points = AskTriangleUI(); 
            List<float[]>  v = MakeVectors(ref points);


            float[] MaxResult = MaxSquare(v);
            float[] MinResult = MinSquare(v);
            WriteResult(MaxResult);
            WriteResult(MinResult);

            //Tuple<float, float[,]> MinResult = MinSquare(vectors);


            //float square = CalcTriangleSquare(triangle);

            // Ожидаем нажатия любой клавиши, чтобы консоль сразу не закрылась.
            Console.ReadKey();
        }
        /// <summary>
        /// функция создания массива векторов из которых будут строится круги
        /// </summary>
        /// <returns></returns>
        /// 


        static List<float[]> MakeVectors(ref float[] points)
        {
            List<float[]> vectors = new List<float[]>();
            


            for (int i = 0; i < points.Length / 2 - 1; i++)
                for (int j = i + 1; j < points.Length / 2; j++)
                {
                    float[] a = new float[4];
                    a[0] = points[2*i];
                    a[1] = points[2*i+1];
                    a[2] = points[2*j];
                    a[3] = points[2*j+1];
                    vectors.Add(a);
                }



            //foreach (float[] i in vectors)
            //{
            //    foreach (float j in i)
            //        Console.WriteLine(j);

            //    Console.WriteLine("\n");
            //}

            return vectors;
        }

        static float[] MaxSquare(List <float[]>vectors)
        {
            float[] SquareCoords = new float[5];
            float max=0;

            foreach (float[] i in vectors)
                if (max < Square(i))
                {
                    max = Square(i);
                    Array.Copy(i, SquareCoords, i.Length);
                }
            SquareCoords[4] = max;
            return SquareCoords;
        }

        static float[] MinSquare(List<float[]> vectors)
        {
            float[] SquareCoords = new float[5];
            float min = 9999999999999f;

            foreach (float[] i in vectors)
                if (min > Square(i))
                {
                    min = Square(i);
                    Array.Copy(i, SquareCoords, i.Length);
                }
            SquareCoords[4] = min;
            return SquareCoords;
        }

        static float Dlina(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }
       

        static float Square(float[] vector)
        {
            float R = Dlina(vector[0], vector[1], vector[2], vector[3]);
            //Console.WriteLine("площадь={0}", (float)Math.PI * R * R);
            return (float)Math.PI * R * R;
            
        }


        static float[] AskTriangleUI()
        {
            // Запрашиваем координаты вершин треугольника у пользователя.
            Console.WriteLine("Введите координаты точек на плоскости " +
            "через пробел в формате x1 y1 x2 y2 x3 y3.....:");
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
        
       
        static void WriteResult(float[] Result)
        {
            Console.WriteLine("координаты и площадь:");
            Console.WriteLine("координаты центра круга x={0} y={1}", Result[0], Result[1]);
            Console.WriteLine("координаты точки окружности x={0} y={1}", Result[2], Result[3]);
            Console.WriteLine("площадь S={0}", Result[4]);
            
        }
    }
}
