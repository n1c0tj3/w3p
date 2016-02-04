using ShowW3p.Lib;
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
            ProcessRepository rep = new ProcessRepository();
            var name = "w3wp.exe";
            var proceslist = rep.FindProcessOnLocalMachine(name);

            Console.WriteLine(String.Format("Aantal {0} processen : {1}\n", name, proceslist.Count));
            foreach (var item in proceslist)
            {
                Console.WriteLine(String.Format("{0} : {1}", item.Pid, item.Name));
            }
            Console.ReadKey();
        }
    }
}
