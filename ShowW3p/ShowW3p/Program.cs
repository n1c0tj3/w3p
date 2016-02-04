using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ShowW3p
{
    class Program
    {
        static void Main(string[] args)
        {
            string proces = "w3wp.exe";
            string wmiQuery = string.Format("select ProcessId, CommandLine from Win32_Process where Name='{0}'", proces);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery);
            ManagementObjectCollection retObjectCollection = searcher.Get();
            foreach (ManagementObject retObject in retObjectCollection)
            {
                string commandline = retObject["CommandLine"] as string;
                string[] cols = commandline.Split('-');
                var pid = retObject["ProcessId"];

                Console.WriteLine(String.Format("{0} : {1}", pid, cols[1]));
            }
            if (retObjectCollection.Count < 1)
            {
                Console.WriteLine(String.Format("Er zijn geen {0} processen gevonden.", proces));
            }
            Console.ReadKey();
        }
    }
}
