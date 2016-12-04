using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursach
{
    class PROCESSING :StartInit
    {
        public List<Row> result_records;

        public delegate bool Filter(string txt, string command);

        

        public void less()
        {
            if (GetCol(where_col)> where_arg)
            {

            }
         }

        public void greater()
        { }

        public void equal()
        { }


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


        public void fill_result(Filter filter)
        {
            foreach (Row record in records)
                if (filter("", ""))
                    result_records.Add(record);

        }

    }
}
