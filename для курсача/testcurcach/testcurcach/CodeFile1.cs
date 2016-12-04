using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace ConsoleApplication1
{

    class test
    {
        public static List<string> cols = new List<string> {"id", "key", "name" };
        
        public static string Getcol(string text)
        {
            foreach(string i in cols)
                if (text.Contains(i))
                {
                    return i;
                }

            return null;
        }

        public static bool Match(string text, string match)
        {
            Stack<Tuple<int, int>> tasks = new Stack<System.Tuple<int, int>>();
            tasks.Push(Tuple.Create(0, 0));
            while (tasks.Count > 0)
            {
                var task = tasks.Pop();
                int it = task.Item1;
                int im = task.Item2;
                //Tuple<int, int> colname=
                string where_col;
                string where_op;
                string where_value;

                while (it < text.Length && im < match.Length)
                {

                    if (match[im] == '?')// если текущий символ маски ?
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