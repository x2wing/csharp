﻿using System;
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
        private delegate bool OperationDelegate(string txt, string mask);
        private Dictionary<string, OperationDelegate> _operations;
        //тут будет хранится результат
        Table result_table = new Table();

        public PROCESSING(List<string> filepath_from_lst) : base(filepath_from_lst)
        {
            FooSelecter();
        }

        //public List<Row> result_records;

        //public delegate bool Filter(string txt, string command);

        

       
            // механизм делегата выбират нужную функцию в зависмости от операции
        public void FooSelecter()
        {
            _operations = new Dictionary<string, OperationDelegate>

            {
                { ">", (col, arg) => String.CompareOrdinal(col,  arg)>0 },
                { "<", (col, arg) => String.CompareOrdinal(col,  arg)<0 },
                { "=", (col, arg) => String.Equals(col,  arg)},
                { "l", Match},
            };

            
        }

        public void fill_result()
        {
            
            foreach (Table T in Tables)
                if (union_arg1 == T.tablename || union_arg2 == T.tablename) //если первый аргумент совпадает с именем таблицы
                    foreach (Row record in T.records)
                        /* тут самая сильная магия (делегаты, индексаторы, словари)
                         если выбранное поле отдельной записи > < = или по маске(в зависмости отwhere_op) чем where_arg (правый аругмент в команде)
                         то добавляем в результирующую таблицу
                         в [] ключ словаря в () параметры функции(или лямбды) в делегате вытащеном из значения словаря
                         */
                        if (_operations[where_op](record[where_col], where_arg))
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

        


        

    }
}

