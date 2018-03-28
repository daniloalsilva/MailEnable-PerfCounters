using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace MailEnable.CustomCounters.Program.Core.Configuration
{
    public class MailEnableWorkersCollection : ConfigurationElementCollection
    {
        public void Add(MailEnableWorkerElement customElement)
        {
            BaseAdd(customElement);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            base.BaseAdd(element, false);
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new MailEnableWorkerElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MailEnableWorkerElement)element).Name;
        }

        public MailEnableWorkerElement this[int Index]
        {
            get
            {
                return (MailEnableWorkerElement)BaseGet(Index);
            }
            set
            {
                if (BaseGet(Index) != null)
                {
                    BaseRemoveAt(Index);
                }
                BaseAdd(Index, value);
            }
        }

        new public MailEnableWorkerElement this[string Name]
        {
            get
            {
                return (MailEnableWorkerElement)BaseGet(Name);
            }
        }

        public int indexof(MailEnableWorkerElement element)
        {
            return BaseIndexOf(element);
        }

        public void Remove(MailEnableWorkerElement url)
        {
            if (BaseIndexOf(url) >= 0)
                BaseRemove(url.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
}
