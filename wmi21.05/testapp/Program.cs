using System;
using System.Management;
using System.Windows.Forms;

namespace WMISample
{
    public class MyWMIQuery
    {
        public static void Main()
        {
            try
            {
                string[] arrComputers = { "192.168.88.239", "UFA-URAL-5", "UFA-URAL-10" };
                foreach (string strComputer in arrComputers)
                {
                    Console.WriteLine("==========================================");
                    Console.WriteLine("Computer: " + strComputer);
                    Console.WriteLine("==========================================");

                    ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher(
                        "\\\\" + strComputer + "\\root\\CIMV2",
                        "SELECT * FROM Win32_QuickFixEngineering");

                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("Win32_QuickFixEngineering instance");
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("HotFixID: {0}", queryObj["HotFixID"]);
                        Console.WriteLine("InstallDate: {0}", queryObj["InstallDate"]);
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException err)
            {
                MessageBox.Show("комп выключен или не удалось подключиться" + err.Message);
            }
            catch (ManagementException err)
            {
                MessageBox.Show("An error occurred while querying for WMI data: " + err.Message);
            }
        }
    }
}