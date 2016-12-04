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
            FormOut A = new FormOut(this, Data1.records);
            A.Write();
            bool flag = PROCESSING.Match(txtCommnd.Text, "union * * where *");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string filepath=PROCESSING.FileSelect();
            txtFilePath.Text = filepath;
            Data1 = new StartInit();
            Data1.Filling(filepath);
            // включим кнопку "результат"
            button2.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Table file1 = new Table();
        }
    }
}
