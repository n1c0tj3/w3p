using ShowW3p.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ShowW3p.Lib
{
    public class ProcessRepository
    {
        public List<Process> FindProcessOnLocalMachine(string name)
        {
            var result = new List<Process>();
            string wmiQuery = string.Format("select ProcessId, CommandLine from Win32_Process where Name='{0}'", name);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery);
            ManagementObjectCollection retObjectCollection = searcher.Get();
            foreach (ManagementObject retObject in retObjectCollection)
            {
                string commandline = retObject["CommandLine"] as string;
                string[] cols = commandline.Split('-');
                var pid = retObject["ProcessId"];
                var p = new Process()
                {
                    Pid = (uint)retObject["ProcessId"],
                    Name = cols[1].Substring(3)
                };
                result.Add(p);
            }
            return result;
        }

        public List<Process> FindProcess(string name, string remotemachine)
        {
            if ( String.IsNullOrEmpty(remotemachine))
            {
                return FindProcessOnLocalMachine(name);
            }
            ManagementScope scope = new ManagementScope("\\\\" + remotemachine + "\\root\\cimv2");
            scope.Connect();
            var result = new List<Process>();
            string wmiQuery = string.Format("select ProcessId, CommandLine from Win32_Process where Name='{0}'", name);
            ObjectQuery query = new ObjectQuery(wmiQuery);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection retObjectCollection = searcher.Get();
            foreach (ManagementObject retObject in retObjectCollection)
            {
                string commandline = retObject["CommandLine"] as string;
                string[] cols = commandline.Split('-');
                var pid = retObject["ProcessId"];
                var p = new Process()
                {
                    Pid = (uint)retObject["ProcessId"],
                    Name = cols[1].Substring(3).Replace('"',' ' )
                };
                result.Add(p);
            }
            return result;
        }
    }
}
