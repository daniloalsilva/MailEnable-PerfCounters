using MailEnable.CustomCounters.Program.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MailEnable.CustomCounters.Program.Workers
{
    public class RegistryWorker : WorkerBase
    {
        // variable declaration
        PerfCounters perfCounters;

        /// <summary>
        /// 
        /// </summary>
        public override string Description
        {
            get
            {
                return "Responsible to create performance counters and post collected values from registry";
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public RegistryWorker(int poolingIntervalInSeconds)
            : base(poolingIntervalInSeconds)
        {

        }

        /// <summary>
        /// Start execution
        /// </summary>
        public override void Start()
        {
            // load configuration file
            using (StreamReader reader = new StreamReader(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Registry.Json"))

                // deserialize perfCounter object
                perfCounters = JsonConvert.DeserializeObject<PerfCounters>(reader.ReadToEnd());

            // First execution will be 10 seconds after initialization
            // After, will be set to pooling correct
            this._poolingTimer = new System.Timers.Timer(new TimeSpan(0, 0, 10).TotalSeconds);
            this._poolingTimer.Elapsed += ExecuteGathering;
            this._poolingTimer.Start();
        }

        /// <summary>
        /// Stop execution
        /// </summary>
        public override void Stop()
        {
            base.Stop();
        }

        private void ExecuteGathering(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Update pooling interval and create log obj
            this._poolingTimer.Interval = this.PoolingInterval;

            // post all category counter values
            perfCounters.PostCounterValues();
        }
    }
}
