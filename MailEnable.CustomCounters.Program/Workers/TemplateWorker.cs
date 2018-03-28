using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailEnable.CustomCounters.Program.Workers
{
    public class TemplateWorker : WorkerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public override string Description
        {
            get
            {
                return "Responsible to generate httpd.conf file to Isapi Rewrite v3 in Sharred Hosting 2012 environment";
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TemplateWorker(int poolingIntervalInSeconds)
            : base(poolingIntervalInSeconds)
        {

        }

        /// <summary>
        /// Start execution
        /// </summary>
        public override void Start()
        {
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

            // Faz a magica acontecer
        }
    }
}
