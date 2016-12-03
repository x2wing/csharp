using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab7form
{
    

    public partial class Form1 : Form
    {
        ToForm Data1;
        ToFile Data2;
        SourceToForm Data3;



        public Form1()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Data3 = new SourceToForm(this);
            Data3.Write(null);
        }
        // на форму
        private void button3_Click(object sender, EventArgs e)
        {
            Data1 = new ToForm(this);

            Data1.Stoped();
            Data1.OverSpeed();
            Data1.FixedSpeed();
            Data1.Write(null);

        }

        private void rtbOut_TextChanged(object sender, EventArgs e)
        {
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Data2 = new ToFile();

            Data2.Stoped();
            Data2.OverSpeed();
            Data2.FixedSpeed();
            Data2.Write(@"out.csv");

        }

        //public RichTextBox G_rtbOut()
        //{
        //    return rtbOut;
        //}


    }


    
}
