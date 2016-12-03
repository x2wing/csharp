using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using System.Windows.Forms;

//*************************************************************************************
//метод Write показывает полиморфизм переопределение виртуальные методы и т.п. по лабе 7
//Filling() переопределен в классе Processing
//*************************************************************************************



namespace lab7form
{
    // класс для хранения одной строки из файла
    class Row
    {

        public DateTime Time { get; set; } //время 
        public double X { get; set; } // координата x
        public double Y { get; set; } // координата y



    }
    // класс список всех строк  из файла
    abstract class Table
    {
        protected List<Row> records;
        // метод абстрактный для заполнения списка      
        public abstract void Filling();
        //вывод заполненого исходного списка(как в предыдущих лабах) не искользуется переопределяется далее в классах ToForm, ToFile, SourceToForm
        public virtual  void Write(string path)
        {
            using (var fd = new StreamWriter(path))
            {
                var writer = new CsvWriter(fd);
                writer.WriteRecords(records);
            }
        }
        

    }





    //класс обработкий
    class Processing : Table
    {

        protected Result Results = new Result(); // тут хрантися список строк результата
        double Tochnost = 0.01; // точность для задания "движение с фиксированной скоростью (с точностью 5 %) более заданного времени;"


        // метод заполнения хранилища - class Table
        public override void Filling()
        {

            using (var fd = new StreamReader(FileSelect()))
            {

                var reader = new CsvReader(fd);
                reader.Configuration.Delimiter = ";";
                records = reader.GetRecords<Row>().ToList();

            }


        }
        // метод вывода OpenFileDialog возвращает путь к исходному файлу
        public string FileSelect()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                return dlg.FileName;
            }
            return null;
        }

        // конструктор. при вызове заполняет хранилище
        public Processing()
        {
            Filling();
        }
        //Вычисление растояния
        
        protected double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }
        //вычисление скорости
        protected double Speed(double Distance, DateTime t1, DateTime t2)
        {
            return Distance / (t2 - t1).TotalSeconds; // TotalSeconds - свойство показывающее количество секунд типа double
        }
        
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

        // превышение скорости
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
        // движение с фиксированной скоростью
        public void FixedSpeed()
        {
            bool FirstIs = false;
            //bool Uchteno = false;
            double S1 = 0;
            double S2 = 0;
            DateTime StartTime = new DateTime();
            DateTime StopTime = new DateTime();

            string EventName = "движение с фиксированной скоростью (с точностью 5 %) более заданного времени";
            double MaxTimeFixedSpeed = 5; //заданное время. Больше этого событие засчитывается
            for (int i = 2; i < records.Count; i++)
            {
                S1 = Speed(Distance(records[i - 2].X, records[i - 2].Y, records[i - 1].X, records[i - 1].Y), records[i - 2].Time, records[i - 1].Time);
                S2 = Speed(Distance(records[i - 1].X, records[i - 1].Y, records[i].X, records[i].Y), records[i - 1].Time, records[i].Time);
                if (Math.Abs(S1 - S2) < Eps(S1) && !FirstIs)
                {
                    StartTime = records[i - 2].Time;
                    FirstIs = true;
                }
                else if (Math.Abs(S1 - S2) >= Eps(S1) && FirstIs)
                {
                    FirstIs = false;
                    StopTime = records[i - 1].Time;
                    if ((StopTime - StartTime).TotalSeconds > MaxTimeFixedSpeed)
                        Results.AddItem(EventName, StartTime, StopTime);

                }




            }
        }
    }

    // класс для хранения строки результата, как class Row только для выходного результата
    class Out
        {
            private string EventName; // название события движение с фиксированной скоростью, полные остановки, превышение заданной скорости
            private DateTime StartTime; // время начала события
            private DateTime StopTime; // время окончания события

        // акцессоры в соответсвии с парадигмами C# для исключения нарушения инкапсуляции)
            public string _EventName{ get { return EventName; }}
            public DateTime _StartTime { get { return StartTime; } } 
            public DateTime _StopTime { get { return StopTime; } }

            public Out() { }//заглушка
        // конструктор для заполнения
            public Out(string EventName, DateTime StartTime, DateTime StopTime)
            {
                this.EventName = EventName;
                this.StartTime = StartTime;
                this.StopTime = StopTime;
            }

        }
    // хранилище результата
        class Result : Out
        {
            private List<Out> Results = new List<Out>();

            public List<Out> GetResult()
            {
                return Results;
            }
        // метод добавления строки в хранилище
            public void AddItem(string EventName, DateTime StartTime, DateTime StopTime)
            {
                Results.Add(new Out(EventName, StartTime, StopTime));
            }

        }



    // класс вывода результата на форму
    class ToForm : Processing
    {
        //**костыль для получения доступа к элемнтам формы (конкретоно нужно текстовое поле rtbOut)**
        Form1 thisform;
        
        public ToForm(Form1 thisform)
        {
            this.thisform = thisform;
        }
        //********************************************************************************************



        // метод вывода результата на форму
        public override void Write(string path)
        {
            // перебор всего хранилищи и вывод
            foreach (Out i in Results.GetResult())
                thisform.rtbOut.Text += i._EventName+";"+ i._StartTime+";" + i._StopTime + "\n";
            // перемотка к концу текстового поля
            thisform.rtbOut.SelectionStart = thisform.rtbOut.Text.Length;
            thisform.rtbOut.ScrollToCaret();





        }
    }
    // вывод исходного файла на форму(чтобы показать полиморфизм))
    class SourceToForm : Processing
    {
        // получим доступ к элемнтам формы
        Form1 thisform;
        public SourceToForm(Form1 thisform)
        {
            this.thisform = thisform;
        }
        // метод вывода исходого содержимого на форму
        public override void Write(string path)
        {
            foreach (var i in records)
                thisform.rtbOut.Text += i.Time + ";" + i.X + ";" + i.Y + "\n";
            // перемотка к концу текстового поля
            thisform.rtbOut.SelectionStart = thisform.rtbOut.Text.Length;
            thisform.rtbOut.ScrollToCaret();
        }
    }


    // метод вывода результата в файл
    class ToFile : Processing
    {

        //public ToFile() { }
        // генератор csv как в предыдущих лабах
        public override void Write(string path)
        {

            using (var fd = new StreamWriter(path))
            {
                var writer = new CsvWriter(fd);
                writer.Configuration.Delimiter = ";";
                writer.WriteRecords(Results.GetResult());
            }
        }
    }

}
