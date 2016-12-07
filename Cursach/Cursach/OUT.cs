using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;

namespace Cursach
{
    class FormOut:_Out
    {
        public FormOut(Form1 thisform, Table ext_records) : base(thisform, ext_records) { }

        public override void Write()
        {
            thisform.lsbOut.Items.Clear();
            foreach (Row record in t_result.records)
                thisform.lsbOut.Items.Add(String.Format("{0};{1};{2};{3};{4}", record.key, record.id, record.surname, record.name, record.last_name));
        }
    }

    class _Out 
    {
        
        
        public Table t_result;

        protected Form1 thisform;
        public  _Out(Form1 thisform, Table ext_records)
        {
            this.thisform = thisform;
            this.t_result = ext_records;
        }

        public void ReadCmd()
        {
            string line;
            StreamReader file = new StreamReader(PROCESSING.FileSelect());
            while ((line = file.ReadLine()) != null)
            {
                thisform.lstCmd.Items.Add(line);

            }

        }

        public virtual  void Write()
        {
            using (var fd = new StreamWriter(@"В.csv"))
            {
                var writer = new CsvWriter(fd);
                writer.WriteRecords(t_result.records);
            }
        }



    }
}
