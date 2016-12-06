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
        StartInit Data1;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> FilePaths=new List<string>();
            foreach (string item in lstFilePaths.Items)
            {
                FilePaths.Add(item);
            }

            PROCESSING data = new PROCESSING(FilePaths);
            // FilePaths в конструктор
            //FormOut A = new FormOut(this, Data1.records);
            //A.Write();
            //bool flag = PROCESSING.Match(txtCommnd.Text, "union * * where *");
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
            //Table file1 = new Table();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            //StartInit A = new StartInit("");
            //foreach (string icmd in lstCmd.Items)
            //{
            //    if (A.Sintax_analize(icmd))
            //        A.Semantec_analize_foo(icmd);
            //}

        }

        private void lstCmd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
