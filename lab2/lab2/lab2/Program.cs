using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{

    class Coords
    {
        private float X1, Y1, X2, Y2;
        private float[] coordsmassive;
        public List<float[]> vectors;

        //свойство для задания массива координат без консольного ввода
        public float[] SetCoords
        {
            set
            {
                coordsmassive = value;
            }
        }

        //задания массива координат. Перегруженный конструктор
        public Coords(params float[] ProgramCoords)
        {
            coordsmassive = ProgramCoords;
        }
        //дефолтный конструктор почти как из методички
        public  Coords()
        {
            // Запрашиваем координаты вершин треугольника у пользователя.
            Console.WriteLine("Введите координаты точек на плоскости " +
            "через пробел в формате x1 y1 x2 y2 x3 y3.....:");
            string coordsLine = Console.ReadLine();
            // Перепаковываем координаты в массив вещественных чисел.
            string[] coords = coordsLine.Split(' ');
            this.coordsmassive = new float[coords.Length];

            for (int i = 0; i < coords.Length; i++)
            {
                this.coordsmassive[i] = Convert.ToInt32(coords[i]);
            }

            
        }

        // генератор векторов
        public void MakeVectors()
        {
             this.vectors = new List<float[]>();



            for (int i = 0; i < coordsmassive.Length / 2 - 1; i++)
                for (int j = i + 1; j < coordsmassive.Length / 2; j++)
                {
                    float[] a = new float[4];
                    a[0] = coordsmassive[2 * i];
                    a[1] = coordsmassive[2 * i + 1];
                    a[2] = coordsmassive[2 * j];
                    a[3] = coordsmassive[2 * j + 1];
                    this.vectors.Add(a);
                }

            
        }

        //Дальше мне лень копипастить функции и превращать их в методы, думаю итак все понятно
        // тут еще наследование можно нагородить)

    
    }


    class Program
    {
        static void Main(string[] args)
        {
            //отладочный код)
            float[] a = { 1, 2, 3, 4, 5, 6, 78, 9 };
            float[] b = { 8,6.9F,12,5.6F,8,8,5,6,8,7,6,8,8,6,8,7 };
            Coords test = new Coords(a);
            test.SetCoords=b;

            test.MakeVectors();

            foreach(float[] vect in test.vectors)
                foreach(float coord in vect)
                    Console.WriteLine("координата {0}",coord);

            Console.ReadKey();
        }
    }
}
