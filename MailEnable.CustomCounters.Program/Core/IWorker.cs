using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailEnable.CustomCounters.Program.Core
{
    public interface IWorker
    {
        /// <summary>
        /// Name of worker
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Description of worker
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Pooling interval
        /// </summary>
        int PoolingInterval { get; }

        /// <summary>
        /// Start worker execution
        /// </summary>
        void Start();

        /// <summary>
        /// Stop worker execution
        /// </summary>
        void Stop();
    }
}
