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
        // поля класса совпадают и именами полей таблицы и с шапкой
        public string key { get; set; } // ключевое поле
        public string id { get; set; } //номер зачетки 
        public string surname { get; set; } // фамилия
        public string name { get; set; } // имя
        public string last_name { get; set; } // отчество

        // доступ к полям по имени через индексатор
        public string this[string pole]
        {
            get
            {
                switch (pole)
                {
                    case "key":
                        return this.key;
                    case "id":
                        return this.id;
                    case "surname":
                        return this.surname;
                    case "name":
                        return this.name;
                    case "last_name":
                        return this.last_name;
                }

                throw new FileLoadException("ошибка в имени поля таблицы оператора where");

            }
        }
    }
    // класс список всех строк  из файла

    class Table
    {

        public List<Row> records;
        public string tablename;
        // метод абстрактный для заполнения списка      

        public Table() { }

        public Table(string path)
        {
            FillingTable(path);
            SetTableName(path);
        }

        private void SetTableName(string path)
        {
            string[] separators = { @"\", @"." };
            string[] temp =  path.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            this.tablename = temp[temp.Length-2] ;            
        }


        private void FillingTable(string path)
        {
            using (var fd = new StreamReader(path))
            {

                var reader = new CsvReader(fd);
                reader.Configuration.Delimiter = ";";
                records = reader.GetRecords<Row>().ToList();
            }


        }

    }




    class StartInit 
    {
        public delegate void Semantec_analize(string cmd);

        Semantec_analize _Semantec_analize;
        protected List<Table> Tables = new List<Table>();
        // аргументы команды
        protected static string union_arg1;
        protected static string union_arg2;
        protected static string where_col; //поле по которому фильтруется вывод
        protected static string where_op; // операция [><=like]
        protected static string where_arg; // аргумент операции


        public StartInit(List<string> filepaths_from_lst) 
        {
            foreach (string filepath in filepaths_from_lst)
            {
                Table T = new Table(filepath);
                Tables.Add(T);
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

                if (command.Contains("union ") && command.Contains("where ") && Match(command, command_mask1))
                {
                    _Semantec_analize = new Semantec_analize(Semantic_analizerUW);
                    return true;
                }
                else if (command.Contains("union ") && Match(command, command_mask2))
                {
                    _Semantec_analize = new Semantec_analize(SemanticAnalizerUnion);
                    return true;
                }
                else if (command.Contains("where ") && Match(command, command_mask3))
                {
                    _Semantec_analize = new Semantec_analize(SemanticAnalizerWhere);
                    return true;
                }


                return false;
            }


        public void Semantec_analize_foo(string cmd)
            {
                _Semantec_analize(cmd);
            }



        public static void Semantic_analizerUW(string cmd)
            {
                List<string> args_temp = new List<string>(cmd.Split('|'));
                SemanticAnalizerUnion(args_temp[0]);
                SemanticAnalizerWhere(args_temp[1]);
                //args.RemoveAll(s => s == "");
                //if (!args[2].EndsWith("|"))
                //    throw new FileLoadException("слишком много аргументов у операции union поддерживается объединение только двух таблиц");
                //else
                //    args[2] = args[2].Trim('|');


            }
        //

        public static void SemanticAnalizerUnion(string cmd)
            {

                //List<string> args = stri cmd.Split(' ');
                List<string> args = new List<string>(cmd.Split(' '));
                args.RemoveAll(s => s == "");
                if (args.Count != 3)
                    throw new FileLoadException("не корректная команда union");
                if (args[2].EndsWith("|"))
                    args[2] = args[2].Trim('|');
                union_arg1 = args[1];
                union_arg2 = args[2];



            }

        public static void SemanticAnalizerWhere(string cmd)
            {
                List<string> args = new List<string>(cmd.Split(' '));
                args.RemoveAll(s => s == "");
                if (args.Count != 4)
                    throw new FileLoadException("не корректная команда where");

                if (!(args[2] == ">" || args[2] == "<" || args[2] == "=" || args[2] == "l"))
                    throw new FileLoadException("не корректная операция. поддерживаются только > < = l");
                if (args[3].EndsWith("|"))
                    args[3] = args[3].Trim('|');
                where_col = args[1];
                where_op = args[2];
                where_arg = args[3];

            }


        // метод поиска по маске
        public  bool Match(string text, string match)
        {
            Stack<Tuple<int, int>> tasks = new Stack<System.Tuple<int, int>>();
            tasks.Push(Tuple.Create(0, 0));
            while (tasks.Count > 0)
            {
                var task = tasks.Pop();
                int it = task.Item1;
                int im = task.Item2;
                while (it < text.Length && im < match.Length)
                {
                    if (match[im] == '?')
                    {
                        it++;
                        im++;
                    }
                    else if (match[im] == '*')
                    {
                        tasks.Push(Tuple.Create(it + 1, im));
                        im++;
                    }
                    else if (match[im] == text[it])
                    {
                        it++;
                        im++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (it == text.Length)
                {
                    if (im == match.Length)
                        return true;

                    if (im == match.Length - 1 && match[im] == '*')
                        return true;
                }
            }
            return false;
        }


        //public string[] cleaning(string[] args)
        //{

        //    //return clean_args;
        //}
    }



    }


