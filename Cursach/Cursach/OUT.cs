using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using System.Windows.Forms;

namespace Cursach
{

    class FormOut : _Out
    {
        // конструктор из родительского класса
        public FormOut(Form1 thisform, Table ext_records) : base(thisform, ext_records) { }

        //вывод результата обработки на форму
        public override void Write()
        {
            thisform.lsbOut.Items.Clear();
            foreach (OutRow record in out_result)
                thisform.lsbOut.Items.Add(String.Format("{0};{1};{2};{3};{4}", record.key, record.id, record.surname, record.name, record.last_name));
        }
    }
    // прототипы переменных и функции Write
    abstract class Out
    {
        protected List<OutRow> out_result = new List<OutRow>();
        protected Form1 thisform;

        public abstract void Write();
    }


    class _Out:Out
    {

        //конструктор принимает экземпляр формы и результирующу таблицу
        public _Out(Form1 thisform, Table ext_records)
        {
            this.thisform = thisform;
            
            foreach (var i in ext_records.GetRecords())
                out_result.Add(new OutRow(i));
        }


        // запись в файл
        public override void Write()
        {
            using (var fd = new StreamWriter(Service.FileSelect()))
            {
                var writer = new CsvWriter(fd);
                writer.Configuration.Delimiter = ";";
                writer.WriteRecords(out_result);
            }
        }



    }

    // выходная таблица
    class OutRow
    {
        public string key { get; set; } // ключевое поле
        public string id { get; set; } //номер зачетки 
        public string surname { get; set; } // фамилия
        public string name { get; set; } // имя
        public string last_name { get; set; } // отчество

        

        public OutRow(Row r)

        {


            this.key = r.key;
            this.id = r.id;
            this.surname = r.surname;
            this.name = r.name;
            this.last_name = r.last_name;


        }
    }
}
