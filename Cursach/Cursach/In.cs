using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;

namespace Cursach
{
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
        public virtual void Write(string path)
        {
            using (var fd = new StreamWriter(path))
            {
                var writer = new CsvWriter(fd);
                writer.WriteRecords(records);
            }
        }


    }
}
