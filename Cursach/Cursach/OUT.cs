using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cursach
{
    class OUT
    {
    }

    class FormOut 
    {
        
        //Filter filter = new Filter(Match);
        public List<Row> _records;

        Form1 thisform;
        public  FormOut(Form1 thisform, List<Row> ext_records)
        {
            this.thisform = thisform;
            this._records = ext_records;
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


        public void Write()
        {
            thisform.lsbOut.Items.Clear();
            foreach (Row record in _records)
                thisform.lsbOut.Items.Add(String.Format("{0};{1};{2};{3};{4}", record.key, record.id, record.surname, record.name, record.last_name));
        }

    }
}
