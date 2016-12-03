using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*  CsvHelper устанавливают через Package Manager Console: 
 *  Tools > NuGet Package Manager > Package Manager Console (сначала надо открыть проект)
 справка тут: https://joshclose.github.io/CsvHelper/
 пример тут: http://carlosferreira.com/writing-csv-files-using-csvhelper-package-c-ienumerable/
 */
using CsvHelper;
using System.IO;
using System.Globalization;


namespace ConsoleApplication3
{
    class InA
    {
        //Значения из файла А
        public TimeSpan TimeD { get ; set; }
        public string IDD { get; set; }
        public string ValueD { get; set; }
        
        
        public double GetValueD
        {
            get { return double.Parse(ValueD); }
        }

        //вытащить время из переменной  TimeD.Делов том, что тип DateTime содержит еще число месяц год.
        //public string GetTime()
        //{
        //    return TimeD.ToString(format);
        //}

    }

    class InB
    {
        //Значения из файла Б
        public string Naimenovanie { get; set; }
        public string IDD { get; set; }
        


        
        
        
    }

    // класс для представления исходных данных(TimeD;IDD;ValueD). не плохо сделать IEnumerator
    class IN
    {
        private List<InA> recordsA;
        private List<InB> recordsB;
        private Dictionary<string, string> NaimenovanieD = new Dictionary<string, string>();

       

        public List<InA>  GetRecordA
            {
            get { return this.recordsA; }
            }
        public List<InB> GetRecordB
        {
            get { return this.recordsB; }
        }

        public string GetNaimenovanie(string IDD)
        {
            return NaimenovanieD[IDD];
        }
        //конструктор
        public IN(string pathA, string pathB)
        {

            FillingA(pathA);
            FillingB(pathB);
            foreach (var i in recordsB)
                NaimenovanieD.Add(i.Naimenovanie,i.IDD);


        }

        private void FillingA(string path)
        {
            //using используется для косвенного закрытия потока после закрывающей }
            using (var fd = new StreamReader(path))
            {

                var reader = new CsvReader(fd);
                reader.Configuration.Delimiter = ";"; // говорим парсеру какой используется разделитель в csv файле
                // создаем список классов InA. InA - одна распарсеная строка. records - список InA( весь файл поделенный на записи с  3-мя полями)
                recordsA = reader.GetRecords<InA>().ToList();
            }
        }
        private void FillingB(string path)
        {
            using (var fd = new StreamReader(path))
            {

                var reader = new CsvReader(fd);
                reader.Configuration.Delimiter = ";"; // говорим парсеру какой используется разделитель в csv файле
                // создаем список классов InB. InB - одна распарсеная строка. records - список InA( весь файл поделенный на записи с  2-мя полями)
                recordsB = reader.GetRecords<InB>().ToList();
            }
        }

        // отладочный метод вывода на консоль csv файла
        public void ConsoleOut()
        {
            foreach (var i in recordsA)
            {
                Console.WriteLine("время:{0}, ИД датчика:{1}, показание {2}, double:{3}", i.TimeD, i.IDD, i.ValueD, i.GetValueD);
            }
        }

    }

    class OUT
    {
        //порядковый номер события, код датчика, наименование датчика, время начала события
        public int count { get; set; }
        public string IDD { get; set; }
        public string Naimenovanie { get; set; }
        public TimeSpan StartEventTime { get; set; }

        public OUT(int count, string IDD, string Naimenovanie, TimeSpan StartEventTime)
        {
            this.count = count;
            this.IDD = IDD;
            this.Naimenovanie = Naimenovanie;
            this.StartEventTime = StartEventTime;
        }
        
    }

    class Processing
    {
        private TimeSpan StartTime;
        private TimeSpan StopTime;
        private double excess;
        List<OUT> Output = new List<OUT>();
        //строка форматирования времени в файле А
        //private string format = "G";


        public Processing(string StartTime, string StopTime, double excess)
        {
            //тут магия)
            this.StartTime= DateTime.ParseExact(StartTime, "H:mm:ss", CultureInfo.InvariantCulture).TimeOfDay;
            this.StopTime= DateTime.ParseExact(StopTime, "H:mm:ss", CultureInfo.InvariantCulture).TimeOfDay;
            this.excess= excess;

        }

        public void GetReport(string AFilePath, string BFilePath)//,  string OutFilePath)
        {
            IN DATA = new IN(AFilePath, BFilePath);
            int counter = 1;

            foreach (var i in DATA.GetRecordA)
            {
                
               // Console.WriteLine("время:{0}, ИД датчика:{1}, показание {2}, double:{3}", i.TimeD, i.IDD, i.ValueD, i.GetValueD);
                if (i.GetValueD > excess)
                    Output.Add(new OUT(counter, i.IDD,DATA.GetNaimenovanie(i.IDD), i.TimeD));
                counter++; 
            }
        }

        public void WriteReport()
        {
            //foreach (var i in Output)
            //    Console.WriteLine("№{0} ИД:{1} Название:{2} Время{3}", i.count, i.IDD, i.Naimenovanie, i.StartEventTime);
            using (var fd = new StreamWriter(@"В.csv"))
            {
                var writer = new CsvWriter(fd);
                writer.WriteRecords(Output);
            }
        }





    }


    class Program
    {
        

        static void Main(string[] args)
        {
            //var fs = new File.ReadAllLines

            //Datchiki.FileToConsole(@"А.csv");
            //string  format = "HH:mm:ss";
            //DateTime result;
            //result = DateTime.ParseExact(@"00:00:20", format, CultureInfo.InvariantCulture);
            //Console.WriteLine(result.ToString(format));
            //


            //@ - делает строку raw (как есть без всяких экранирований). На всякий случай.
            //IN DATA = new IN(@"А.csv");
            //DATA.ConsoleOut();
            Processing A = new Processing(@"0:00:05", @"0:00:40", 2);
            A.GetReport(@"А.csv",@"Б.csv");
            A.WriteReport();

            Console.ReadKey();
        }
    }
}
