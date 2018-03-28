using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using MailEnable.CustomCounters.Program;
using MailEnable.CustomCounters.Program.Core.Configuration;
using MailEnable.CustomCounters.Program.Workers;

namespace MailEnable.CustomCounters.Service
{
    public partial class MailEnableService : ServiceBase
    {
        private MailEnableManager mailEnableManager;

        public MailEnableService()
        {
            mailEnableManager = new MailEnableManager();

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // Create service manager
            this.mailEnableManager = new MailEnableManager();
            // Getting workers from configuration file
            MailEnableSection config = (MailEnableSection)ConfigurationManager.GetSection("MailEnable");

            // Add each worker to manager
            foreach (MailEnableWorkerElement worker in config.Instances)
            {
                try
                {
                    Type type = Type.GetType(worker.Assembly);
                    var workerObj = (WorkerBase)Activator.CreateInstance(type, new object[] { worker.PoolingInterval });
                    this.mailEnableManager.AddWorker(workerObj);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            mailEnableManager.Run();
        }

        protected override void OnStop()
        {
            if (this.mailEnableManager != null)
                this.mailEnableManager.Stop();
        }
    }
}
