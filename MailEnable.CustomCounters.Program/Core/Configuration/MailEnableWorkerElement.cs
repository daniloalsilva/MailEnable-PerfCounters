using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace MailEnable.CustomCounters.Program.Core.Configuration
{
    public class MailEnableWorkerElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("assembly", IsRequired = true)]
        public string Assembly
        {
            get { return (string)base["assembly"]; }
            set { base["assembly"] = value; }
        }

        [ConfigurationProperty("poolingInterval", IsRequired = true, DefaultValue = "300")]
        public int PoolingInterval
        {
            get { return Convert.ToInt32(base["poolingInterval"]); }
            set { base["poolingInterval"] = value; }
        }

        [ConfigurationProperty("enabled", IsRequired = true, DefaultValue = true)]
        public bool Enabled
        {
            get { return Convert.ToBoolean(base["enabled"]); }
            set { base["enabled"] = value; }
        }
    }
}
