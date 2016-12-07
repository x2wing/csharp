using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;

namespace Cursach
{
    public partial class Form1 : Form
    {
        Table result; 

        public Form1()
        {
            InitializeComponent();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            /* получаем список путей к файлам таблиц  в переменную FilePaths
            HashSet - структура которая не может содержать повторяющихся элементов*/
            HashSet<string> FilePaths=new HashSet<string>();
            foreach (string item in lstFilePaths.Items)
            {
                FilePaths.Add(item);
            }
            // создаем все
            PROCESSING data = new PROCESSING(FilePaths);
            // анализ и разбор команд в цикле
            foreach (string icmd in lstCmd.Items)
            {
                if (data.Sintax_analize(icmd))
                {
                    data.Semantec_analize_foo(icmd);
                    data.Fill_result();
                }
            }


            result = data.Get_result_table();// вывод результата на форму параметры конструктора:this - экземпляр формы и data.Get_result_table()-результирующая таблица
            FormOut OutputToForm = new FormOut(this, result);
            OutputToForm.Write();
           
            // после заполнения результирующей таблицы включим кнопку сохранить
            btnSave.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lstFilePaths.Items.Add(PROCESSING.FileSelect());
            //string A = lstFilePaths.Items.ToString();
            //var a = lstFilePaths.Items.;
            //txtFilePath.Text = filepath;
            //Data1 = new StartInit();
            //Data1.Filling(filepath);
            //// включим кнопку "результат"
            button2.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Out OutputToFile = new _Out(this, result);
            OutputToFile.Write();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            

        }

        private void lstCmd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCmd_Click(object sender, EventArgs e)
        {

            _Out OUT = new _Out(this, null );
            OUT.ReadCmd(); //прочитали файл в кнопку
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
