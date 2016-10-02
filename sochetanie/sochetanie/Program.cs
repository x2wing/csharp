using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    
    class Program
    {
        static void Main(string[] args)
        {
            int[] sum = new int[20];
            int[] m = { 1, 2, 3, 4, 5, 6, 7, 8 };
            int counter=0;
            for (int i=0; i < m.Length -2; i++ )
                for(int j=i+1; j<m.Length-1;j++)
                    for (int k = j + 1; k < m.Length; k++)
                    {
                        Console.Write("{0}) ",counter++);
                        Console.WriteLine("{0}{1}{2}", m[i], m[j], m[k]);
                    }

                //for (int a = 0; a < m.Length;a++ )
                //    for (int i = a; i < m.Length-1 ; i++)
                //        Console.WriteLine("m[a] = {0}\nm[i+1]={1}",m[a], m[i + 1]);
                //sum[i] = m[a] + m[i+1];

                //for (int i=0; i<sum.Length; i++)
                //    Console.WriteLine(sum[i]);

                Console.ReadKey();
        }

        
    }
}
