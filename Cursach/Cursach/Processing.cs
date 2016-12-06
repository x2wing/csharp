using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Cursach
{
    class PROCESSING :StartInit
    {
        public PROCESSING(List<string> filepath_from_lst) : base(filepath_from_lst)
        {
        }

        //public List<Row> result_records;

        public delegate bool Filter(string txt, string command);


        public Filter FooSelect()
        {
            switch (where_op)
            {
                case ">":
                    return item.key;
                case "<":
                    return item.id;
                case "=":
                    return item.surname;
                case "l":
                    return;
                default:
                    throw new Exception("операция не поддерживается используйте > < = l");

            }
        }

        public void fill_result(Filter filter)
        {
            Table result_table = new Table();
            foreach (Table T in Tables)
                if (union_arg1 == T.tablename || union_arg2 == T.tablename) //если первый аргумент совпадает с именем таблицы
                    foreach (Row record in T.records)
                        if (filter(record[where_col], where_arg))
                            result_table.records.Add(record);

                else if (Tables.Count == 1) ; //какое-то действие
               

        }

        public static string FileSelect()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                return dlg.FileName;
            }
            return null;
        }

        // метод поиска по маске
        public static bool Match(string text, string match)
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


        

    }
}

