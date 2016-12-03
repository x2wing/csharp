using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        private int b =2;
        static void Main(string[] args)
        {
            Console.WriteLine(this.b);
        }

        static void squareVal(int valParameter)
        {
            valParameter *= valParameter;
        }

        // Passing by reference
        static void squareRef(ref int refParameter)
        {
            refParameter *= refParameter;
        }
    }
}
