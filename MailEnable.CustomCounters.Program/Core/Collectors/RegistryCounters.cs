using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MailEnable.CustomCounters.Program.Core
{
    public class PerfCounters
    {
        // variable definition
        public RegistryCategoryCounter[] registryCategoryCounter;

        // constructor
        public PerfCounters(RegistryCategoryCounter[] registryCategoryCounter)
        {
            this.registryCategoryCounter = registryCategoryCounter;

            // initialize counter categories
            foreach (RegistryCategoryCounter categoryCounter in registryCategoryCounter)
                categoryCounter.CreateCategoryCounter();
        }

        // post counter values
        public void PostCounterValues()
        {
            // post counter values
            foreach (RegistryCategoryCounter categoryCounter in registryCategoryCounter)
               categoryCounter.PostCounterRawValue();
        }
    }

    public class RegistryCategoryCounter
    {
        // variable definition
        public string categoryName;
        public string categoryHelp;
        public RegistryCounter[] registryCounters;

        // constructor 
        public RegistryCategoryCounter(RegistryCounter[] registryCounters, string categoryName, string categoryHelp)
        {
            this.registryCounters = registryCounters;
            this.categoryName = categoryName;
            this.categoryHelp = categoryHelp;
        }

        // post counter raw data
        public void PostCounterRawValue()
        {
            foreach (RegistryCounter registryCounter in registryCounters)
                try
                {
                    // load PerformanceCounter object 
                    using (var perfCounter = new PerformanceCounter(this.categoryName, registryCounter.counterName, false))

                        // post registry value in counter
                        perfCounter.RawValue = registryCounter.GetRegistryValue();
                }
                catch (Exception)
                {
                    // omit throw - must be changed in next version
                }
        }

        // creates category counter
        public void CreateCategoryCounter()
        {
            // validate if category exists before continue
            if (PerformanceCounterCategory.Exists(categoryName))
            {
                // deletes category counter if its already created
                PerformanceCounterCategory.Delete(categoryName);
            }

            // create a collection object 
            CounterCreationDataCollection CounterDatas = new CounterCreationDataCollection();

            // adding counters of the object to collection
            foreach (RegistryCounter counter in registryCounters)
                CounterDatas.Add(counter.counterData);

            // creating counter
            PerformanceCounterCategory.Create(categoryName, categoryHelp, CounterDatas);
        }
    }

    public class RegistryCounter
    {
        // variable definition
        public string registryName, registryPath, counterName;
        public bool createDelta;
        internal string fileName;
        public CounterCreationData counterData;

        // constructor
        public RegistryCounter(string registryName, string registryPath, string counterName, bool createDelta)
        {
            // initialize variables
            this.registryName = registryName;
            this.registryPath = registryPath;
            this.counterName = counterName;
            this.createDelta = createDelta;

            // prepare filepath to save temp files if delta is used
            fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + String.Format(@"\Delta\{0}.data", counterName));

            // creates counter object for initialized objects
            CreateCounter();
        }

        // create counter object
        public void CreateCounter()
        {
            counterData = new CounterCreationData();
            counterData.CounterName = counterName;
            counterData.CounterHelp = String.Format("{0} - {1} - {2}", counterName, registryName, registryPath);
            counterData.CounterType = PerformanceCounterType.NumberOfItems64;
        }

        /// <returns>Returns registry value found on path used to create object</returns>
        public long GetRegistryValue()
        {
            // store long data retrieved from registry
            long data = long.Parse(Registry.GetValue(registryPath, registryName, "0").ToString());

            Console.WriteLine("delta option: {0}", createDelta);

            // validate if this counter must use a delta value
            if (createDelta)
            {
                // retrieve last data
                long delta = RetrieveLastData();

                // serialize actual data
                SaveData(data);

                Console.WriteLine("delta: {0}, data: {1}", delta, data);

                // this registry data is always incremental, if delta if greater than data, counter must be reset
                if (delta >= data)
                    return 0;
                else
                    return data - delta;
            }
            else
                // return data without delta
                return data;
        }

        // method to save counter data 
        internal void SaveData(long data)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
                writer.Write(data);
        }

        // method to retrieve last counter data
        internal long RetrieveLastData()
        {
            // create directory if dont exists
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            // try/catch for retriving last data
            try
            {
                // read data file
                using (StreamReader reader = new StreamReader(fileName))
                    // return it's value
                    return long.Parse(reader.ReadToEnd());
            }

            // if file dont exists, return zero
            catch (Exception)
            {
                Console.WriteLine("file dont exists {0}", fileName);
                return (long)0;
            }

        }
    }

}
