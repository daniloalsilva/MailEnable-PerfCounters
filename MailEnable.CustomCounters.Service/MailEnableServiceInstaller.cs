using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace MailEnable.CustomCounters.Service
{
    [RunInstaller(true)]
    public partial class MailEnableServiceInstaller : System.Configuration.Install.Installer
    {
        public MailEnableServiceInstaller()
        {
            InitializeComponent();
            
            // Defining service account
            ServiceProcessInstaller process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;

            // Setting service properties
            ServiceInstaller serviceAdmin = new ServiceInstaller();
            serviceAdmin.StartType = ServiceStartMode.Automatic;
            serviceAdmin.ServiceName = "MailEnableCounters";
            serviceAdmin.DisplayName = "Mail Enable Performance Counters";
            serviceAdmin.Description = "Service responsible to create custom performance counter for Mail Enable";

            // Add installer
            this.Installers.AddRange(new System.Configuration.Install.Installer[] { serviceAdmin, process });

        }
    }
}
