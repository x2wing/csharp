using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace lab5v1
{
    // класс в который помещается одна строка из файла. На этот раз без костылей
    class Row
    {
        
        public DateTime Time { get; set; } //время 
        public double X { get; set; } // координата x
        public double Y { get; set; } // координата y



    }
    // класс список всех строк  из файла
    abstract class Table : Row
    {
        protected List<Row> records;
        // метод заполнения списка
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected abstract double Distance(double x1, double y1, double x2, double y2);

        protected abstract double Speed(double Distance, DateTime t1, DateTime t2);
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void Filling(string path)
        {
            while (true)
            {
                //try
                //{
                using (var fd = new StreamReader(path))
                {

                    var reader = new CsvReader(fd);
                    reader.Configuration.Delimiter = ";";
                    records = reader.GetRecords<Row>().ToList();
                    break; // выход из цикла если все хорошо
                }
                //}
                //catch
                //{
                //    Console.WriteLine("чето не открывается. Проверьте файл и нажмите любую клавишу");
                //    Console.ReadKey();
                //}
            }

        }
    }
    // класс обработки данных
    class Processing : Table
    {
        
        Result Results = new Result(); // тут хрантися список строк результата
        double Tochnost = 0.01; // точность для задания "движение с фиксированной скоростью (с точностью 5 %) более заданного времени;"
        //Конструктор предыдущий класс
        public Processing(string path)
        {
            Filling(path);
        }
        //Вычисление растояния
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected override double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }
        //вычисление скорости
        protected override double Speed(double Distance, DateTime t1, DateTime t2)
        {
            return Distance / (t2 - t1).TotalSeconds; // TotalSeconds - свойство показывающее количество секунд типа double
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //вычисление погрешности
        private double Eps(double Value)
        {
            return Value * Tochnost;
        }
        // метод для задания б) полные остановки;
        public void Stoped()
        {
            bool FirstIs = false; // флаг было ли поймано время начала события
            double S = 0; //переменная скорости
            DateTime StartTime = new DateTime(); //время начала события
            string EventName = "полная остановка"; //коммент
            // прогоняем весь список
            for (int i = 1; i < records.Count; i++)
            {
                //вычисляем скрость
                S = Speed(Distance(records[i - 1].X, records[i - 1].Y, records[i].X, records[i].Y), records[i - 1].Time, records[i].Time);
                //условие конца события
                if (S != 0 && FirstIs)
                {
                    FirstIs = false;
                    DateTime StopTime = records[i - 1].Time;
                    Results.AddItem(EventName, StartTime, StopTime);
                    //Console.WriteLine($"конец события {records[i - 1].Time}");
                }
                // условие начала события
                else if (S == 0 && !FirstIs)
                {
                    StartTime = records[i - 1].Time;
                    FirstIs = true;
                }


            }

        }


        public void OverSpeed()
        {
            double MaxSpeed = 8;
            bool FirstIs = false;
            double S = 0;
            DateTime StartTime = new DateTime();
            string EventName = "превышение скорости";

            for (int i = 1; i < records.Count; i++)
            {
                S = Speed(Distance(records[i - 1].X, records[i - 1].Y, records[i].X, records[i].Y), records[i - 1].Time, records[i].Time);
                if (S < MaxSpeed && FirstIs)
                {
                    FirstIs = false;
                    DateTime StopTime = records[i - 1].Time;
                    Results.AddItem(EventName, StartTime, StopTime);
                }
                else if (S > MaxSpeed && !FirstIs)
                {
                    StartTime = records[i - 1].Time;
                    FirstIs = true;
                }
            }
            if (FirstIs == true)
            {
                //Console.WriteLine($"конец {EventName}        {records[records.Count - 1].Time}");
                DateTime StopTime = records[records.Count - 1].Time;
                Results.AddItem(EventName, StartTime, StopTime);
            }
        }

        public void FixedSpeed()
        {
            bool FirstIs = false;
            bool Uchteno = false;
            double S1 = 0;
            double S2 = 0;
            DateTime StartTime = new DateTime();
            DateTime StopTime = new DateTime();

            string EventName = "движение с фиксированной скоростью (с точностью 5 %) более заданного времени";
            double MaxTimeFixedSpeed = 5; //заданное время. Больше этого событие засчитывается
            for (int i = 2; i < records.Count; i++)
            {
                S1 = Speed(Distance(records[i-2].X, records[i-2].Y, records[i-1].X, records[i-1].Y), records[i-2].Time, records[i-1].Time);
                S2 = Speed(Distance(records[i-1].X, records[i-1].Y, records[i].X, records[i].Y), records[i-1].Time, records[i].Time);
                if (Math.Abs(S1-S2) <Eps(S1)&& !FirstIs)
                {
                    StartTime = records[i - 2].Time;
                    FirstIs = true;
                }
                else if(Math.Abs(S1 - S2) >= Eps(S1) && FirstIs )
                {
                    FirstIs = false;
                    StopTime = records[i-1].Time;
                    if ((StopTime - StartTime).TotalSeconds > MaxTimeFixedSpeed)
                        Results.AddItem(EventName, StartTime, StopTime);
                    
                }

                
                

            }
        }

        class Out
        {
            private string EventName; // название события движение с фиксированной скоростью, полные остановки, превышение заданной скорости
            private DateTime StartTime; // время начала события
            private DateTime StopTime; // время окончания события

            public Out() { }//заглушка
            public Out(string EventName, DateTime StartTime, DateTime StopTime)
            {
                this.EventName = EventName;
                this.StartTime = StartTime;
                this.StopTime = StopTime;
            }

        }
        class Result : Out
        {
            private List<Out> Results = new List<Out>();

            public void AddItem(string EventName, DateTime StartTime, DateTime StopTime)
            {
                Results.Add(new Out(EventName, StartTime, StopTime));
            }

        }

        class Program
        {
            static void Main(string[] args)
            {

                Processing A = new Processing(@"GPSTRACE.csv");

                A.Stoped();
                A.OverSpeed();
                A.FixedSpeed();
                Console.ReadKey();
            }
        }
    }
}