using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace MailEnable.CustomCounters.Program.Core.Configuration
{
    public class MailEnableSection : ConfigurationSection
    {
        [ConfigurationProperty("workers", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(MailEnableWorkerElement), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public MailEnableWorkersCollection Instances
        {
            get
            {
                return (MailEnableWorkersCollection)base["workers"];
            }
        }
    }
}
