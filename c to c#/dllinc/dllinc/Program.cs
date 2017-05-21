using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
// Allow managed code to call unmanaged functions that are implemented in a DLL
using System.Runtime.InteropServices;


namespace dllinc
{
    class Program
    {
        [DllImport("mathdll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SuperSort(int[] vector);

        static void Main(string[] args)
        {
            int[] a = { 4, 10 };


            SuperSort(a);
            Console.WriteLine($"c={a}");
            Thread.Sleep(2000);
        }
    }
}
