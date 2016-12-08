using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using CsvHelper;

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
            try
            {
                /* получаем список путей к файлам таблиц  в переменную FilePaths
                HashSet - структура которая не может содержать повторяющихся элементов*/
                HashSet<string> FilePaths = new HashSet<string>();
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
            }
            catch(ArgumentException Errno)
            {
                MessageBox.Show(this, Errno.Message, Errno.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // после заполнения результирующей таблицы включим кнопку сохранить
            btnSave.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lstFilePaths.Items.Add(Service.FileSelect());
            // включим кнопку "результат"
            button2.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _Out OutputToFile = new _Out(this, result);
                OutputToFile.Write();
            }
            catch
            {
                MessageBox.Show(this, "Возникла ошибка при сохранении файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            

        }

        private void lstCmd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCmd_Click(object sender, EventArgs e)
        {

            try
            {
                Service.ReadCmd(this); //прочитали файл в кнопку
            }
            catch
            {
                MessageBox.Show(this, "Возникла ошибка при чтении файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
