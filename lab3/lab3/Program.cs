using System    ;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Vector
    {
        private  double[] StartCoords;
        private  double[] EndCoords;
        private static int Razmernost; // static потому что студия ругается в перегрузке оператора
        private double LenVector;

        //конструктор копирования
        public Vector(Vector previousVector)
        {
            this.StartCoords = previousVector.StartCoords;
            this.EndCoords = previousVector.EndCoords;
            Razmernost = previousVector.VRazmernost;
            LenVector = previousVector.LenVector;

        }
        
        
        //конструктор создает нулевой вектор размерности Razmernost
        public Vector(int Razmernost)
        {
            StartCoords = new double[Razmernost];
            EndCoords = new double[Razmernost];
            LenVector = 0;
        }

        //конструктор создает вектор из массива точек - сначала координаты начала, потом координаты конца
        public Vector(params double[] Coords)
        {
            Razmernost=Coords.Length / 2;
            StartCoords = new double[Razmernost];
            EndCoords = new double[Razmernost];
            Array.Copy(Coords,0, StartCoords, 0,Razmernost);
            Array.Copy(Coords, Razmernost, EndCoords, 0, Razmernost);
            LenVector = VectorLength();
        }

        //конструктор создает вектор из двух наборов координат
        public Vector(double[] StartCoords, double[] EndCoords)
        {
            this.StartCoords = StartCoords;
            this.EndCoords = EndCoords;
            Razmernost = StartCoords.Length;
            LenVector = VectorLength();

        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            Vector result = new Vector(Razmernost);
            for (int i = 0; i < Razmernost; i++)
            {
                result.StartCoords[i] = v1.StartCoords[i] + v2.StartCoords[i];
                result.EndCoords[i] = v1.EndCoords[i] + v2.EndCoords[i];
            }
            return result;
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            Vector result = new Vector(Razmernost);
            for (int i = 0; i < Razmernost; i++)
            {
                result.StartCoords[i] = v1.StartCoords[i] - v2.StartCoords[i];
                result.EndCoords[i] = v1.EndCoords[i] - v2.EndCoords[i];
            }
            return result;
        }

        // свойство размерность пространства
        public int VRazmernost
        {
            get
            {
                return Razmernost;
            }
            set
            {
                Razmernost = value;
            }
        }

        public double[] GetStartCoords
        {
            get
            {
                return StartCoords;
            }

        }

        public double[] GetEndCoords
        {
            get
            {
                return EndCoords;
            }
            
        }

        
        // метод возвращает длину n-мерного вектра L=sqrt((x1-x2)^2+(y1-y2)^2)
        public double VectorLength()
        {
            double Sum=0;
            for (int i = 0; i < Razmernost; i++)
                Sum += (StartCoords[i] - EndCoords[i]) * (StartCoords[i] - EndCoords[i]);
            
            return Math.Sqrt(Sum);
 
        }

        // статический метод  возвращает длину n-мерного вектра v
        public static double VectorLength(Vector v)
        {
            
            double Sum = 0;
            for (int i = 0; i < v.VRazmernost; i++)
                Sum += (v.StartCoords[i] - v.EndCoords[i]) * (v.StartCoords[i] - v.EndCoords[i]);

            return Math.Sqrt(Sum);

        }
        // растояние между двумя векторами (проверить правильность математики)
        public static double Distance(Vector v1, Vector v2)
        {
            Vector result = v1 - v2;
            return VectorLength(result);
        }
        // метод нормализации
        public Vector Normalize()
        {
            Vector result = new Vector(Razmernost);
            for (int i = 0; i < Razmernost; i++)
            {
                result.StartCoords[i] = StartCoords[i] / LenVector;
                result.EndCoords[i] = EndCoords[i] / LenVector;
            }
            return result;
 
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            double[] x = { 56,76,89 };
            double[] y = { 3,30,56 };
            

            Vector a = new Vector(1,2,3,4,5,6); // первый конструктор
            Vector b = new Vector(a);// конструктор копирования
            Vector c = new Vector(x,y); // второй конструктор
            Vector d = new Vector(5); // создание нулевого вектора размерности 5

            
            Vector Sum = a+c; // сложение двух векторов
            Vector Sub = a-c; // разность двух вектров
            Console.WriteLine("длина вектора", Vector.VectorLength(a));
            Console.WriteLine("размерность вектора вектора={0}", Sub.VRazmernost);
            
            Vector NormalizeVector = a.Normalize(); // нормализация
            Console.WriteLine("Координаты нормализованного вектора={0}", string.Join(";",NormalizeVector.GetStartCoords));
            Console.WriteLine("длина нормализованного вектора={0}", NormalizeVector.VectorLength());
            

            Console.ReadKey();
         
        }
    }
}
