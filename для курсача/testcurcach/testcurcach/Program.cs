using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ConsoleApplication1
{
    class myfield
    {
        public int A;
        public int B;
        public int a { get; set; }
        public int b { get; set; }
    }
    class Program
    {
        public int A { get; set; }
        public int B { get; set; }
        static void Main(string[] args)
        {

            FieldInfo[] myFieldInfo;
            Type myType = typeof(myfield);
            // Get the type and fields of FieldInfoClass.
            myFieldInfo = myType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance
                | BindingFlags.Public);
            var S = typeof(myfield).GetFields(BindingFlags.Public | BindingFlags.Instance);


            Console.WriteLine("\nName            : {0}", myFieldInfo[0].Name);
            Console.WriteLine("Declaring Type  : {0}", myFieldInfo[1].Name);
            Console.WriteLine("IsPublic        : {0}", myFieldInfo[0].IsPublic);
            Console.WriteLine("MemberType      : {0}", myFieldInfo[0].MemberType);
            Console.WriteLine("FieldType       : {0}", myFieldInfo[0].FieldType);
            Console.WriteLine("IsFamily        : {0}", myFieldInfo[0].IsFamily);
            
            string txtstream = "union t1 t2 where id=0";
            string regexp = "union * * where *=*";

            Console.WriteLine(test.Getcol(txtstream));
            //bool flag = test.Match(txtstream, regexp);
            Console.ReadKey();
            
        }
    }
}
