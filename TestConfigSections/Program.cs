using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Diagnostics.PerformanceData;
using System.Diagnostics;
using Microsoft.Win32;

namespace TestConfigSections
{
    class Program
    {
        static void Main(string[] args)
        {
            // variable declaration
            PerfCounters perfCounters;

            // load configuration file
            using (StreamReader reader = new StreamReader(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Registry.Json"))

                // deserialize perfCounter object
                perfCounters = JsonConvert.DeserializeObject<PerfCounters>(reader.ReadToEnd());

            Console.WriteLine("Type 'N' to stop, any other will increment counter in 1");

            while (Console.ReadLine() != "N")
            {
                // post all category counter values
                perfCounters.PostCounterValues();
            }

        }
    }
}

