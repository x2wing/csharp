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
        public int key { get; set; } // координата y
        public int id { get; set; } //время 
        public string surname { get; set; } // координата x
        public string name { get; set; } // координата x
        public string last_name { get; set; } // координата x


        
    }
    // класс список всех строк  из файла
    abstract class Table
    {
        

        public List<Row> records;
        // метод абстрактный для заполнения списка      
        public abstract void Filling(string path);
        //вывод заполненого исходного списка(как в предыдущих лабах) не искользуется переопределяется далее в классах ToForm, ToFile, SourceToForm
        
    }

     

    class StartInit: Table
    {
        public delegate void Semantec_analize(string cmd);

        Semantec_analize _Semantec_analize;
        // аргументы команды
        protected string union_arg1;
        protected string union_arg2;

        protected string where_col; //поле по которому фильтруется вывод
        protected string where_op; // операция [><=like]
        protected string where_arg; // аргумент операции

        public override void Filling(string path)
        {
            using (var fd = new StreamReader(path))
            {

                var reader = new CsvReader(fd);
                reader.Configuration.Delimiter = ";";
                records = reader.GetRecords<Row>().ToList();

            }
        }

        public Object GetCol(string where_col, Row item)
        {
            switch (where_col)
            {
                case "key":
                    return item.key;
                case "id":
                    return item.id;
                case "surname":
                    return item.surname;
                case "name":
                    return item.name;
                case "last_name":
                    return item.last_name;
                default:
                    return null;

            }


        }

        public bool Sintax_analize(string command)
        {
            const string command_mask1 = "union * *|where * ? *|";
            const string command_mask2 = "union * *|";
            const string command_mask3 = "where * ? *|";

            if (command.Contains("union ") && command.Contains("where ") && PROCESSING.Match(command, command_mask1))
            {
                _Semantec_analize =  new Semantec_analize(Semantic_analizer1);
                return true;
            }
            else if (command.Contains("union ") && PROCESSING.Match(command, command_mask2))
            {
                _Semantec_analize =  new Semantec_analize(Semantic_analizer2);
                return true;
            }
            else if (command.Contains("where ") && PROCESSING.Match(command, command_mask3))
            {
                _Semantec_analize =  new Semantec_analize(Semantic_analizer3);
                return true;
            }

            
            return false;
        }

        public void Semantec_analize_foo(string cmd)
        {
            _Semantec_analize(cmd);
        }

        
        public static  void Semantic_analizer1(string cmd)
        {

        }
        public static void Semantic_analizer2(string cmd)
        {

            //List<string> args = stri cmd.Split(' ');
            List<string> args = new List<string>(cmd.Split(' '));
            args.RemoveAll(s => s== "" );

        }
        public static void Semantic_analizer3(string cmd)
        {

        }


        //public string[] cleaning(string[] args)
        //{

        //    //return clean_args;
        //}
    }



}
